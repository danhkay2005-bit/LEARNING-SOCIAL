namespace StudyApp.DTO
{
    public static class UserSession
    {
        public static NguoiDungDTO? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Login(NguoiDungDTO user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }

    public class NguoiDungDTO
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HinhDaiDien { get; set; }    

        public int MaVaiTro { get; set; }
        
        // ===== GAMIFICATION PROPERTIES =====
        public int Vang { get; set; }
        public int KimCuong { get; set; }
        public int TongDiemXp { get; set; }
        public int ChuoiNgayHocLienTiep { get; set; }
        public int TongSoTheHoc { get; set; }
        public int TongSoTheDung { get; set; }
    }
}
