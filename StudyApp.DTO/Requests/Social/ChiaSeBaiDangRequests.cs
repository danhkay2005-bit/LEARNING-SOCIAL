using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request chia sẻ bài đăng
    /// </summary>
    public class ChiaSeBaiDangRequest
    {
        /// <summary>
        /// Bài đăng gốc được chia sẻ
        /// </summary>
        public int MaBaiDangGoc { get; set; }

        public Guid MaNguoiChiaSe { get; set; }

        /// <summary>
        /// Nội dung thêm khi chia sẻ (optional)
        /// </summary>
        public string? NoiDungThem { get; set; }
    }
}
