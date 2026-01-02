using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request thêm bộ đề yêu thích
    public class ThemBoDeYeuThichRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [MaxLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? GhiChu { get; set; }
    }

    // Request xóa bộ đề yêu thích
    public class XoaBoDeYeuThichRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }
    }

    // Request lấy danh sách bộ đề yêu thích
    public class LayBoDeYeuThichRequest
    {
        public string? TuKhoa { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}