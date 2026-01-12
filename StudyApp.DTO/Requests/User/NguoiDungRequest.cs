using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.User
{
    // =========================
    // AUTH
    // =========================

    public class DangKyNguoiDungRequest
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3-50 ký tự")]
        public string TenDangNhap { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-100 ký tự")]
        public string MatKhau { get; set; } = null!;

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string XacNhanMatKhau { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? SoDienThoai { get; set; }

        [StringLength(100, ErrorMessage = "Họ và tên tối đa 100 ký tự")]
        public string? HoVaTen { get; set; }

        public DateOnly? NgaySinh { get; set; }

        [Range(0, 2, ErrorMessage = "Giới tính không hợp lệ")]
        public byte? GioiTinh { get; set; }
    }

    public class DangNhapRequest
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string TenDangNhap { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string MatKhau { get; set; } = null!;
    }

    // =========================
    // PROFILE
    // =========================

    public class CapNhatHoSoRequest
    {
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? SoDienThoai { get; set; }

        [StringLength(100, ErrorMessage = "Họ và tên tối đa 100 ký tự")]
        public string? HoVaTen { get; set; }

        public DateOnly? NgaySinh { get; set; }

        [Range(0, 2, ErrorMessage = "Giới tính không hợp lệ")]
        public byte? GioiTinh { get; set; }

        [StringLength(500, ErrorMessage = "Tiểu sử tối đa 500 ký tự")]
        public string? TieuSu { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? AnhBia { get; set; }
    }

    public class DoiMatKhauRequest
    {
        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc")]
        public string MatKhauCu { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-100 ký tự")]
        public string MatKhauMoi { get; set; } = null!;

        [Required(ErrorMessage = "Xác nhận mật khẩu mới là bắt buộc")]
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string XacNhanMatKhauMoi { get; set; } = null!;
    }

    public class CapNhatVaiTroCapDoRequest
    {
        public VaiTroEnum VaiTro { get; set; }
        public int MaCapDo { get; set; }
    }

    
}
