using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Responses.Learn;
using StudyApp.BLL.Interfaces.Learn;

namespace StudyApp.BLL.Services.Learn
{
    public class TagService : ITagService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public TagService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            var tags = await _context.Tags
                .OrderByDescending(x => x.SoLuotDung) // Ưu tiên các tag dùng nhiều
                .ToListAsync();

            return _mapper.Map<IEnumerable<TagResponse>>(tags);
        }

        public async Task<bool> GanTagsChoBoDeAsync(int maBoDe, List<string> danhSachTenTag)
        {
            // 1. Xóa các liên kết TagBoDe cũ
            var oldLinks = await _context.TagBoDes.Where(x => x.MaBoDe == maBoDe).ToListAsync();
            _context.TagBoDes.RemoveRange(oldLinks);
            await _context.SaveChangesAsync();

            foreach (var name in danhSachTenTag.Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)))
            {
                // 2. Find or Create Tag
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TenTag.ToLower() == name.ToLower());
                if (tag == null)
                {
                    tag = new Tag { TenTag = name, ThoiGianTao = DateTime.Now, SoLuotDung = 1 };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    tag.SoLuotDung = (tag.SoLuotDung ?? 0) + 1;
                }

                // 3. Link mới
                _context.TagBoDes.Add(new TagBoDe { MaBoDe = maBoDe, MaTag = tag.MaTag, ThoiGianThem = DateTime.Now });
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TagSelectResponse>> SearchTagsAsync(string keyword)
        {
            var tags = await _context.Tags
                .Where(t => t.TenTag.Contains(keyword))
                .Take(10)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TagSelectResponse>>(tags);
        }
    }
}