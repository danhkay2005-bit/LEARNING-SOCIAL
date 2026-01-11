using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.User
{
    // =========================
    // BASIC PROFILE
    // =========================

    public class NguoiDungResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = null!;

        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoVaTen { get; set; }

        public DateOnly? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }

        public VaiTroEnum VaiTro { get; set; }
        public int CapDo { get; set; }

        public string? HinhDaiDien { get; set; }
        public string? TieuSu { get; set; }

        public bool DaXacThucEmail { get; set; }
        public bool TrangThaiOnline { get; set; }

        public DateTime? LanOnlineCuoi { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // =========================
    // GAMIFICATION
    // =========================

    public class NguoiDungGamificationResponse
    {
        public int TongDiemXp { get; set; }
        public int Vang { get; set; }
        public int KimCuong { get; set; }

        public int TongSoTheHoc { get; set; }
        public int TongSoTheDung { get; set; }
        public int TongThoiGianHocPhut { get; set; }

        public int ChuoiNgayHocLienTiep { get; set; }
        public int ChuoiNgayDaiNhat { get; set; }

        public int SoStreakFreeze { get; set; }
        public int SoStreakHoiSinh { get; set; }

        public DateOnly? NgayHoatDongCuoi { get; set; }
    }

    // =========================
    // THÁCH ĐẤU
    // =========================

    public class NguoiDungThachDauResponse
    {
        public int SoTranThachDau { get; set; }
        public int SoTranThang { get; set; }
        public int SoTranThua { get; set; }
    }
}
