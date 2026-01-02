using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response vật phẩm
public class VatPhamResponse
{
    public int MaVatPham { get; set; }
    public string TenVatPham { get; set; } = null!;
    public string? MoTa { get; set; }
    public int Gia { get; set; }
    public LoaiTienTeEnum LoaiTienTe { get; set; }
    public string TenLoaiTienTe => LoaiTienTe switch
    {
        LoaiTienTeEnum.Vang => "Vàng",
        LoaiTienTeEnum.KimCuong => "Kim cương",
        LoaiTienTeEnum.XP => "XP",
        _ => "Không xác định"
    };
    public string IconTienTe => LoaiTienTe switch
    {
        LoaiTienTeEnum.Vang => "🪙",
        LoaiTienTeEnum.KimCuong => "💎",
        LoaiTienTeEnum.XP => "⭐",
        _ => "💰"
    };
    public string? DuongDanHinh { get; set; }
    public string? DuongDanFile { get; set; }
    public int? ThoiHanPhut { get; set; }
    public string? ThoiHanFormatted => FormatThoiHan(ThoiHanPhut);
    public double? GiaTriHieuUng { get; set; }
    public byte? DoHiem { get; set; }
    public string TenDoHiem => DoHiem switch
    {
        1 => "Phổ thông",
        2 => "Hiếm",
        3 => "Sử thi",
        4 => "Huyền thoại",
        5 => "Độc nhất",
        _ => "Không xác định"
    };
    public string MauDoHiem => DoHiem switch
    {
        1 => "#9E9E9E",
        2 => "#4CAF50",
        3 => "#2196F3",
        4 => "#9C27B0",
        5 => "#FF9800",
        _ => "#9E9E9E"
    };
    public bool? ConHang { get; set; }
    public DanhMucSanPhamTomTatResponse? DanhMuc { get; set; }

    private static string? FormatThoiHan(int? phut)
    {
        if (phut == null) return "Vĩnh viễn";
        if (phut < 60) return $"{phut} phút";
        if (phut < 1440) return $"{phut / 60} giờ";
        return $"{phut / 1440} ngày";
    }
}

// Response vật phẩm tóm tắt
public class VatPhamTomTatResponse
{
    public int MaVatPham { get; set; }
    public string TenVatPham { get; set; } = null!;
    public string? DuongDanHinh { get; set; }
    public string? DuongDanFile { get; set; }
    public byte? DoHiem { get; set; }
}

// Response danh mục sản phẩm tóm tắt
public class DanhMucSanPhamTomTatResponse
{
    public int MaDanhMuc { get; set; }
    public string TenDanhMuc { get; set; } = null!;
    public string? BieuTuong { get; set; }
}

// Response mua vật phẩm
public class MuaVatPhamResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public int? SoTienDaChi { get; set; }
    public LoaiTienTeEnum? LoaiTienTe { get; set; }
    public int? SoDuConLai { get; set; }
    public KhoNguoiDungResponse? VatPhamMoi { get; set; }
}

// Response cửa hàng
public class CuaHangResponse
{
    public List<DanhMucVoiVatPhamResponse> DanhMucs { get; set; } = [];
    public int TongVatPham { get; set; }
    public int VangHienTai { get; set; }
    public int KimCuongHienTai { get; set; }
}

// Response danh mục với vật phẩm
public class DanhMucVoiVatPhamResponse
{
    public int MaDanhMuc { get; set; }
    public string TenDanhMuc { get; set; } = null!;
    public string? BieuTuong { get; set; }
    public string? MoTa { get; set; }
    public List<VatPhamResponse> VatPhams { get; set; } = [];
}