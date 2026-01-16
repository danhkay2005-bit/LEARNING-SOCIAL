using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface ITheoDoiNguoiDungService
    {
       
        /// Theo dõi người dùng
        
        Task<TheoDoiNguoiDungResponse> TheoDoiAsync(TheoDoiNguoiDungRequest request);

        /// Bỏ theo dõi người dùng
     
        Task<bool> BoTheoDoiAsync(BoTheoDoiNguoiDungRequest request);

      
        /// Kiểm tra trạng thái theo dõi
     
        Task<TrangThaiTheoDoiResponse> KiemTraTheoDoiAsync(KiemTraTheoDoiRequest request);

       
        /// Lấy danh sách người theo dõi (followers)
       
        Task<IEnumerable<NguoiTheoDoiResponse>> LayDanhSachNguoiTheoDoiAsync(Guid maNguoiDung, Guid? nguoiXemId = null);

        
        /// Lấy danh sách người đang theo dõi (following)
       
        Task<IEnumerable<NguoiDangTheoDoiResponse>> LayDanhSachDangTheoDoiAsync(Guid maNguoiDung, Guid? nguoiXemId = null);

        
        /// Lấy thống kê theo dõi
       
        Task<ThongKeTheoDoiResponse> LayThongKeTheoDoiAsync(Guid maNguoiDung);

        
        /// Lấy gợi ý người dùng để theo dõi
        
        Task<IEnumerable<GoiYTheoDoiResponse>> LayGoiYTheoDoiAsync(Guid maNguoiDung, int soLuong = 10);

        
        /// Lấy danh sách bạn chung (mutual friends)
      
        Task<IEnumerable<NguoiTheoDoiResponse>> LayDanhSachBanChungAsync(Guid maNguoiDung1, Guid maNguoiDung2);
    }
}