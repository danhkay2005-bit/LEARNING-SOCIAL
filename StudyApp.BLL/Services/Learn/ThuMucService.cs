using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Services.Learn
{
    public class ThuMucService : IThuMucService
    {
        private readonly LearningDbContext _context; // Thay bằng tên DbContext của bạn
        private readonly IMapper _mapper;

        public ThuMucService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy danh sách phẳng (Flat list) theo User
        public async Task<IEnumerable<ThuMucResponse>> GetByUserAsync(Guid userId)
        {
            var folders = await _context.ThuMucs
                .Where(x => x.MaNguoiDung == userId)
                .OrderBy(x => x.ThuTu)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ThuMucResponse>>(folders);
        }

        // Lấy danh sách dạng cây (Tree structure)
        public async Task<IEnumerable<ThuMucTreeResponse>> GetTreeByUserAsync(Guid userId)
        {
            // Load toàn bộ thư mục của user vào bộ nhớ (để AutoMapper map đệ quy dễ hơn)
            var allFolders = await _context.ThuMucs
                .Include(x => x.InverseMaThuMucChaNavigation)
                .Where(x => x.MaNguoiDung == userId)
                .ToListAsync();

            // Lọc ra các thư mục gốc (không có cha)
            var rootFolders = allFolders
                .Where(x => x.MaThuMucCha == null)
                .OrderBy(x => x.ThuTu);

            return _mapper.Map<IEnumerable<ThuMucTreeResponse>>(rootFolders);
        }

        // Tạo thư mục mới
        public async Task<ThuMucResponse> CreateAsync(TaoThuMucRequest request)
        {
            var folder = _mapper.Map<ThuMuc>(request);

            _context.ThuMucs.Add(folder);
            await _context.SaveChangesAsync();

            return _mapper.Map<ThuMucResponse>(folder);
        }

        // Cập nhật thư mục - ĐÂY LÀ PHẦN BỊ THIẾU CỦA BẠN
        public async Task<ThuMucResponse> UpdateAsync(CapNhatThuMucRequest request)
        {
            var existingFolder = await _context.ThuMucs
                .FirstOrDefaultAsync(x => x.MaThuMuc == request.MaThuMuc);

            if (existingFolder == null)
                throw new KeyNotFoundException("Không tìm thấy thư mục để cập nhật.");

            // Sử dụng AutoMapper để map dữ liệu thay đổi vào Entity hiện có
            _mapper.Map(request, existingFolder);

            // ThoiGianCapNhat sẽ được tự động set trong MappingProfile bạn đã viết
            _context.ThuMucs.Update(existingFolder);
            await _context.SaveChangesAsync();

            return _mapper.Map<ThuMucResponse>(existingFolder);
        }

        // Xóa thư mục
        public async Task<bool> DeleteAsync(int id)
        {
            var folder = await _context.ThuMucs.FindAsync(id);
            if (folder == null) return false;

            // Lưu ý: Nếu có ràng buộc khóa ngoại, EF sẽ báo lỗi nếu còn Bộ đề bên trong.
            // Bạn có thể xử lý xóa các bộ đề hoặc set null MaThuMuc của bộ đề trước khi xóa.
            _context.ThuMucs.Remove(folder);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}