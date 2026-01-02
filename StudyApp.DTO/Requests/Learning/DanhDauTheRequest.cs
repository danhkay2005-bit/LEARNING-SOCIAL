using StudyApp.DTO.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request đánh dấu thẻ
    public class DanhDauTheRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Loại đánh dấu là bắt buộc")]
        public LoaiDanhDauTheEnum LoaiDanhDau { get; set; }

        [MaxLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? GhiChu { get; set; }
    }

    // Request bỏ đánh dấu thẻ
    public class BoDanhDauTheRequest
    {
        [Required(ErrorMessage = "Mã đánh dấu là bắt buộc")]
        public int MaDanhDau { get; set; }
    }

    // Request lấy danh sách thẻ đánh dấu
    public class LayTheDanhDauRequest
    {
        public LoaiDanhDauTheEnum? LoaiDanhDau { get; set; }

        public int? MaBoDe { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}