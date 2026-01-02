using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO điểm danh hàng ngày
public class DiemDanhRequest
{
    // Có thể để trống vì thông tin người dùng lấy từ token
}

// DTO cấu hình điểm danh (Admin)
public class CapNhatCauHinhDiemDanhRequest
{
    [Required(ErrorMessage = "Ngày thứ là bắt buộc")]
    [Range(1, 7, ErrorMessage = "Ngày thứ từ 1-7")]
    public int NgayThu { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng vàng không được âm")]
    public int? ThuongVang { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thưởng XP không được âm")]
    public int? ThuongXp { get; set; }

    [StringLength(200)]
    public string? ThuongDacBiet { get; set; }
}