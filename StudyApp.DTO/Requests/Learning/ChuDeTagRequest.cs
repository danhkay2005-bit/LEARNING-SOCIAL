using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tìm kiếm chủ đề
    public class TimKiemChuDeRequest
    {
        public string? TuKhoa { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    // Request tìm kiếm tag
    public class TimKiemTagRequest
    {
        public string? TuKhoa { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    // Request thêm tag vào bộ đề
    public class ThemTagBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Tên tag là bắt buộc")]
        [MaxLength(50, ErrorMessage = "Tên tag không được vượt quá 50 ký tự")]
        public string TenTag { get; set; } = null!;
    }

    // Request xóa tag khỏi bộ đề
    public class XoaTagBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Mã tag là bắt buộc")]
        public int MaTag { get; set; }
    }
}