using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tạo nhiệm vụ mới (Admin)
public class TaoNhiemVuRequest
{
    [Required(ErrorMessage = "Tên nhiệm vụ là bắt buộc")]
    [StringLength(200, ErrorMessage = "Tên nhiệm vụ tối đa 200 ký tự")]
    public string TenNhiemVu { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }

    [Required(ErrorMessage = "Loại nhiệm vụ là bắt buộc")]
    public string LoaiNhiemVu { get; set; } = null!;

    public string? LoaiDieuKien { get; set; }

    [Required(ErrorMessage = "Điều kiện đạt được là bắt buộc")]
    [Range(1, int.MaxValue, ErrorMessage = "Điều kiện phải lớn hơn 0")]
    public int DieuKienDatDuoc { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng vàng không được âm")]
    public int? ThuongVang { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng kim cương không được âm")]
    public int? ThuongKimCuong { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng XP không được âm")]
    public int? ThuongXp { get; set; }

    public DateOnly? NgayBatDau { get; set; }
    public DateOnly? NgayKetThuc { get; set; }
    public bool? ConHieuLuc { get; set; } = true;
}

// DTO cập nhật nhiệm vụ (Admin)
public class CapNhatNhiemVuRequest
{
    [StringLength(200, ErrorMessage = "Tên nhiệm vụ tối đa 200 ký tự")]
    public string? TenNhiemVu { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }
    public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }
    public LoaiDieuKienEnum LoaiDieuKien { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Điều kiện phải lớn hơn 0")]
    public int? DieuKienDatDuoc { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng vàng không được âm")]
    public int? ThuongVang { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng kim cương không được âm")]
    public int? ThuongKimCuong { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng XP không được âm")]
    public int? ThuongXp { get; set; }

    public DateOnly? NgayBatDau { get; set; }
    public DateOnly? NgayKetThuc { get; set; }
    public bool? ConHieuLuc { get; set; }
}

// DTO cập nhật tiến độ nhiệm vụ
public class CapNhatTienDoNhiemVuRequest
{
    [Required(ErrorMessage = "Mã nhiệm vụ là bắt buộc")]
    public int MaNhiemVu { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Tiến độ không được âm")]
    public int TienDoThem { get; set; }
}

// DTO nhận thưởng nhiệm vụ
public class NhanThuongNhiemVuRequest
{
    [Required(ErrorMessage = "Mã nhiệm vụ là bắt buộc")]
    public int MaNhiemVu { get; set; }
}