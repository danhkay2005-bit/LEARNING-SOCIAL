using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System.Text.RegularExpressions;

namespace StudyApp.BLL.Services.Learn
{
    public class BoDeHocService : IBoDeHocService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public BoDeHocService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoDeHocResponse>> GetByFilterAsync(int maChuDe)
        {
            // Đợi cho đến khi "khóa" được mở
            await _semaphore.WaitAsync();
            try
            {
                // 1. Khởi tạo query và lọc ngay các bản ghi đã xóa
                var query = _context.BoDeHocs
                    .Where(b => b.DaXoa == false) // HOẶC !b.DaXoa (Chỉ lấy các bản ghi chưa bị xóa)
                    .AsQueryable();

                // 2. Lọc theo chủ đề nếu có
                if (maChuDe > 0)
                {
                    query = query.Where(b => b.MaChuDe == maChuDe);
                }

                // 3. Thực thi lấy danh sách với các điều kiện công khai và sắp xếp
                var list = await query
                    .Where(b => b.LaCongKhai == true)
                    .OrderByDescending(b => b.ThoiGianTao)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<BoDeHocResponse>>(list);
            }
            finally
            {
                // Giải phóng khóa
                _semaphore.Release();
            }
        }

        #region Helper Methods (Xử lý Hashtag)



        private async Task ClearSpecializedData(int maThe)
        {
            // 1. Xóa đáp án Trắc nghiệm
            var tracNghiems = _context.DapAnTracNghiems.Where(x => x.MaThe == maThe);
            if (await tracNghiems.AnyAsync()) _context.DapAnTracNghiems.RemoveRange(tracNghiems);

            // 2. Xóa từ Điền khuyết
            var dienKhuyets = _context.TuDienKhuyets.Where(x => x.MaThe == maThe);
            if (await dienKhuyets.AnyAsync()) _context.TuDienKhuyets.RemoveRange(dienKhuyets);

            // 3. Xóa phần tử Sắp xếp
            var sapXeps = _context.PhanTuSapXeps.Where(x => x.MaThe == maThe);
            if (await sapXeps.AnyAsync()) _context.PhanTuSapXeps.RemoveRange(sapXeps);

            // 4. Xóa cặp Ghép cặp
            var ghepCaps = _context.CapGheps.Where(x => x.MaThe == maThe);
            if (await ghepCaps.AnyAsync()) _context.CapGheps.RemoveRange(ghepCaps);

            // Lưu tạm thời để đảm bảo các bản ghi cũ bị xóa trước khi chèn mới
            await _context.SaveChangesAsync();
        }

