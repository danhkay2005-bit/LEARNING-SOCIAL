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

        public BoDeHocService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Helper Methods (Xử lý Hashtag)
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

        public async Task<BoDeHocResponse> UpdateAsync(int id, CapNhatBoDeHocRequest request)
        {
            var existing = await _context.BoDeHocs.FindAsync(id);
            if (existing == null || existing.DaXoa == true)
                throw new KeyNotFoundException("Bộ đề không tồn tại.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _mapper.Map(request, existing);
                existing.ThoiGianCapNhat = DateTime.Now;

                _context.BoDeHocs.Update(existing);
                await _context.SaveChangesAsync();

                await HandleHashtagsAsync(id, request.MoTa);

                await transaction.CommitAsync();
                return _mapper.Map<BoDeHocResponse>(existing);
            }
            catch
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
    }
}