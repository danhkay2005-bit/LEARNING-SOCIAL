using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo thư mục
    public class TaoThuMucRequest
    {
        [Required(ErrorMessage = "Tên thư mục là bắt buộc")]
        [MaxLength(100, ErrorMessage = "Tên thư mục không được vượt quá 100 ký tự")]
        public string TenThuMuc { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? MoTa { get; set; }

        public int? MaThuMucCha { get; set; }

        public int? ThuTu { get; set; }
    }

    // Request cập nhật thư mục
    public class CapNhatThuMucRequest
    {
        [Required(ErrorMessage = "Mã thư mục là bắt buộc")]
        public int MaThuMuc { get; set; }

        [MaxLength(100, ErrorMessage = "Tên thư mục không được vượt quá 100 ký tự")]
        public string? TenThuMuc { get; set; }

        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? MoTa { get; set; }

        public int? MaThuMucCha { get; set; }

        public int? ThuTu { get; set; }
    }

    // Request xóa thư mục
    public class XoaThuMucRequest
    {
        [Required(ErrorMessage = "Mã thư mục là bắt buộc")]
        public int MaThuMuc { get; set; }

        public bool XoaCaBoDeTrongThuMuc { get; set; } = false;
    }

    // Request sắp xếp thư mục
    public class SapXepThuMucRequest
    {
        [Required(ErrorMessage = "Danh sách thứ tự là bắt buộc")]
        public List<ThuTuThuMucItem> DanhSachThuTu { get; set; } =[];
    }

    public class ThuTuThuMucItem
    {
        public int MaThuMuc { get; set; }
        public int ThuTu { get; set; }
    }
}