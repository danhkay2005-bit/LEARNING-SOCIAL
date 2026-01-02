using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response kho người dùng
public class KhoNguoiDungResponse
{
    public int MaKho { get; set; }
    public int MaVatPham { get; set; }
    public string TenVatPham { get; set; } = null!;
    public string? DuongDanHinh { get; set; }
    public string? DuongDanFile { get; set; }
    public int? SoLuong { get; set; }
    public bool? DaTrangBi { get; set; }
    public DateTime? ThoiGianMua { get; set; }
    public DateTime? ThoiGianHetHan { get; set; }
    public bool ConHieuLuc => ThoiGianHetHan == null || ThoiGianHetHan > DateTime.Now;
    public string? TenDanhMuc { get; set; }
    public byte? DoHiem { get; set; }
}

// Response danh sách kho
public class DanhSachKhoResponse
{
    public List<KhoNguoiDungResponse> VatPhams { get; set; } = [];
    public int TongVatPham { get; set; }
    public int TongSoLuong { get; set; }
}

// Response sử dụng vật phẩm
public class SuDungVatPhamResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public int? SoLuongConLai { get; set; }
    public BoostDangHoatDongResponse? BoostMoi { get; set; }
}