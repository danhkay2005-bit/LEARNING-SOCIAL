using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response tùy chỉnh profile
public class TuyChinhProfileResponse
{
    public Guid MaNguoiDung { get; set; }
    public VatPhamTomTatResponse? AvatarDangDung { get; set; }
    public VatPhamTomTatResponse? KhungDangDung { get; set; }
    public VatPhamTomTatResponse? HinhNenDangDung { get; set; }
    public VatPhamTomTatResponse? HieuUngDangDung { get; set; }
    public VatPhamTomTatResponse? ThemeDangDung { get; set; }
    public VatPhamTomTatResponse? NhacNenDangDung { get; set; }
    public VatPhamTomTatResponse? BadgeDangDung { get; set; }
    public ThanhTuuTomTatResponse? HuyHieuHienThi { get; set; }
    public string? CauChamNgon { get; set; }
    public DateTime? ThoiGianCapNhat { get; set; }
}

// Response thành tựu tóm tắt
public class ThanhTuuTomTatResponse
{
    public int MaThanhTuu { get; set; }
    public string TenThanhTuu { get; set; } = null!;
    public string? BieuTuong { get; set; }
    public string? HinhHuy { get; set; }
    public byte? DoHiem { get; set; }
}

// Response cập nhật tùy chỉnh profile
public class CapNhatTuyChinhProfileResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public TuyChinhProfileResponse? TuyChinhProfile { get; set; }
}