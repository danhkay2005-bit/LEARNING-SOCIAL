using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin chia sẻ bài đăng
    /// </summary>
    public class ChiaSeBaiDangResponse
    {
        public int MaChiaSe { get; set; }

        public int MaBaiDangGoc { get; set; }
        public Guid MaNguoiChiaSe { get; set; }

        /// <summary>
        /// Bài đăng mới được tạo từ hành động chia sẻ
        /// </summary>
        public int? MaBaiDangMoi { get; set; }

        public string? NoiDungThem { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
