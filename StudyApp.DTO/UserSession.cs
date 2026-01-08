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
        public string MatKhauMaHoa { get; set; } = string.Empty;
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }  // ✅ SỬA: string thay vì int
        public int MaVaiTro { get; set; }
        public int Vang { get; set; }
        public int KimCuong { get; set; }
    }
}