        private async Task InsertSpecializedData(int maThe, ChiTietTheRequest request)
        {
            // Tùy vào dữ liệu trong request có gì thì chèn cái đó

            // 1. Nếu có dữ liệu Trắc nghiệm
            if (request.DapAnTracNghiem != null && request.DapAnTracNghiem.Any())
            {
                var items = request.DapAnTracNghiem.Select(x => {
                    var entity = _mapper.Map<DapAnTracNghiem>(x);
                    entity.MaThe = maThe;
                    return entity;
                });
                await _context.DapAnTracNghiems.AddRangeAsync(items);
            }

            // 2. Nếu có dữ liệu Điền khuyết
            if (request.TuDienKhuyets != null && request.TuDienKhuyets.Any())
            {
                var items = request.TuDienKhuyets.Select(x => {
                    var entity = _mapper.Map<TuDienKhuyet>(x);
                    entity.MaThe = maThe;
                    return entity;
                });
                await _context.TuDienKhuyets.AddRangeAsync(items);
            }

            // 3. Nếu có dữ liệu Sắp xếp
            if (request.PhanTuSapXeps != null && request.PhanTuSapXeps.Any())
            {
                var items = request.PhanTuSapXeps.Select(x => {
                    var entity = _mapper.Map<PhanTuSapXep>(x);
                    entity.MaThe = maThe;
                    return entity;
                });
                await _context.PhanTuSapXeps.AddRangeAsync(items);
            }

            // 4. Nếu có dữ liệu Ghép cặp
            if (request.CapGheps != null && request.CapGheps.Any())
            {
                var items = request.CapGheps.Select(x => {
                    var entity = _mapper.Map<CapGhep>(x);
                    entity.MaThe = maThe;
                    return entity;
                });
                await _context.CapGheps.AddRangeAsync(items);
            }
        }
        /// <summary>
        /// Tự động tách hashtag từ mô tả, cập nhật bảng Tags và bảng trung gian TagBoDes
        /// </summary>
        private async Task HandleHashtagsAsync(int boDeId, string? moTa)
        {
            // Regex lấy danh sách hashtag mới từ mô tả
            var hashtagMatches = Regex.Matches(moTa ?? "", @"#([\p{L}\p{N}_]+)");
            var hashtagNames = hashtagMatches.Cast<Match>()
                                             .Select(m => m.Groups[1].Value.ToLower().Trim())
                                             .Distinct()
                                             .ToList();

            // 1. LẤY LIÊN KẾT CŨ VÀ GIẢM SỐ LƯỢT DÙNG
            // Sử dụng MaTagNavigation như trong Profile AutoMapper của bạn
            var oldLinks = await _context.TagBoDes
                                         .Include(x => x.MaTagNavigation)
                                         .Where(x => x.MaBoDe == boDeId)
                                         .ToListAsync();

            foreach (var link in oldLinks)
            {
                if (link.MaTagNavigation != null && link.MaTagNavigation.SoLuotDung > 0)
                {
                    link.MaTagNavigation.SoLuotDung--;
                }
            }

            // Xóa các liên kết cũ
            _context.TagBoDes.RemoveRange(oldLinks);
            await _context.SaveChangesAsync();

            if (!hashtagNames.Any()) return;

            // 2. XỬ LÝ HASHTAG MỚI VÀ CỘNG SỐ LƯỢT DÙNG
            foreach (var name in hashtagNames)
            {
                // Tìm Tag đã tồn tại
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TenTag.ToLower() == name);

                if (tag == null)
                {
                    // Nếu chưa có thì tạo mới, mặc định lượt dùng là 1
                    tag = new Tag { TenTag = name, SoLuotDung = 1 };
                    _context.Tags.Add(tag);
                }
                else
                {
                    // Nếu đã có thì cộng thêm 1
                    tag.SoLuotDung++;
                }

                // Lưu để lấy MaTag cho bản ghi trung gian
                await _context.SaveChangesAsync();

                // 3. TẠO LIÊN KẾT MỚI
                _context.TagBoDes.Add(new TagBoDe
                {
                    MaBoDe = boDeId,
                    MaTag = tag.MaTag
                });
            }

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read Operations
        public async Task<BoDeHocResponse?> GetByIdAsync(int id)
        {
            var item = await _context.BoDeHocs
                .Include(x => x.TagBoDes).ThenInclude(t => t.MaTagNavigation)
                .FirstOrDefaultAsync(x => x.MaBoDe == id && x.DaXoa != true);

            return _mapper.Map<BoDeHocResponse>(item);
        }

        public async Task<IEnumerable<BoDeHocResponse>> GetByUserAsync(Guid userId)
        {
            var list = await _context.BoDeHocs
                .Include(x => x.TagBoDes).ThenInclude(t => t.MaTagNavigation)
                .Where(x => x.MaNguoiDung == userId && x.DaXoa != true)
                .OrderByDescending(x => x.ThoiGianTao)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BoDeHocResponse>>(list);
        }
        #endregion

