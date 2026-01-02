using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO phân trang chung
public class PaginationRequest
{
    [Range(1, 100, ErrorMessage = "Số lượng mỗi trang từ 1-100")]
    public int PageSize { get; set; } = 20;

    [Range(1, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn 0")]
    public int PageNumber { get; set; } = 1;

    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}

// DTO tìm kiếm chung
public class SearchRequest : PaginationRequest
{
    [StringLength(200, ErrorMessage = "Từ khóa tìm kiếm tối đa 200 ký tự")]
    public string? Keyword { get; set; }
}

// DTO xác thực email
public class XacThucEmailRequest
{
    [Required(ErrorMessage = "Token là bắt buộc")]
    public string Token { get; set; } = null!;
}

// DTO quên mật khẩu
public class QuenMatKhauRequest
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;
}

// DTO đặt lại mật khẩu
public class DatLaiMatKhauRequest
{
    [Required(ErrorMessage = "Token là bắt buộc")]
    public string Token { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-100 ký tự")]
    public string MatKhauMoi { get; set; } = null!;

    [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
    [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string XacNhanMatKhau { get; set; } = null!;
}