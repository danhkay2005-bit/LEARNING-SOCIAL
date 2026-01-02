using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tạo cấp độ mới (Admin)
public class TaoCapDoRequest
{
    [Required(ErrorMessage = "Tên cấp độ là bắt buộc")]
    [StringLength(50, ErrorMessage = "Tên cấp độ tối đa 50 ký tự")]
    public string TenCapDo { get; set; } = null!;

    public string? BieuTuong { get; set; }

    [Required(ErrorMessage = "Mức XP tối thiểu là bắt buộc")]
    [Range(0, int.MaxValue, ErrorMessage = "Mức XP tối thiểu không được âm")]
    public int MucXptoiThieu { get; set; }

    [Required(ErrorMessage = "Mức XP tối đa là bắt buộc")]
    [Range(1, int.MaxValue, ErrorMessage = "Mức XP tối đa phải lớn hơn 0")]
    public int MucXptoiDa { get; set; }
}

// DTO cập nhật cấp độ (Admin)
public class CapNhatCapDoRequest
{
    [StringLength(50, ErrorMessage = "Tên cấp độ tối đa 50 ký tự")]
    public string? TenCapDo { get; set; }

    public string? BieuTuong { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Mức XP tối thiểu không được âm")]
    public int? MucXptoiThieu { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Mức XP tối đa phải lớn hơn 0")]
    public int? MucXptoiDa { get; set; }
}