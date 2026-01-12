using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Services.Learn
{
    public class TheFlashcardService : ITheFlashcardService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public TheFlashcardService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TheFlashcardResponse> GetByIdAsync(int id)
        {
            var card = await _context.TheFlashcards
                .Include(x => x.DapAnTracNghiems)
                .Include(x => x.CapGheps)
                .Include(x => x.PhanTuSapXeps)
                .Include(x => x.TuDienKhuyets)
                .FirstOrDefaultAsync(x => x.MaThe == id);

            return _mapper.Map<TheFlashcardResponse>(card);
        }

        public async Task<IEnumerable<TheFlashcardResponse>> GetByBoDeAsync(int maBoDe)
        {
            var cards = await _context.TheFlashcards
                .Where(x => x.MaBoDe == maBoDe)
                .OrderBy(x => x.ThuTu)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TheFlashcardResponse>>(cards);
        }

        public async Task<TheFlashcardResponse> CreateAsync(TaoTheFlashcardRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Map và tạo thẻ chính
                var card = _mapper.Map<TheFlashcard>(request);
                _context.TheFlashcards.Add(card);
                await _context.SaveChangesAsync();

                // 2. Cập nhật số lượng thẻ trong Bộ đề học
                var boDe = await _context.BoDeHocs.FindAsync(request.MaBoDe);
                if (boDe != null)
                {
                    boDe.SoLuongThe = (boDe.SoLuongThe ?? 0) + 1;
                    boDe.ThoiGianCapNhat = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<TheFlashcardResponse>(card);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TheFlashcardResponse> UpdateAsync(CapNhatTheFlashcardRequest request)
        {
            var existingCard = await _context.TheFlashcards.FindAsync(request.MaThe);
            if (existingCard == null) throw new KeyNotFoundException("Không tìm thấy thẻ học.");

            _mapper.Map(request, existingCard);
            _context.TheFlashcards.Update(existingCard);

            await _context.SaveChangesAsync();
            return _mapper.Map<TheFlashcardResponse>(existingCard);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var card = await _context.TheFlashcards.FindAsync(id);
            if (card == null) return false;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Giảm số lượng thẻ trong Bộ đề
                var boDe = await _context.BoDeHocs.FindAsync(card.MaBoDe);
                if (boDe != null)
                {
                    boDe.SoLuongThe = Math.Max(0, (boDe.SoLuongThe ?? 1) - 1);
                }

                // 2. Xóa thẻ (Cascading sẽ tự xóa các bảng con nếu bạn cấu hình trong DB)
                _context.TheFlashcards.Remove(card);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}