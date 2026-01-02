using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response bảo vệ chuỗi ngày
public class BaoVeChuoiNgayResponse
{
    public int MaBaoVe { get; set; }
    public DateOnly NgaySuDung { get; set; }
    public LoaiBaoVeStreakEnum? LoaiBaoVe { get; set; }
    public string TenLoaiBaoVe => LoaiBaoVe switch
    {
        LoaiBaoVeStreakEnum.Freeze => "Streak Freeze",
        LoaiBaoVeStreakEnum.HoiSinh => "Streak Hồi Sinh",
        _ => "Không xác định"
    };
    public string IconLoaiBaoVe => LoaiBaoVe switch
    {
        LoaiBaoVeStreakEnum.Freeze => "❄️",
        LoaiBaoVeStreakEnum.HoiSinh => "🔥",
        _ => "🛡️"
    };
    public int? ChuoiNgayTruocKhi { get; set; }
    public int? ChuoiNgaySauKhi { get; set; }
}

// Response sử dụng bảo vệ chuỗi ngày
public class SuDungBaoVeChuoiNgayResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public LoaiBaoVeStreakEnum? LoaiBaoVe { get; set; }
    public int ChuoiNgayDuocBaoVe { get; set; }
    public int SoStreakFreezeConLai { get; set; }
    public int SoStreakHoiSinhConLai { get; set; }
}

// Response trạng thái chuỗi ngày
public class TrangThaiChuoiNgayResponse
{
    public int ChuoiNgayHienTai { get; set; }
    public int ChuoiNgayDaiNhat { get; set; }
    public DateOnly? NgayHoatDongCuoi { get; set; }
    public bool DaHoatDongHomNay { get; set; }
    public bool ChuoiNgayDangGapNguyHiem { get; set; }
    public int SoStreakFreeze { get; set; }
    public int SoStreakHoiSinh { get; set; }
    public List<BaoVeChuoiNgayResponse> LichSuBaoVe { get; set; } = [];
}