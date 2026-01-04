using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tạo thành tựu mới (Admin)
public class TaoThanhTuuRequest
{
    [Required(ErrorMessage = "Tên thành tựu là bắt buộc")]
    [StringLength(200, ErrorMessage = "Tên thành tựu tối đa 200 ký tự")]
    public string TenThanhTuu { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }
    public string? HinhHuy { get; set; }

    [Required(ErrorMessage = "Loại thành tựu là bắt buộc")]
    public LoaiThanhTuuEnum LoaiThanhTuu { get; set; }  // Changed to enum

    public LoaiDieuKienEnum? DieuKienLoai { get; set; }  // Changed to nullable enum

    [Required(ErrorMessage = "Điều kiện giá trị là bắt buộc")]
    [Range(1, int.MaxValue, ErrorMessage = "Giá trị điều kiện phải lớn hơn 0")]
    public int DieuKienGiaTri { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongXp { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongVang { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongKimCuong { get; set; }

    [Range(1, 5, ErrorMessage = "Độ hiếm từ 1-5")]
    public byte? DoHiem { get; set; }

    public bool? BiAn { get; set; } = false;
}

// DTO cập nhật thành tựu (Admin)
public class CapNhatThanhTuuRequest
{
    [StringLength(200, ErrorMessage = "Tên thành tựu tối đa 200 ký tự")]
    public string? TenThanhTuu { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }
    public string? HinhHuy { get; set; }

    public LoaiThanhTuuEnum? LoaiThanhTuu { get; set; }  // Made nullable
    public LoaiDieuKienEnum? DieuKienLoai { get; set; }  // Made nullable

    [Range(1, int.MaxValue, ErrorMessage = "Giá trị điều kiện phải lớn hơn 0")]
    public int? DieuKienGiaTri { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongXp { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongVang { get; set; }

    [Range(0, int.MaxValue)]
    public int? ThuongKimCuong { get; set; }

    [Range(1, 5, ErrorMessage = "Độ hiếm từ 1-5")]
    public byte? DoHiem { get; set; }

    public bool? BiAn { get; set; }
}

// DTO đánh dấu đã xem thành tựu
public class DanhDauXemThanhTuuRequest
{
    [Required]
    public int MaThanhTuu { get; set; }
}

// DTO chia sẻ thành tựu
public class ChiaSeThanhTuuRequest
{
    [Required]
    public int MaThanhTuu { get; set; }
}