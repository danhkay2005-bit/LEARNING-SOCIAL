using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Services.Learn
{
    public class ChuDeService : IChuDeService
    {
        private readonly LearningDbContext _context; // Thay bằng DbContext của bạn
        private readonly IMapper _mapper;

        public ChuDeService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChuDeResponse>> GetAllAsync()
        {
            var list = await _context.ChuDes.OrderBy(x => x.TenChuDe).ToListAsync();
            return _mapper.Map<IEnumerable<ChuDeResponse>>(list);
        }

        public async Task<ChuDeResponse?> GetByIdAsync(int id)
        {
            var item = await _context.ChuDes.FindAsync(id);
            return _mapper.Map<ChuDeResponse>(item);
        }

        public async Task<ChuDeResponse> CreateAsync(TaoChuDeRequest request)
        {
            var item = _mapper.Map<ChuDe>(request);
            _context.ChuDes.Add(item);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChuDeResponse>(item);
        }

        public async Task<ChuDeResponse> UpdateAsync(CapNhatChuDeRequest request)
        {
            var existing = await _context.ChuDes.FindAsync(request.MaChuDe);
            if (existing == null) throw new Exception("Không tìm thấy chủ đề.");

            _mapper.Map(request, existing);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChuDeResponse>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.ChuDes.FindAsync(id);
            if (item == null) return false;

            _context.ChuDes.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}