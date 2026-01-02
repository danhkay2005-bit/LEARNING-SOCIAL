using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo mục tiêu cá nhân
    public class TaoMucTieuRequest
    {
        [Required(ErrorMessage = "Loại mục tiêu là bắt buộc")]
        public LoaiMucTieuEnum LoaiMucTieu { get; set; }

        [MaxLength(200, ErrorMessage = "Tên mục tiêu không được vượt quá 200 ký tự")]
        public string? TenMucTieu { get; set; }

        [Required(ErrorMessage = "Giá trị mục tiêu là bắt buộc")]
        [Range(1, 10000, ErrorMessage = "Giá trị mục tiêu phải từ 1 đến 10000")]
        public int GiaTriMucTieu { get; set; }

        [MaxLength(50, ErrorMessage = "Đơn vị không được vượt quá 50 ký tự")]
        public string? DonVi { get; set; }

        public DateOnly? NgayBatDau { get; set; }

        public DateOnly? NgayKetThuc { get; set; }
    }

    // Request cập nhật mục tiêu
    public class CapNhatMucTieuRequest
    {
        [Required(ErrorMessage = "Mã mục tiêu là bắt buộc")]
        public int MaMucTieu { get; set; }

        [MaxLength(200, ErrorMessage = "Tên mục tiêu không được vượt quá 200 ký tự")]
        public string? TenMucTieu { get; set; }

        [Range(1, 10000, ErrorMessage = "Giá trị mục tiêu phải từ 1 đến 10000")]
        public int? GiaTriMucTieu { get; set; }

        public DateOnly? NgayKetThuc { get; set; }
    }

    // Request xóa mục tiêu
    public class XoaMucTieuRequest
    {
        [Required(ErrorMessage = "Mã mục tiêu là bắt buộc")]
        public int MaMucTieu { get; set; }
    }

    // Request lấy danh sách mục tiêu
    public class LayMucTieuRequest
    {
        public LoaiMucTieuEnum? LoaiMucTieu { get; set; }

        public bool? DaHoanThanh { get; set; }

        public bool? ConHieuLuc { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}