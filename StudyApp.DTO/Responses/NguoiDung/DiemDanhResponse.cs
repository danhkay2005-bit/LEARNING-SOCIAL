using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response điểm danh hàng ngày
public class DiemDanhResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
    public int NgayThuMay { get; set; }
    public int? ThuongVang { get; set; }
    public int? ThuongXp { get; set; }
    public string? ThuongDacBiet { get; set; }
    public bool DaNhanThuongDacBiet { get; set; }
    public int VangHienTai { get; set; }
    public int XpHienTai { get; set; }
}

// Response lịch sử điểm danh
public class LichSuDiemDanhResponse
{
    public DateOnly NgayDiemDanh { get; set; }
    public int? NgayThuMay { get; set; }
    public int? ThuongVang { get; set; }
    public int? ThuongXp { get; set; }
    public string? ThuongDacBiet { get; set; }
}

// Response trạng thái điểm danh
public class TrangThaiDiemDanhResponse
{
    public bool DaDiemDanhHomNay { get; set; }
    public int NgayLienTiepHienTai { get; set; }
    public DateOnly? NgayDiemDanhGanNhat { get; set; }
    public List<CauHinhDiemDanhResponse> CauHinhTuan { get; set; } = [];
    public int NgayTiepTheo { get; set; }
    public int? ThuongVangTiepTheo { get; set; }
    public int? ThuongXpTiepTheo { get; set; }
    public string? ThuongDacBietTiepTheo { get; set; }
}

// Response cấu hình điểm danh
public class CauHinhDiemDanhResponse
{
    public int NgayThu { get; set; }
    public int? ThuongVang { get; set; }
    public int? ThuongXp { get; set; }
    public string? ThuongDacBiet { get; set; }
    public bool DaDiemDanh { get; set; }
}