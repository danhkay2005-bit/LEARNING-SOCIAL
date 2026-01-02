using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tạo vai trò mới (Admin)
public class TaoVaiTroRequest
{
    [Required(ErrorMessage = "Tên vai trò là bắt buộc")]
    [StringLength(50, ErrorMessage = "Tên vai trò tối đa 50 ký tự")]
    public string TenVaiTro { get; set; } = null!;

    [StringLength(200, ErrorMessage = "Mô tả tối đa 200 ký tự")]
    public string? MoTa { get; set; }
}

// DTO cập nhật vai trò (Admin)
public class CapNhatVaiTroRequest
{
    [StringLength(50, ErrorMessage = "Tên vai trò tối đa 50 ký tự")]
    public string? TenVaiTro { get; set; }

    [StringLength(200, ErrorMessage = "Mô tả tối đa 200 ký tự")]
    public string? MoTa { get; set; }
}

// DTO gán vai trò cho người dùng (Admin)
public class GanVaiTroNguoiDungRequest
{
    [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
    public Guid MaNguoiDung { get; set; }

    [Required(ErrorMessage = "Mã vai trò là bắt buộc")]
    public VaiTroEnum MaVaiTro { get; set; }
}   