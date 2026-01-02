using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response cấp độ
public class CapDoResponse
{
    public int MaCapDo { get; set; }
    public string TenCapDo { get; set; } = null!;
    public string? BieuTuong { get; set; }
    public int MucXptoiThieu { get; set; }
    public int MucXptoiDa { get; set; }
}

// Response tiến độ cấp độ người dùng
public class TienDoCapDoResponse
{
    public CapDoResponse CapDoHienTai { get; set; } = null!;
    public CapDoResponse? CapDoTiepTheo { get; set; }
    public int XpHienTai { get; set; }
    public int XpCanThemDeLen => CapDoTiepTheo != null
        ? CapDoTiepTheo.MucXptoiThieu - XpHienTai
        : 0;
    public double PhanTramTienDo
    {
        get
        {
            if (CapDoTiepTheo == null) return 100;
            int tongXpCanThiet = CapDoHienTai.MucXptoiDa - CapDoHienTai.MucXptoiThieu;
            int xpDaDat = XpHienTai - CapDoHienTai.MucXptoiThieu;
            return tongXpCanThiet > 0
                ? Math.Round((double)xpDaDat / tongXpCanThiet * 100, 2)
                : 100;
        }
    }
}

// Response lên cấp
public class LenCapResponse
{
    public bool DaLenCap { get; set; }
    public CapDoResponse? CapDoMoi { get; set; }
    public CapDoResponse? CapDoCu { get; set; }
    public List<PhanThuongLenCapResponse> PhanThuongs { get; set; } = [];
}

// Response phần thưởng lên cấp
public class PhanThuongLenCapResponse
{
    public string LoaiThuong { get; set; } = null!;
    public int SoLuong { get; set; }
    public string? MoTa { get; set; }
}