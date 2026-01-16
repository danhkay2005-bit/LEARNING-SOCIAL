using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IChiaSeBaiDangService
    {
        /// <summary>
        /// Chia sẻ bài đăng
        /// </summary>
        Task<ChiaSeBaiDangResponse> ChiaSeBaiDangAsync(ChiaSeBaiDangRequest request);

        /// <summary>
        /// Lấy danh sách chia sẻ của một bài đăng
        /// </summary>
        Task<List<ChiaSeBaiDangResponse>> LayDanhSachChiaSeTheoMaBaiDangAsync(int maBaiDang);

        /// <summary>
        /// Lấy thống kê chia sẻ của bài đăng
        /// </summary>
        Task<ThongKeChiaSeBaiDangResponse> LayThongKeChiaSeAsync(int maBaiDang);

        /// <summary>
        /// Lấy chi tiết một chia sẻ
        /// </summary>
        Task<ChiaSeBaiDangResponse?> LayChiTietChiaSeAsync(int maChiaSe);

        /// <summary>
        /// Hủy chia sẻ bài đăng
        /// </summary>
        Task<bool> HuyChiaSeAsync(int maChiaSe, Guid maNguoiDung);

        /// <summary>
        /// Lấy danh sách bài đăng đã chia sẻ của người dùng
        /// </summary>
        Task<List<ChiaSeBaiDangResponse>> LayDanhSachChiaSeTheoNguoiDungAsync(Guid maNguoiDung, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// Kiểm tra người dùng đã chia sẻ bài đăng chưa
        /// </summary>
        Task<bool> KiemTraDaChiaSeAsync(int maBaiDang, Guid maNguoiDung);
        /// <summary>
        /// lấy danh sach chia sẻ theo ngu7oiwf dùng
        
        Task<List<ChiaSeBaiDangResponse>> LayDanhSachBaiDangDaChiaSeAsync( Guid maNguoiDung,int  skip, int take);
    }
}