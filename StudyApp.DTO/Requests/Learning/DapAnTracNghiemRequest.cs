using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo đáp án trắc nghiệm
    public class TaoDapAnTracNghiemRequest
    {
        [Required(ErrorMessage = "Nội dung đáp án là bắt buộc")]
        [MaxLength(1000, ErrorMessage = "Nội dung không được vượt quá 1000 ký tự")]
        public string NoiDung { get; set; } = null!;

        public bool LaDapAnDung { get; set; } = false;

        public int? ThuTu { get; set; }

        [MaxLength(1000, ErrorMessage = "Giải thích không được vượt quá 1000 ký tự")]
        public string? GiaiThich { get; set; }
    }

    // Request cập nhật đáp án trắc nghiệm
    public class CapNhatDapAnTracNghiemRequest
    {
        [Required(ErrorMessage = "Mã đáp án là bắt buộc")]
        public int MaDapAn { get; set; }

        [MaxLength(1000, ErrorMessage = "Nội dung không được vượt quá 1000 ký tự")]
        public string? NoiDung { get; set; }

        public bool? LaDapAnDung { get; set; }

        public int? ThuTu { get; set; }

        [MaxLength(1000, ErrorMessage = "Giải thích không được vượt quá 1000 ký tự")]
        public string? GiaiThich { get; set; }
    }
}