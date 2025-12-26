using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests
{
    public class DangKyNguoiDungRequest
    {
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }
    }

    public class CapNhatHoSoNguoiDungRequest
    {
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? AnhBia { get; set; }
        public string? TieuSu { get; set; }
    }

    public class DoiMatKhauNguoiDungRequest
    {
        public string? MatKhauHienTai { get; set; }
        public string? MatKhauMoi { get; set; }
    }

    public class DangNhapRequest
    {
        public string? TenDangNhap { get; set; }
        public string? MatKhau {  get; set; }
    }
}
