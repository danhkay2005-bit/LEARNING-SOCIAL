using AutoMapper;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StudyApp.BLL.Services.Learn
{
    public class DapAnTracNghiemService : IDapAnTracNghiemService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public DapAnTracNghiemService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> SyncAnswersAsync(int maThe, List<TaoDapAnTracNghiemRequest> requests)
        {
            // Xóa cũ thêm mới (Sync logic)
            var oldItems = await _context.DapAnTracNghiems.Where(x => x.MaThe == maThe).ToListAsync();
            _context.DapAnTracNghiems.RemoveRange(oldItems);

            var newItems = _mapper.Map<List<DapAnTracNghiem>>(requests);
            newItems.ForEach(x => x.MaThe = maThe);

            _context.DapAnTracNghiems.AddRange(newItems);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DapAnTracNghiemResponse>> GetByMaTheAsync(int maThe)
        {
            var items = await _context.DapAnTracNghiems.Where(x => x.MaThe == maThe).ToListAsync();
            return _mapper.Map<IEnumerable<DapAnTracNghiemResponse>>(items);
        }
    }
}
