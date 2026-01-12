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
    public class TuDienKhuyetService : ITuDienKhuyetService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public TuDienKhuyetService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> SyncBlanksAsync(int maThe, List<TaoTuDienKhuyetRequest> requests)
        {
            var oldItems = await _context.TuDienKhuyets.Where(x => x.MaThe == maThe).ToListAsync();
            _context.TuDienKhuyets.RemoveRange(oldItems);

            var newItems = _mapper.Map<List<TuDienKhuyet>>(requests);
            newItems.ForEach(x => x.MaThe = maThe);

            _context.TuDienKhuyets.AddRange(newItems);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
