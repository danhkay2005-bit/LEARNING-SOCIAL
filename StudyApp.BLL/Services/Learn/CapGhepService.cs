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
    public class CapGhepService : ICapGhepService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public CapGhepService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> SyncPairsAsync(int maThe, List<CapGhepRequest> requests)
        {
            var oldItems = await _context.CapGheps.Where(x => x.MaThe == maThe).ToListAsync();
            _context.CapGheps.RemoveRange(oldItems);

            var newItems = _mapper.Map<List<CapGhep>>(requests);
            newItems.ForEach(x => x.MaThe = maThe);

            _context.CapGheps.AddRange(newItems);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
