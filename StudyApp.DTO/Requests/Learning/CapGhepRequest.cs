using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo cặp ghép
    public class TaoCapGhepRequest
    {
        [Required(ErrorMessage = "Vế trái là bắt buộc")]
        [MaxLength(500, ErrorMessage = "Vế trái không được vượt quá 500 ký tự")]
        public string VeTrai { get; set; } = null!;

        [Required(ErrorMessage = "Vế phải là bắt buộc")]
        [MaxLength(500, ErrorMessage = "Vế phải không được vượt quá 500 ký tự")]
        public string VePhai { get; set; } = null!;

        public int? ThuTu { get; set; }
    }

    // Request cập nhật cặp ghép
    public class CapNhatCapGhepRequest
    {
        [Required(ErrorMessage = "Mã cặp là bắt buộc")]
        public int MaCap { get; set; }

        [MaxLength(500, ErrorMessage = "Vế trái không được vượt quá 500 ký tự")]
        public string? VeTrai { get; set; }

        [MaxLength(500, ErrorMessage = "Vế phải không được vượt quá 500 ký tự")]
        public string? VePhai { get; set; }

        public int? ThuTu { get; set; }
    }
}