using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IChiaSeBaiDangService
    {
        /// Chia sẻ bài đăng - tạo bài đăng mới và ghi lại lịch sử chia sẻ
     
        Task<ChiaSeBaiDangResponse> ChiaSeBaiDangAsync(ChiaSeBaiDangRequest request);
        /// Lấy danh sách chia sẻ của một bài đăng
        Task<IEnumerable<ChiaSeBaiDangResponse>> GetDanhSachChiaSeByBaiDangAsync(int maBaiDang);


        /// Lấy danh sách bài đăng người dùng đã chia sẻ
        Task<IEnumerable<ChiaSeBaiDangResponse>> GetDanhSachChiaSeByNguoiDungAsync(Guid maNguoiDung);


        /// Lấy thống kê chia sẻ của bài đăng
        Task<ThongKeChiaSeBaiDangResponse> GetThongKeChiaSeAsync(int maBaiDang);
      
        /// Xóa chia sẻ (xóa bài đăng mới và record chia sẻ)
        Task<bool> XoaChiaSeAsync(int maChiaSe, Guid maNguoiDung);

        
        /// Kiểm tra người dùng đã chia sẻ bài đăng chưa
        Task<bool> DaChiaSeAsync(int maBaiDang, Guid maNguoiDung);
    }
}