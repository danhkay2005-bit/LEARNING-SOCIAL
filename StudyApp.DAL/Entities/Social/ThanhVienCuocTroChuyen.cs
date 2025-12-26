using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ThanhVienCuocTroChuyen
{
    public int MaCuocTroChuyen { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string? BiDanh { get; set; }

    public string? VaiTro { get; set; }

    public int? SoTinChuaDoc { get; set; }

    public bool? TatThongBao { get; set; }

    public bool? GhimCuocTro { get; set; }

    public bool? DaRoiNhom { get; set; }

    public DateTime? ThoiGianThamGia { get; set; }

    public DateTime? ThoiGianXemCuoi { get; set; }

    public virtual CuocTroChuyen MaCuocTroChuyenNavigation { get; set; } = null!;
}
