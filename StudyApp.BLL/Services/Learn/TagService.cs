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

        public async Task<bool> MergeTagsAsync(int tagSaiId, string tenTagChuan)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Tìm hoặc tạo Tag chuẩn (ví dụ: "C#")
                var tagChuan = await _context.Tags.FirstOrDefaultAsync(t => t.TenTag == tenTagChuan)
                               ?? new Tag { TenTag = tenTagChuan };

                if (tagChuan.MaTag == 0)
                {
                    _context.Tags.Add(tagChuan);
                    await _context.SaveChangesAsync();
                }

                // 2. Chuyển hướng toàn bộ bộ đề từ Tag sai sang Tag chuẩn
                var linkings = await _context.TagBoDes.Where(x => x.MaTag == tagSaiId).ToListAsync();
                foreach (var link in linkings)
                {
                    // Kiểm tra tránh trùng lặp nếu bộ đề đã có cả 2 tag
                    bool exists = await _context.TagBoDes.AnyAsync(x => x.MaBoDe == link.MaBoDe && x.MaTag == tagChuan.MaTag);
                    if (!exists) link.MaTag = tagChuan.MaTag;
                    else _context.TagBoDes.Remove(link);
                }

                // 3. Xóa Tag sai
                var tagSai = await _context.Tags.FindAsync(tagSaiId);
                if (tagSai != null) _context.Tags.Remove(tagSai);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteTagAsync(int tagId)
        {
            // 1. Sử dụng Transaction để đảm bảo an toàn dữ liệu
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tag = await _context.Tags.FindAsync(tagId);
                if (tag == null) return false;

                // 2. Xóa tất cả các liên kết của Tag này với các Bộ đề trong bảng trung gian
                var links = _context.TagBoDes.Where(tb => tb.MaTag == tagId);
                _context.TagBoDes.RemoveRange(links);

                // 3. Xóa chính nó khỏi bảng Tags
                _context.Tags.Remove(tag);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch 
            {
                await transaction.RollbackAsync();
                // Bạn có thể log lỗi ex ở đây
                return false;
            }
        }

        public async Task<bool> UpdateTagNameAsync(int tagId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) return false;

            var tag = await _context.Tags.FindAsync(tagId);
            if (tag == null) return false;

            // Kiểm tra xem tên mới có bị trùng với Tag khác không (tránh Duplicate Key)
            bool isExist = await _context.Tags.AnyAsync(t => t.TenTag.ToLower() == newName.ToLower() && t.MaTag != tagId);
            if (isExist)
            {
                // Nếu trùng, Admin nên dùng tính năng MergeTags thay vì Update
                throw new Exception("Tên Tag này đã tồn tại trong hệ thống.");
            }

            tag.TenTag = newName.Trim();
            // Bạn có thể cập nhật thêm trường ThoiGianCapNhat nếu có

            _context.Tags.Update(tag);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}