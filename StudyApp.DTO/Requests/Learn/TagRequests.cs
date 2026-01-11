using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    // Tạo tag thủ công (admin)
    public class TaoTagRequest
    {
        [Required]
        [MaxLength(100)]
        public string TenTag { get; set; } = null!;
    }

    // Cập nhật tag
    public class CapNhatTagRequest
    {
        [Required]
        public int MaTag { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenTag { get; set; } = null!;
    }

    // Gợi ý tag khi gõ #
    public class GoiYTagRequest
    {
        [Required]
        public string Keyword { get; set; } = null!;
    }

    // Xử lý hashtag từ nội dung người dùng nhập
    public class XuLyTagTuNoiDungRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        [Required]
        public string NoiDung { get; set; } = null!;
    }
}
