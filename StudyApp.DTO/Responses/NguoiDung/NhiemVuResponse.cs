using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response nhiệm vụ
public class NhiemVuResponse
{
    public int MaNhiemVu { get; set; }
    public string TenNhiemVu { get; set; } = null!;
    public string? MoTa { get; set; }
    public string? BieuTuong { get; set; }
    public LoaiNhiemVuEnum? LoaiNhiemVu { get; set; }
    public string TenLoaiNhiemVu => LoaiNhiemVu switch
    {
        LoaiNhiemVuEnum.HangNgay => "Hàng ngày",
        LoaiNhiemVuEnum.HangTuan => "Hàng tuần",
        LoaiNhiemVuEnum.ThanhTuu => "Thành tựu",
        LoaiNhiemVuEnum.SuKien => "Sự kiện",
        _ => "Khác"
    };
    public LoaiDieuKienEnum? LoaiDieuKien { get; set; }
    public string TenLoaiDieuKien => LoaiDieuKien switch
    {
        LoaiDieuKienEnum.HocThe => "Học thẻ",
        LoaiDieuKienEnum.ThachDau => "Thách đấu",
        LoaiDieuKienEnum.DiemDanh => "Điểm danh",
        LoaiDieuKienEnum.MuaVatPham => "Mua vật phẩm",
        LoaiDieuKienEnum.TuongTac => "Tương tác",
        LoaiDieuKienEnum.ChiaSeBoDe => "Chia sẻ bộ đề",
        LoaiDieuKienEnum.LenLevel => "Lên level",
        _ => "Khác"
    };
    public int DieuKienDatDuoc { get; set; }
    public int? ThuongVang { get; set; }
    public int? ThuongKimCuong { get; set; }
    public int? ThuongXp { get; set; }
    public DateOnly? NgayBatDau { get; set; }
    public DateOnly? NgayKetThuc { get; set; }
    public bool? ConHieuLuc { get; set; }
}

// Response nhiệm vụ với tiến độ người dùng
public class NhiemVuVoiTienDoResponse : NhiemVuResponse
{
    public int TienDoHienTai { get; set; }
    public double PhanTramHoanThanh => DieuKienDatDuoc > 0
        ? Math.Round((double)TienDoHienTai / DieuKienDatDuoc * 100, 2)
        : 0;
    public bool DaHoanThanh { get; set; }
    public bool DaNhanThuong { get; set; }
    public DateOnly? NgayBatDauThamGia { get; set; }
    public DateTime? NgayHoanThanh { get; set; }
}

// Response tiến độ nhiệm vụ
public class TienDoNhiemVuResponse
{
    public Guid MaNguoiDung { get; set; }
    public int MaNhiemVu { get; set; }
    public string TenNhiemVu { get; set; } = null!;
    public int? TienDoHienTai { get; set; }
    public int DieuKienDatDuoc { get; set; }
    public double PhanTramHoanThanh => DieuKienDatDuoc > 0
        ? Math.Round((double)(TienDoHienTai ?? 0) / DieuKienDatDuoc * 100, 2)
        : 0;
    public bool? DaHoanThanh { get; set; }
    public bool? DaNhanThuong { get; set; }
    public DateOnly? NgayBatDau { get; set; }
    public DateTime? NgayHoanThanh { get; set; }
}

// Response nhận thưởng nhiệm vụ
public class NhanThuongNhiemVuResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public int? VangNhanDuoc { get; set; }
    public int? KimCuongNhanDuoc { get; set; }
    public int? XpNhanDuoc { get; set; }
    public int? VangHienTai { get; set; }
    public int? KimCuongHienTai { get; set; }
    public int? TongXpHienTai { get; set; }
}

// Response danh sách nhiệm vụ theo loại
public class DanhSachNhiemVuResponse
{
    public List<NhiemVuVoiTienDoResponse> NhiemVuHangNgay { get; set; } = [];
    public List<NhiemVuVoiTienDoResponse> NhiemVuHangTuan { get; set; } = [];
    public List<NhiemVuVoiTienDoResponse> NhiemVuSuKien { get; set; } = [];
    public int TongNhiemVuHoanThanh { get; set; }
    public int TongNhiemVu { get; set; }
    public double PhanTramHoanThanhTongThe => TongNhiemVu > 0
        ? Math.Round((double)TongNhiemVuHoanThanh / TongNhiemVu * 100, 2)
        : 0;
}