using System;

namespace Common
{
    public static class SessionManager
    {
        // Thông tin người dùng đang đăng nhập
        public static Guid? CurrentUserId { get; private set; }
        public static string? CurrentUsername { get; private set; }
        public static string? CurrentHoTen { get; private set; }
        public static VaiTro CurrentVaiTro { get; private set; }
        public static int CurrentLevel { get; private set; }
        public static int CurrentXP { get; private set; }
        public static int CurrentVang { get; private set; }
        public static int CurrentKimCuong { get; private set; }
        public static int CurrentStreak { get; private set; }

        // Kiểm tra đã đăng nhập chưa
        public static bool IsLoggedIn => CurrentUserId.HasValue;

        // Kiểm tra quyền Admin
        public static bool IsAdmin => CurrentVaiTro == VaiTro.Admin;

        // Đăng nhập - Lưu thông tin user
        public static void Login(Guid userId, string username, string hoTen,
            VaiTro vaiTro, int level, int xp, int vang, int kimCuong, int streak)
        {
            CurrentUserId = userId;
            CurrentUsername = username;
            CurrentHoTen = hoTen;
            CurrentVaiTro = vaiTro;
            CurrentLevel = level;
            CurrentXP = xp;
            CurrentVang = vang;
            CurrentKimCuong = kimCuong;
            CurrentStreak = streak;
        }

        // Đăng xuất - Xóa thông tin
        public static void Logout()
        {
            CurrentUserId = null;
            CurrentUsername = null;
            CurrentHoTen = null;
            CurrentVaiTro = VaiTro.Member;
            CurrentLevel = 0;
            CurrentXP = 0;
            CurrentVang = 0;
            CurrentKimCuong = 0;
            CurrentStreak = 0;
        }

        // Cập nhật XP
        public static void UpdateXP(int xp)
        {
            CurrentXP = xp;
            CurrentLevel = Helpers.GetLevelFromXP(xp);
        }

        // Cập nhật Vàng
        public static void UpdateVang(int vang)
        {
            CurrentVang = vang;
        }

        // Cập nhật Kim Cương
        public static void UpdateKimCuong(int kimCuong)
        {
            CurrentKimCuong = kimCuong;
        }

        // Cập nhật Streak
        public static void UpdateStreak(int streak)
        {
            CurrentStreak = streak;
        }
    }
}