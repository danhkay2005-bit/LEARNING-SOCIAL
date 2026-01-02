using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response lịch sử hoạt động
public class LichSuHoatDongResponse
{
    public int MaHoatDong { get; set; }
    public LoaiHoatDongEnum LoaiHoatDong { get; set; }
    public string TenLoaiHoatDong => LoaiHoatDong switch
    {
        LoaiHoatDongEnum.DangNhap => "Đăng nhập",
        LoaiHoatDongEnum.DangXuat => "Đăng xuất",
        LoaiHoatDongEnum.HocThe => "Học thẻ",
        LoaiHoatDongEnum.TaoBoDe => "Tạo bộ đề",
        LoaiHoatDongEnum.DatThanhTuu => "Đạt thành tựu",
        LoaiHoatDongEnum.LenLevel => "Lên level",
        LoaiHoatDongEnum.ThachDau => "Thách đấu",
        LoaiHoatDongEnum.ChuoiNgay => "Chuỗi ngày",
        LoaiHoatDongEnum.DiemDanh => "Điểm danh",
        LoaiHoatDongEnum.MuaVatPham => "Mua vật phẩm",
        LoaiHoatDongEnum.NhanThuong => "Nhận thưởng",
        LoaiHoatDongEnum.ChiaSeBoDe => "Chia sẻ bộ đề",
        LoaiHoatDongEnum.DangKy => "Đăng ký",
        _ => "Hoạt động khác"
    };
    public string? MoTa { get; set; }
    public string? ChiTiet { get; set; }
    public int? DiemXpNhan { get; set; }
    public DateTime? ThoiGian { get; set; }
    public string ThoiGianTuongDoi => FormatThoiGianTuongDoi(ThoiGian);

    private static string FormatThoiGianTuongDoi(DateTime? thoiGian)
    {
        if (thoiGian == null) return "";
        var khoangCach = DateTime.Now - thoiGian.Value;

        if (khoangCach.TotalMinutes < 1) return "Vừa xong";
        if (khoangCach.TotalMinutes < 60) return $"{(int)khoangCach.TotalMinutes} phút trước";
        if (khoangCach.TotalHours < 24) return $"{(int)khoangCach.TotalHours} giờ trước";
        if (khoangCach.TotalDays < 7) return $"{(int)khoangCach.TotalDays} ngày trước";
        if (khoangCach.TotalDays < 30) return $"{(int)(khoangCach.TotalDays / 7)} tuần trước";
        return thoiGian.Value.ToString("dd/MM/yyyy");
    }
}

// Response danh sách lịch sử hoạt động
public class DanhSachLichSuHoatDongResponse
{
    public List<LichSuHoatDongResponse> HoatDongs { get; set; } = [];
    public int TongHoatDong { get; set; }
    public int TongTrang { get; set; }
    public int TrangHienTai { get; set; }
}