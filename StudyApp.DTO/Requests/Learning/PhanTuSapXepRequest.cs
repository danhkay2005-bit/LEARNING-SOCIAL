using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo phần tử sắp xếp
    public class TaoPhanTuSapXepRequest
    {
        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        [MaxLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự")]
        public string NoiDung { get; set; } = null!;

        [Required(ErrorMessage = "Thứ tự đúng là bắt buộc")]
        [Range(1, 100, ErrorMessage = "Thứ tự phải từ 1 đến 100")]
        public int ThuTuDung { get; set; }
    }

    // Request cập nhật phần tử sắp xếp
    public class CapNhatPhanTuSapXepRequest
    {
        [Required(ErrorMessage = "Mã phần tử là bắt buộc")]
        public int MaPhanTu { get; set; }

        [MaxLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự")]
        public string? NoiDung { get; set; }

        [Range(1, 100, ErrorMessage = "Thứ tự phải từ 1 đến 100")]
        public int? ThuTuDung { get; set; }
    }
}