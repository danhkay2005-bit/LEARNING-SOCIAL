using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class TienDoHocTap
{
    public int MaTienDo { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaThe { get; set; }

    public byte? TrangThai { get; set; }

    public double? HeSoDe { get; set; }

    public int? KhoangCachNgay { get; set; }

    public int? SoLanLap { get; set; }

    public DateTime? NgayOnTapTiepTheo { get; set; }

    public int? SoLanDung { get; set; }

    public int? SoLanSai { get; set; }

    public double? TyLeDung { get; set; }

    public int? ThoiGianTraLoiTbgiay { get; set; }

    public byte? DoKhoCanNhan { get; set; }

    public DateTime? LanHocCuoi { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
