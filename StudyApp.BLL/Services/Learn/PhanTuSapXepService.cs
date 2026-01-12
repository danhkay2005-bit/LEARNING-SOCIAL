using AutoMapper;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;    

namespace StudyApp.BLL.Services.Learn
{
    public class PhanTuSapXepService : IPhanTuSapXepService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public PhanTuSapXepService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> SyncElementsAsync(int maThe, List<TaoPhanTuSapXepRequest> requests)
        {
            var oldItems = await _context.PhanTuSapXeps.Where(x => x.MaThe == maThe).ToListAsync();
            _context.PhanTuSapXeps.RemoveRange(oldItems);

            var newItems = _mapper.Map<List<PhanTuSapXep>>(requests);
            newItems.ForEach(x => x.MaThe = maThe);

            _context.PhanTuSapXeps.AddRange(newItems);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
