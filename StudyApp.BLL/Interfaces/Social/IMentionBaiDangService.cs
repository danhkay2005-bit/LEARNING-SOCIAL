using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IMentionBaiDangService
    {
        /// <summary>
        /// Lấy danh sách mention trong bài đăng
        /// </summary>
        Task<IEnumerable<MentionBaiDangResponse>> GetDanhSachMentionByBaiDangAsync(int maBaiDang);

        /// <summary>
        /// Lấy danh sách bài đăng có mention người dùng
        /// </summary>
        Task<IEnumerable<MentionBaiDangResponse>> GetDanhSachBaiDangDuocMentionAsync(Guid maNguoiDung);

        /// <summary>
        /// Thêm mention vào bài đăng
        /// </summary>
        Task<MentionBaiDangResponse> ThemMentionAsync(int maBaiDang, Guid maNguoiDuocMention);

        /// <summary>
        /// Xóa mention khỏi bài đăng
        /// </summary>
        Task<bool> XoaMentionAsync(int maBaiDang, Guid maNguoiDuocMention);

        /// <summary>
        /// Kiểm tra người dùng có được mention trong bài đăng không
        /// </summary>
        Task<bool> KiemTraDuocMentionAsync(int maBaiDang, Guid maNguoiDung);
    }
}