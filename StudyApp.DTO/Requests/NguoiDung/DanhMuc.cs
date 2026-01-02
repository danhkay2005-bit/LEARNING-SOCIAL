using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tạo danh mục sản phẩm (Admin)
public class TaoDanhMucSanPhamRequest
{
    [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
    [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
    public string TenDanhMuc { get; set; } = null!;

    public string? BieuTuong { get; set; }

    [StringLength(300, ErrorMessage = "Mô tả tối đa 300 ký tự")]
    public string? MoTa { get; set; }

    public int? ThuTuHienThi { get; set; }
}

// DTO cập nhật danh mục sản phẩm (Admin)
public class CapNhatDanhMucSanPhamRequest
{
    [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
    public string? TenDanhMuc { get; set; }

    public string? BieuTuong { get; set; }

    [StringLength(300, ErrorMessage = "Mô tả tối đa 300 ký tự")]
    public string? MoTa { get; set; }

    public int? ThuTuHienThi { get; set; }
}