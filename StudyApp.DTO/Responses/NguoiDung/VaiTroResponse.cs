using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response vai trò
public class VaiTroResponse
{
    public int MaVaiTro { get; set; }
    public string TenVaiTro { get; set; } = null!;
    public string? MoTa { get; set; }
}

// Response vai trò chi tiết (Admin)
public class VaiTroChiTietResponse : VaiTroResponse
{
    public DateTime? ThoiGianTao { get; set; }
    public int SoNguoiDung { get; set; }
}