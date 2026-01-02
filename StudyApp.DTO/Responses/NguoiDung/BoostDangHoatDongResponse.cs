using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response boost đang hoạt động
public class BoostDangHoatDongResponse
{
    public int MaBoost { get; set; }
    public int MaVatPham { get; set; }
    public string TenVatPham { get; set; } = null!;
    public string? DuongDanHinh { get; set; }
    public LoaiBoostEnum? LoaiBoost { get; set; }
    public string TenLoaiBoost => LoaiBoost switch
    {
        LoaiBoostEnum.XP => "Boost XP",
        LoaiBoostEnum.Vang => "Boost Vàng",
        _ => "Boost"
    };
    public string IconLoaiBoost => LoaiBoost switch
    {
        LoaiBoostEnum.XP => "⭐",
        LoaiBoostEnum.Vang => "🪙",
        _ => "🚀"
    };
    public double HeSoNhan { get; set; }
    public string HeSoNhanFormatted => $"x{HeSoNhan:0.#}";
    public DateTime? ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
    public bool? ConHieuLuc { get; set; }
    public int ThoiGianConLaiGiay => ConHieuLuc == true
        ? (int)Math.Max(0, (ThoiGianKetThuc - DateTime.Now).TotalSeconds)
        : 0;
    public string ThoiGianConLaiFormatted => FormatThoiGian(ThoiGianConLaiGiay);

    private static string FormatThoiGian(int giay)
    {
        if (giay <= 0) return "Hết hạn";
        if (giay < 60) return $"{giay} giây";
        if (giay < 3600) return $"{giay / 60} phút {giay % 60} giây";
        return $"{giay / 3600} giờ {(giay % 3600) / 60} phút";
    }
}

// Response danh sách boost đang hoạt động
public class DanhSachBoostResponse
{
    public List<BoostDangHoatDongResponse> Boosts { get; set; } = [];
    public double TongHeSoXp { get; set; } = 1.0;
    public double TongHeSoVang { get; set; } = 1.0;
    public bool DangCoBoostXp => TongHeSoXp > 1.0;
    public bool DangCoBoostVang => TongHeSoVang > 1.0;
}

// Response kích hoạt boost
public class KichHoatBoostResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public BoostDangHoatDongResponse? Boost { get; set; }
}