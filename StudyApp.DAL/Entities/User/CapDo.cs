using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class CapDo
{
    public int MaCapDo { get; set; }

    public string TenCapDo { get; set; } = null!;

    public string? BieuTuong { get; set; }

    public int MucXptoiThieu { get; set; }

    public int MucXptoiDa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
