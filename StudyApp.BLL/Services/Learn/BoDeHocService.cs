using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

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
                .Where(x => x.MaNguoiDung == userId && x.DaXoa != true)
                .OrderByDescending(x => x.ThoiGianTao)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BoDeHocResponse>>(list);
        }

        public async Task<BoDeHocResponse> CreateAsync(TaoBoDeHocRequest request)
        {
            var boDe = _mapper.Map<BoDeHoc>(request);

            // Các trường mặc định đã được cấu hình trong MappingProfile (DaXoa, ThoiGianTao)
            _context.BoDeHocs.Add(boDe);
            await _context.SaveChangesAsync();

            return _mapper.Map<BoDeHocResponse>(boDe);
        }

        public async Task<BoDeHocResponse> UpdateAsync(int id, CapNhatBoDeHocRequest request)
        {
            var existing = await _context.BoDeHocs.FindAsync(id);
            if (existing == null || existing.DaXoa == true)
                throw new KeyNotFoundException("Bộ đề không tồn tại.");

            _mapper.Map(request, existing);
            existing.ThoiGianCapNhat = DateTime.Now;

            _context.BoDeHocs.Update(existing);
            await _context.SaveChangesAsync();

            return _mapper.Map<BoDeHocResponse>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.BoDeHocs.FindAsync(id);
            if (existing == null) return false;

            // Soft Delete (Xóa mềm)
            existing.DaXoa = true;
            existing.ThoiGianCapNhat = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Logic Fork (Sao chép sâu): Nhân bản Bộ đề + Toàn bộ Flashcards + Dữ liệu liên quan
        /// </summary>
        public async Task<BoDeHocResponse> ForkAsync(SaoChepBoDeHocRequest request)
        {
            // 1. Lấy bộ đề gốc kèm tất cả "họ hàng"
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
                // 2. Tạo Bộ đề mới (Bản sao)
                var newBoDe = new BoDeHoc
                {
                    MaNguoiDung = request.MaNguoiDungMoi,
                    MaChuDe = origin.MaChuDe,
                    TieuDe = $"[Bản sao] {origin.TieuDe}",
                    MoTa = origin.MoTa,
                    AnhBia = origin.AnhBia,
                    MucDoKho = origin.MucDoKho,
                    MaBoDeGoc = origin.MaBoDe,
                    LaCongKhai = false, // Luôn để riêng tư khi mới fork
                    SoLuongThe = origin.SoLuongThe,
                    DaXoa = false,
                    ThoiGianTao = DateTime.Now
                };

                _context.BoDeHocs.Add(newBoDe);
                await _context.SaveChangesAsync(); // Lấy MaBoDe mới

                // 3. Sao chép danh sách thẻ
                foreach (var oldCard in origin.TheFlashcards)
                {
                    var newCard = _mapper.Map<TheFlashcard>(oldCard);
                    newCard.MaThe = 0; // Reset PK để DB tự tăng
                    newCard.MaBoDe = newBoDe.MaBoDe; // Gán vào bộ đề mới
                    newCard.ThoiGianTao = DateTime.Now;

                    // Reset các thống kê
                    newCard.SoLuongHoc = 0;
                    newCard.SoLanDung = 0;
                    newCard.SoLanSai = 0;

                    _context.TheFlashcards.Add(newCard);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<BoDeHocResponse>(newBoDe);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}