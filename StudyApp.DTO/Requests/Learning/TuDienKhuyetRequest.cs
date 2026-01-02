using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo từ điền khuyết
    public class TaoTuDienKhuyetRequest
    {
        [Required(ErrorMessage = "Từ cần điền là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Từ cần điền không được vượt quá 200 ký tự")]
        public string TuCanDien { get; set; } = null!;

        [Required(ErrorMessage = "Vị trí trong câu là bắt buộc")]
        [Range(0, 1000, ErrorMessage = "Vị trí không hợp lệ")]
        public int ViTriTrongCau { get; set; }

        [MaxLength(200, ErrorMessage = "Gợi ý không được vượt quá 200 ký tự")]
        public string? GoiY { get; set; }
    }

    // Request cập nhật từ điền khuyết
    public class CapNhatTuDienKhuyetRequest
    {
        [Required(ErrorMessage = "Mã từ điền khuyết là bắt buộc")]
        public int MaTuDienKhuyet { get; set; }

        [MaxLength(200, ErrorMessage = "Từ cần điền không được vượt quá 200 ký tự")]
        public string? TuCanDien { get; set; }

        [Range(0, 1000, ErrorMessage = "Vị trí không hợp lệ")]
        public int? ViTriTrongCau { get; set; }

        [MaxLength(200, ErrorMessage = "Gợi ý không được vượt quá 200 ký tự")]
        public string? GoiY { get; set; }
    }
}