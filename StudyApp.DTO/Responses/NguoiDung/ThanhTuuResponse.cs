using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response thành tựu
public class ThanhTuuResponse
{
    public int MaThanhTuu { get; set; }
    public string TenThanhTuu { get; set; } = null!;
    public string? MoTa { get; set; }
    public string? BieuTuong { get; set; }
    public string? HinhHuy { get; set; }
    public LoaiThanhTuuEnum? LoaiThanhTuu { get; set; }
    public string TenLoaiThanhTuu => LoaiThanhTuu switch
    {
        LoaiThanhTuuEnum.HocTap => "Học tập",
        LoaiThanhTuuEnum.ChuoiNgay => "Chuỗi ngày",
        LoaiThanhTuuEnum.XaHoi => "Xã hội",
        LoaiThanhTuuEnum.SangTao => "Sáng tạo",
        LoaiThanhTuuEnum.ThachDau => "Thách đấu",
        LoaiThanhTuuEnum.KhamPha => "Khám phá",
        LoaiThanhTuuEnum.AnDanh => "Ẩn danh",
        _ => "Khác"
    };
    public LoaiDieuKienEnum? DieuKienLoai { get; set; }
    public int DieuKienGiaTri { get; set; }
    public int? ThuongXp { get; set; }
    public int? ThuongVang { get; set; }
    public int? ThuongKimCuong { get; set; }
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
        1 => "#9E9E9E",    // Xám
        2 => "#4CAF50",    // Xanh lá
        3 => "#2196F3",    // Xanh dương
        4 => "#9C27B0",    // Tím
        5 => "#FF9800",    // Cam vàng
        _ => "#9E9E9E"
    };
    public bool? BiAn { get; set; }
}

// Response thành tựu với trạng thái người dùng
public class ThanhTuuVoiTrangThaiResponse : ThanhTuuResponse
{
    public bool DaDat { get; set; }
    public DateTime? NgayDat { get; set; }
    public bool? DaXem { get; set; }
    public bool? DaChiaSe { get; set; }
    public int TienDoHienTai { get; set; }
    public double PhanTramTienDo => DieuKienGiaTri > 0
        ? Math.Min(100, Math.Round((double)TienDoHienTai / DieuKienGiaTri * 100, 2))
        : 0;
}

// Response thành tựu đã đạt được
public class ThanhTuuDatDuocResponse
{
    public int MaThanhTuu { get; set; }
    public string TenThanhTuu { get; set; } = null!;
    public string? MoTa { get; set; }
    public string? BieuTuong { get; set; }
    public string? HinhHuy { get; set; }
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
    public DateTime? NgayDat { get; set; }
    public bool? DaXem { get; set; }
    public bool? DaChiaSe { get; set; }
}

// Response thông báo thành tựu mới
public class ThongBaoThanhTuuMoiResponse
{
    public bool CoThanhTuuMoi { get; set; }
    public List<ThanhTuuDatDuocResponse> ThanhTuuMoi { get; set; } = [];
    public int TongThuongVang { get; set; }
    public int TongThuongKimCuong { get; set; }
    public int TongThuongXp { get; set; }
}

// Response danh sách thành tựu phân loại
public class DanhSachThanhTuuResponse
{
    public List<ThanhTuuVoiTrangThaiResponse> TatCaThanhTuu { get; set; } = [];
    public int TongThanhTuu { get; set; }
    public int SoThanhTuuDaDat { get; set; }
    public double PhanTramHoanThanh => TongThanhTuu > 0
        ? Math.Round((double)SoThanhTuuDaDat / TongThanhTuu * 100, 2)
        : 0;

    // Phân loại theo loại thành tựu
    public Dictionary<string, ThongKeThanhTuuTheoLoai> ThongKeTheoLoai { get; set; } = [];
}

// Thống kê thành tựu theo loại
public class ThongKeThanhTuuTheoLoai
{
    public int Tong { get; set; }
    public int DaDat { get; set; }
    public double PhanTram => Tong > 0 ? Math.Round((double)DaDat / Tong * 100, 2) : 0;
}