        #region CRUD & Specialized Operations
        public async Task<BoDeHocResponse> CreateAsync(TaoBoDeHocRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var boDe = _mapper.Map<BoDeHoc>(request);
                boDe.ThoiGianTao = DateTime.Now;
                boDe.DaXoa = false;

                _context.BoDeHocs.Add(boDe);
                await _context.SaveChangesAsync();

                await HandleHashtagsAsync(boDe.MaBoDe, request.MoTa);

                await transaction.CommitAsync();
                return _mapper.Map<BoDeHocResponse>(boDe);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BoDeHocResponse> UpdateFullAsync(int id, LuuToanBoBoDeRequest request)
        {
            // 1. Kiểm tra bộ đề tồn tại
            var existingBoDe = await _context.BoDeHocs
                .Include(b => b.TheFlashcards) // Load kèm danh sách thẻ cũ
                .FirstOrDefaultAsync(b => b.MaBoDe == id);

            if (existingBoDe == null || existingBoDe.DaXoa == true)
                throw new KeyNotFoundException("Bộ đề không tồn tại.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 2. CẬP NHẬT THÔNG TIN CHUNG (HEADER)
                _mapper.Map(request.ThongTinChung, existingBoDe);
                existingBoDe.ThoiGianCapNhat = DateTime.Now;
                _context.BoDeHocs.Update(existingBoDe);

                // 3. XỬ LÝ DANH SÁCH THẺ (CARDS)
                // Lấy danh sách ID thẻ từ Request (chỉ những thẻ có MaThe - tức là thẻ cũ được update)
                var requestCardIds = request.DanhSachThe
                    .Where(t => t.TheChinh is CapNhatTheFlashcardRequest)
                    .Select(t => ((CapNhatTheFlashcardRequest)t.TheChinh).MaThe)
                    .ToList();

                // Bước A: XÓA THẺ (Những thẻ có trong DB nhưng không có trong Request)
                var cardsToDelete = existingBoDe.TheFlashcards
                    .Where(c => !requestCardIds.Contains(c.MaThe))
                    .ToList();
                if (cardsToDelete.Any()) _context.TheFlashcards.RemoveRange(cardsToDelete);

                // Bước B: CẬP NHẬT HOẶC THÊM MỚI
                foreach (var cardReq in request.DanhSachThe)
                {
                    if (cardReq.TheChinh is CapNhatTheFlashcardRequest updateReq)
                    {
                        // -- CẬP NHẬT THẺ ĐANG CÓ --
                        var existingCard = existingBoDe.TheFlashcards.FirstOrDefault(c => c.MaThe == updateReq.MaThe);
                        if (existingCard != null)
                        {
                            _mapper.Map(updateReq, existingCard);
                            _context.TheFlashcards.Update(existingCard);

                            // Xử lý dữ liệu đặc thù (Xóa cũ nạp mới cho đơn giản)
                            await ClearSpecializedData(existingCard.MaThe);
                            await InsertSpecializedData(existingCard.MaThe, cardReq);
                        }
                    }
                    else
                    {
                        // -- THÊM THẺ MỚI (Người dùng nhấn nút + trong lúc sửa) --
                        var newCard = _mapper.Map<TheFlashcard>(cardReq.TheChinh);
                        newCard.MaBoDe = id;
                        _context.TheFlashcards.Add(newCard);
                        await _context.SaveChangesAsync(); // Để lấy ID mới cho thẻ

                        await InsertSpecializedData(newCard.MaThe, cardReq);
                    }
                }

                // 4. Xử lý Hashtags (giữ nguyên logic cũ của bạn)
                await HandleHashtagsAsync(id, request.ThongTinChung.MoTa);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<BoDeHocResponse>(existingBoDe);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.BoDeHocs.FindAsync(id);
            if (existing == null) return false;

            existing.DaXoa = true;
            existing.ThoiGianCapNhat = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<BoDeHocResponse> ForkAsync(SaoChepBoDeHocRequest request)
        {
            var origin = await _context.BoDeHocs
                .Include(x => x.TheFlashcards).ThenInclude(f => f.CapGheps)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.DapAnTracNghiems)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.PhanTuSapXeps)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.TuDienKhuyets)
                .FirstOrDefaultAsync(x => x.MaBoDe == request.MaBoDeGoc);

            if (origin == null) throw new Exception("Bộ đề gốc không tồn tại.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newBoDe = new BoDeHoc
                {
                    MaNguoiDung = request.MaNguoiDungMoi,
                    MaChuDe = origin.MaChuDe,
                    TieuDe = $"[Bản sao] {origin.TieuDe}",
                    MoTa = origin.MoTa,
                    AnhBia = origin.AnhBia,
                    MucDoKho = origin.MucDoKho,
                    MaBoDeGoc = origin.MaBoDe,
                    LaCongKhai = false,
                    SoLuongThe = origin.SoLuongThe,
                    DaXoa = false,
                    ThoiGianTao = DateTime.Now
                };

                _context.BoDeHocs.Add(newBoDe);
                await _context.SaveChangesAsync();

                await HandleHashtagsAsync(newBoDe.MaBoDe, newBoDe.MoTa);

                foreach (var oldCard in origin.TheFlashcards)
                {
                    var newCard = _mapper.Map<TheFlashcard>(oldCard);
                    newCard.MaThe = 0; // Reset PK để DB tự tăng
                    newCard.MaBoDe = newBoDe.MaBoDe;
                    newCard.ThoiGianTao = DateTime.Now;
                    newCard.SoLuongHoc = 0;
                    newCard.SoLanDung = 0;
                    newCard.SoLanSai = 0;

                    _context.TheFlashcards.Add(newCard);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<BoDeHocResponse>(newBoDe);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Tạo toàn bộ bộ đề cùng danh sách thẻ trong một lần gọi duy nhất
        /// </summary>
        public async Task<BoDeHocResponse> CreateFullAsync(LuuToanBoBoDeRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Lưu Bộ Đề (Header)
                var boDe = _mapper.Map<BoDeHoc>(request.ThongTinChung);
                boDe.ThoiGianTao = DateTime.Now;
                boDe.DaXoa = false;
                boDe.SoLuongThe = request.DanhSachThe.Count;

                _context.BoDeHocs.Add(boDe);
                await _context.SaveChangesAsync();

                if (boDe.MaChuDe > 0) // Kiểm tra nếu bộ đề có gắn mã chủ đề
                {
                    var chuDe = await _context.ChuDes.FindAsync(boDe.MaChuDe);
                    if (chuDe != null)
                    {
                        chuDe.SoLuotDung = (chuDe.SoLuotDung ?? 0) + 1;
                        // Không cần gọi SaveChanges ngay, cuối hàm gọi một lần là đủ
                    }
                }

                // 2. Xử lý Hashtag từ mô tả của bộ đề
                await HandleHashtagsAsync(boDe.MaBoDe, request.ThongTinChung.MoTa);

                // 3. Duyệt danh sách thẻ để lưu chi tiết
                foreach (var item in request.DanhSachThe)
                {
                    var theEntity = _mapper.Map<TheFlashcard>(item.TheChinh);
                    theEntity.MaBoDe = boDe.MaBoDe;
                    theEntity.ThoiGianTao = DateTime.Now;

                    _context.TheFlashcards.Add(theEntity);
                    await _context.SaveChangesAsync(); // Lưu để lấy MaThe (Identity) cho bảng con

                    // A. Trắc nghiệm
                    if (item.DapAnTracNghiem?.Any() == true)
                    {
                        var listDapAn = _mapper.Map<List<DapAnTracNghiem>>(item.DapAnTracNghiem);
                        listDapAn.ForEach(x => x.MaThe = theEntity.MaThe);
                        _context.DapAnTracNghiems.AddRange(listDapAn);
                    }

                    // B. Sắp xếp
                    if (item.PhanTuSapXeps?.Any() == true)
                    {
                        var listSapXep = _mapper.Map<List<PhanTuSapXep>>(item.PhanTuSapXeps);
                        listSapXep.ForEach(x => x.MaThe = theEntity.MaThe);
                        _context.PhanTuSapXeps.AddRange(listSapXep);
                    }

                    // C. Điền khuyết
                    if (item.TuDienKhuyets?.Any() == true)
                    {
                        var listDienKhuyet = _mapper.Map<List<TuDienKhuyet>>(item.TuDienKhuyets);
                        listDienKhuyet.ForEach(x => x.MaThe = theEntity.MaThe);
                        _context.TuDienKhuyets.AddRange(listDienKhuyet);
                    }

                    // D. Cặp ghép
                    if (item.CapGheps?.Any() == true)
                    {
                        var listCapGhep = _mapper.Map<List<CapGhep>>(item.CapGheps);
                        listCapGhep.ForEach(x => x.MaThe = theEntity.MaThe); // Đã sửa: Gán khóa ngoại MaThe
                        _context.CapGheps.AddRange(listCapGhep);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<BoDeHocResponse>(boDe);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Lỗi hệ thống khi lưu bộ đề: " + ex.Message);
            }
        }
        #endregion

        public async Task<IEnumerable<BoDeHocResponse>> GetPublicRandomAsync(int count)
        {
            var list = await _context.BoDeHocs
                .Include(x => x.TagBoDes).ThenInclude(t => t.MaTagNavigation)
                .Where(x => x.LaCongKhai == true && x.DaXoa != true)
                .OrderBy(x => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                .Take(count)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BoDeHocResponse>>(list);
        }

        public async Task<IEnumerable<BoDeHocResponse>> GetByTopicAsync(int topicId)
        {
            var list = await _context.BoDeHocs
                .Include(x => x.TagBoDes).ThenInclude(t => t.MaTagNavigation)
                .Where(x => x.MaChuDe == topicId && x.LaCongKhai == true && x.DaXoa != true)
                .OrderByDescending(x => x.ThoiGianTao)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BoDeHocResponse>>(list);
        }


        public async Task<IEnumerable<dynamic>> GetPopularTopicsAsync()
        {
            // Giả sử bạn có bảng ChuDes liên kết với BoDeHocs
            var popularTopics = await _context.BoDeHocs
                .Where(x => x.LaCongKhai == true && x.DaXoa != true)
                .GroupBy(x => new { x.MaChuDe, x.MaChuDeNavigation!.TenChuDe }) // Giả sử có Navigation Property
                .Select(g => new
                {
                    Id = g.Key.MaChuDe,
                    Name = g.Key.TenChuDe,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync<dynamic>();

            return popularTopics;
        }

        public async Task<HocBoDeResponse> GetFullDataToLearnAsync(int boDeId)
        {
            var boDe = await _context.BoDeHocs
                .Include(x => x.TheFlashcards).ThenInclude(f => f.DapAnTracNghiems)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.TuDienKhuyets)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.PhanTuSapXeps)
                .Include(x => x.TheFlashcards).ThenInclude(f => f.CapGheps)
                .FirstOrDefaultAsync(x => x.MaBoDe == boDeId && x.DaXoa != true);

            if (boDe == null) throw new Exception("Bộ đề không tồn tại.");

            // Khi gọi dòng này, AutoMapper sẽ dùng cấu hình "Complex Mapping" ở trên để xử lý
            return new HocBoDeResponse
            {
                ThongTinChung = _mapper.Map<BoDeHocResponse>(boDe),
                DanhSachCauHoi = _mapper.Map<List<ChiTietCauHoiHocResponse>>(boDe.TheFlashcards)
            };
        }

        public async Task<ThachDauResponse> CreateChallengeAsync(TaoThachDauRequest request)
        {
            // 1. Map TaoThachDauRequest -> ThachDau Entity
            var thachDau = _mapper.Map<ThachDau>(request);

            _context.ThachDaus.Add(thachDau);
            await _context.SaveChangesAsync(); // Lưu để lấy MaThachDau tự tăng

            // 2. Tự động thêm người tạo vào danh sách người chơi (ThachDauNguoiChoi)
            var creator = new ThachDauNguoiChoi
            {
                MaThachDau = thachDau.MaThachDau,
                MaNguoiDung = request.NguoiTao // Lấy từ UserSession gửi xuống
            };
            _context.ThachDauNguoiChois.Add(creator);
            await _context.SaveChangesAsync();

            return _mapper.Map<ThachDauResponse>(thachDau);
        }

        public async Task<bool> UpdateChallengeResultAsync(CapNhatKetQuaThachDauRequest request)
        {
            // 1. Tìm bản ghi người chơi cụ thể trong phòng
            var participant = await _context.ThachDauNguoiChois
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau && x.MaNguoiDung == request.MaNguoiDung);

            if (participant == null) return false;

            // 2. Map kết quả thi vào Entity
            _mapper.Map(request, participant);
            await _context.SaveChangesAsync();

            // 3. Logic phân định thắng thua (Xử lý khi có từ 2 người nộp bài)
            await DetermineWinnerAsync(request.MaThachDau);
            return true;
        }

        private async Task DetermineWinnerAsync(int maThachDau)
        {
            var players = await _context.ThachDauNguoiChois
                .Where(x => x.MaThachDau == maThachDau && x.Diem != null) // Chỉ lấy người đã nộp bài
                .ToListAsync();

            if (players.Count >= 2)
            {
                // Sắp xếp: Điểm cao thắng -> Nếu bằng điểm, thời gian ngắn thắng
                var winner = players
                    .OrderByDescending(p => p.Diem)
                    .ThenBy(p => p.ThoiGianLamBaiGiay)
                    .First();

                foreach (var p in players)
                {
                    p.LaNguoiThang = (p == winner); // Gán cờ thắng/thua
                }

                // Cập nhật trạng thái phòng thành "DaKetThuc"
                var challenge = await _context.ThachDaus.FindAsync(maThachDau);
                if (challenge != null) challenge.TrangThai = "DaKetThuc";

                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> JoinChallengeAsync(ThamGiaThachDauRequest request)
        {
            // 1. Kiểm tra sự tồn tại của trận thách đấu và trạng thái phòng
            var challenge = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau);

            // Kiểm tra phòng có tồn tại không và có đang ở trạng thái chờ người chơi không
            if (challenge == null || challenge.TrangThai != TrangThaiThachDauEnum.ChoNguoiChoi.ToString())
            {
                return false;
            }

            // 2. Kiểm tra xem người dùng đã có trong phòng chưa để tránh trùng lặp
            var isAlreadyJoined = challenge.ThachDauNguoiChois
                .Any(x => x.MaNguoiDung == request.MaNguoiDung);

            if (isAlreadyJoined) return true; // Nếu đã tham gia rồi thì trả về true để tiếp tục vào học

            // 3. Sử dụng AutoMapper để ánh xạ từ Request sang Entity ThachDauNguoiChoi
            // Profile đã cấu hình sẽ bỏ qua (Ignore) các trường Diem, ThoiGian... vì người chơi mới tham gia
            var participant = _mapper.Map<ThachDauNguoiChoi>(request);

            _context.ThachDauNguoiChois.Add(participant);

            // Lưu thay đổi vào Database
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateCardProgressAsync(CapNhatTienDoHocTapRequest request)
        {
            // 1. Tìm bản ghi tiến độ
            var progress = await _context.TienDoHocTaps
                .FirstOrDefaultAsync(x => (request.MaTienDo.HasValue && x.MaTienDo == request.MaTienDo)
                                       || (x.MaThe == request.MaThe && x.MaNguoiDung == request.MaNguoiDung));

            // 2. Nếu chưa có, khởi tạo (Dùng 2.5 thay vì 2.5m)
            if (progress == null)
            {
                progress = new TienDoHocTap
                {
                    MaThe = request.MaThe,
                    MaNguoiDung = request.MaNguoiDung,
                    HeSoDe = 2.5, // Dùng double (không có chữ m)
                    SoLanLap = 0,
                    KhoangCachNgay = 0,
                    ThoiGianTao = DateTime.Now
                };
                _context.TienDoHocTaps.Add(progress);
            }

            // 3. Lấy chất lượng phản xạ q (0-5)
            int q = (int)(request.TrangThai ?? TrangThaiHocEnum.New);

            // 4. Thuật toán SM-2 tính Khoảng cách (Interval)
            if (q >= 3) // Trả lời đúng
            {
                if (progress.SoLanLap == 0)
                    progress.KhoangCachNgay = 1;
                else if (progress.SoLanLap == 1)
                    progress.KhoangCachNgay = 6;
                else
                {
                    // Mọi thứ ở đây đều là double nên không cần ép kiểu phức tạp
                    double interval = (progress.KhoangCachNgay ?? 1) * (progress.HeSoDe ?? 2.5);
                    progress.KhoangCachNgay = (int)Math.Round(interval);
                }

                progress.SoLanLap++;
                progress.SoLanDung = (progress.SoLanDung ?? 0) + 1;
            }
            else // Trả lời sai (q < 3)
            {
                progress.SoLanLap = 0;
                progress.KhoangCachNgay = 1;
                progress.SoLanSai = (progress.SoLanSai ?? 0) + 1;
            }

            // 5. Cập nhật Hệ số dễ (Ease Factor)
            // Công thức: $EF' = EF + (0.1 - (5 - q) * (0.08 + (5 - q) * 0.02))$
            double currentEF = progress.HeSoDe ?? 2.5;
            double newEF = currentEF + (0.1 - (5 - q) * (0.08 + (5 - q) * 0.02));

            // Đảm bảo EF không thấp hơn 1.3
            progress.HeSoDe = Math.Max(1.3, newEF);

            // 6. Cập nhật các thông số thời gian
            progress.NgayOnTapTiepTheo = DateTime.Now.AddDays(progress.KhoangCachNgay ?? 1);
            progress.LanHocCuoi = DateTime.Now;
            progress.TrangThai = (byte)q;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task TangSoLuotHocAsync(int maBoDe)
        {
            var boDe = await _context.BoDeHocs.FindAsync(maBoDe);
            if (boDe != null)
            {
                boDe.SoLuotHoc = (boDe.SoLuotHoc ?? 0) + 1;
                await _context.SaveChangesAsync();
            }
        }
        public async Task LuuKetQuaPhienHocAsync(PhienHoc phienHoc)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // ... (Giữ nguyên logic lưu PhienHoc và LichSuHocBoDe của bạn)
                _context.PhienHocs.Add(phienHoc);
                await _context.SaveChangesAsync();

                if (phienHoc.MaBoDe == null) throw new Exception("MaBoDe null");

                var boDe = await _context.BoDeHocs.FindAsync(phienHoc.MaBoDe);
                if (boDe != null)
                {
                    boDe.SoLuotHoc = (boDe.SoLuotHoc ?? 0) + 1;
                }

                var lichSu = new LichSuHocBoDe
                {
                    // ... các field cũ của bạn
                    MaBoDe = phienHoc.MaBoDe.Value,
                    MaPhien = phienHoc.MaPhien
                };
                _context.LichSuHocBoDes.Add(lichSu);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}