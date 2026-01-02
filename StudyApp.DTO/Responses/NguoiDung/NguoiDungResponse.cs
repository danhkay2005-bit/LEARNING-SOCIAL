using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response thông tin người dùng cơ bản
public class NguoiDungResponse
{
    public Guid MaNguoiDung { get; set; }
    public string TenDangNhap { get; set; } = null!;
    public string? Email { get; set; }
    public string? SoDienThoai { get; set; }
    public string? HoVaTen { get; set; }
    public DateOnly? NgaySinh { get; set; }
    public GioiTinhEnum? GioiTinh { get; set; }
    public string TenGioiTinh => GioiTinh switch
    {
        GioiTinhEnum.Nam => "Nam",
        GioiTinhEnum.Nu => "Nữ",
        GioiTinhEnum.Khac => "Khác",
        _ => "Chưa cập nhật"
    };
    public string? HinhDaiDien { get; set; }
    public string? AnhBia { get; set; }
    public string? TieuSu { get; set; }
    public bool? TrangThaiOnline { get; set; }
    public DateTime? LanOnlineCuoi { get; set; }
    public DateTime? ThoiGianTao { get; set; }

    // Thông tin vai trò và cấp độ
    public VaiTroResponse? VaiTro { get; set; }
    public CapDoResponse? CapDo { get; set; }
}

// Response thông tin người dùng chi tiết (profile đầy đủ)
public class NguoiDungChiTietResponse : NguoiDungResponse
{
    // Tài sản
    public int? Vang { get; set; }
    public int? KimCuong { get; set; }
    public int? TongDiemXp { get; set; }

    // Thống kê học tập
    public int? TongSoTheHoc { get; set; }
    public int? TongSoTheDung { get; set; }
    public int? TongThoiGianHocPhut { get; set; }
    public string ThoiGianHocFormatted => FormatThoiGianHoc(TongThoiGianHocPhut);

    // Chuỗi ngày
    public int? ChuoiNgayHocLienTiep { get; set; }
    public int? ChuoiNgayDaiNhat { get; set; }
    public int? SoStreakFreeze { get; set; }
    public int? SoStreakHoiSinh { get; set; }
    public DateOnly? NgayHoatDongCuoi { get; set; }

    // Thống kê thách đấu
    public int? SoTranThachDau { get; set; }
    public int? SoTranThang { get; set; }
    public int? SoTranThua { get; set; }
    public double? TyLeThang => SoTranThachDau > 0
        ? Math.Round((double)(SoTranThang ?? 0) / SoTranThachDau.Value * 100, 2)
        : null;

    // Trạng thái
    public bool? DaXacThucEmail { get; set; }

    // Tùy chỉnh profile
    public TuyChinhProfileResponse? TuyChinhProfile { get; set; }

    private static string FormatThoiGianHoc(int? phut)
    {
        if (phut == null || phut == 0) return "0 phút";
        var gio = phut / 60;
        var phutConLai = phut % 60;
        if (gio > 0 && phutConLai > 0) return $"{gio} giờ {phutConLai} phút";
        if (gio > 0) return $"{gio} giờ";
        return $"{phutConLai} phút";
    }
}

// Response đăng nhập
public class DangNhapResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public NguoiDungResponse NguoiDung { get; set; } = null!;
}

// Response đăng ký
public class DangKyResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public Guid? MaNguoiDung { get; set; }
}

// Response thông tin người dùng ngắn gọn (dùng trong danh sách)
public class NguoiDungTomTatResponse
{
    public Guid MaNguoiDung { get; set; }
    public string TenDangNhap { get; set; } = null!;
    public string? HoVaTen { get; set; }
    public string? HinhDaiDien { get; set; }
    public int? TongDiemXp { get; set; }
    public string? TenCapDo { get; set; }
    public bool? TrangThaiOnline { get; set; }
}

// Response bảng xếp hạng
public class BangXepHangNguoiDungResponse
{
    public int Hang { get; set; }
    public Guid MaNguoiDung { get; set; }
    public string TenDangNhap { get; set; } = null!;
    public string? HoVaTen { get; set; }
    public string? HinhDaiDien { get; set; }
    public int? TongDiemXp { get; set; }
    public string? TenCapDo { get; set; }
    public int? ChuoiNgayHocLienTiep { get; set; }
}