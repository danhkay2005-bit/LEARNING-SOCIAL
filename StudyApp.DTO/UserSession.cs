using System;

namespace StudyApp.DTO
{
    public static class UserSession
    {
        public static NguoiDungDTO? CurrentUser { get; set; }
        public static bool IsLoggedIn => CurrentUser != null;
        public static void Logout() => CurrentUser = null;
    }

    public class NguoiDungDTO
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhauMaHoa { get; set; } = string.Empty; // Pass đã mã hóa
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public int SoDienThoai { get; set; }
        public int MaVaiTro { get; set; }
        public int Vang { get; set; }
        public int KimCuong { get; set; }

    }
}
