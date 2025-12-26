using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class DanhDauThe
{
    public int MaDanhDau { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaThe { get; set; }

    public string? LoaiDanhDau { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
