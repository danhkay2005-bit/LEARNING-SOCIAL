using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class GhiChuThe
{
    public int MaGhiChu { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaThe { get; set; }

    public string? NoiDung { get; set; }

    public string? MauNen { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianSua { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
