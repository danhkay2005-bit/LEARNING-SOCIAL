using StudyApp.DTO.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request đánh giá bộ đề
    public class DanhGiaBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Số sao là bắt buộc")]
        [Range(1, 5, ErrorMessage = "Số sao phải từ 1-5")]
        public byte SoSao { get; set; }

        [MaxLength(1000, ErrorMessage = "Nội dung không được vượt quá 1000 ký tự")]
        public string? NoiDung { get; set; }

        public MucDoKhoEnum? DoKhoThucTe { get; set; }

        [Range(1, 5, ErrorMessage = "Chất lượng nội dung phải từ 1-5")]
        public byte? ChatLuongNoiDung { get; set; }
    }

    // Request cập nhật đánh giá
    public class CapNhatDanhGiaRequest
    {
        [Required(ErrorMessage = "Mã đánh giá là bắt buộc")]
        public int MaDanhGia { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao phải từ 1-5")]
        public byte? SoSao { get; set; }

        [MaxLength(1000, ErrorMessage = "Nội dung không được vượt quá 1000 ký tự")]
        public string? NoiDung { get; set; }

        public MucDoKhoEnum? DoKhoThucTe { get; set; }

        [Range(1, 5, ErrorMessage = "Chất lượng nội dung phải từ 1-5")]
        public byte? ChatLuongNoiDung { get; set; }
    }

    // Request xóa đánh giá
    public class XoaDanhGiaRequest
    {
        [Required(ErrorMessage = "Mã đánh giá là bắt buộc")]
        public int MaDanhGia { get; set; }
    }

    // Request lấy danh sách đánh giá
    public class LayDanhGiaBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        public byte? SoSao { get; set; }

        public string? SapXepTheo { get; set; } // "moi_nhat", "huu_ich"

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}