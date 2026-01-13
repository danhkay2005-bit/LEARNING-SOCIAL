using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
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
    }
}