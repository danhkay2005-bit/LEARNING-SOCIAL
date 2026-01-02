using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo ghi chú thẻ
    public class TaoGhiChuTheRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Nội dung ghi chú là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string NoiDung { get; set; } = null!;

        [MaxLength(20, ErrorMessage = "Mã màu không hợp lệ")]
        public string? MauNen { get; set; }
    }

    // Request cập nhật ghi chú thẻ
    public class CapNhatGhiChuTheRequest
    {
        [Required(ErrorMessage = "Mã ghi chú là bắt buộc")]
        public int MaGhiChu { get; set; }

        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string? NoiDung { get; set; }

        [MaxLength(20, ErrorMessage = "Mã màu không hợp lệ")]
        public string? MauNen { get; set; }
    }

    // Request xóa ghi chú thẻ
    public class XoaGhiChuTheRequest
    {
        [Required(ErrorMessage = "Mã ghi chú là bắt buộc")]
        public int MaGhiChu { get; set; }
    }
}