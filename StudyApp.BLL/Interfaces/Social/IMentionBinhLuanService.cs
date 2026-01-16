using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IMentionBinhLuanService
    {
        /// <summary>
        /// Lấy danh sách mention trong bình luận
        /// </summary>
        Task<IEnumerable<MentionBinhLuanResponse>> GetDanhSachMentionByBinhLuanAsync(int maBinhLuan);

        /// <summary>
        /// Lấy danh sách bình luận có mention người dùng
        /// </summary>
        Task<IEnumerable<MentionBinhLuanResponse>> GetDanhSachBinhLuanDuocMentionAsync(Guid maNguoiDung);

        /// <summary>
        /// Thêm mention vào bình luận
        /// </summary>
        Task<MentionBinhLuanResponse> ThemMentionAsync(int maBinhLuan, Guid maNguoiDuocMention);

        /// <summary>
        /// Xóa mention khỏi bình luận
        /// </summary>
        Task<bool> XoaMentionAsync(int maBinhLuan, Guid maNguoiDuocMention);

        /// <summary>
        /// Kiểm tra người dùng có được mention trong bình luận không
        /// </summary>
        Task<bool> KiemTraDuocMentionAsync(int maBinhLuan, Guid maNguoiDung);

        /// <summary>
        /// Lấy danh sách mention trong nhiều bình luận (bulk)
        /// </summary>
        Task<Dictionary<int, IEnumerable<MentionBinhLuanResponse>>> GetDanhSachMentionByBinhLuansAsync(List<int> maBinhLuans);
    }
}