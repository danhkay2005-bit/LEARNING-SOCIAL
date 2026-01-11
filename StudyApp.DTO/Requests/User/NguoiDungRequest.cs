using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Requests.User
{
    // =========================
    // AUTH
    // =========================

    public class DangKyNguoiDungRequest
    {
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;

        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoVaTen { get; set; }

        public DateOnly? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }
    }

    public class DangNhapRequest
    {
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
    }

    // =========================
    // PROFILE
    // =========================

    public class CapNhatHoSoRequest
    {
        public string? HoVaTen { get; set; }
        public DateOnly? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }

        public string? HinhDaiDien { get; set; }
        public string? TieuSu { get; set; }
    }

    public class DoiMatKhauRequest
    {
        public string MatKhauCu { get; set; } = null!;
        public string MatKhauMoi { get; set; } = null!;
    }
    public class CapNhatVaiTroCapDoRequest
    {
        public VaiTroEnum VaiTro { get; set; }
        public int MaCapDo { get; set; }
    }
}
