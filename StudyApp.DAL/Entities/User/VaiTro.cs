using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class VaiTro
{
    public int MaVaiTro { get; set; }

    public string TenVaiTro { get; set; } = null!;

    public string? MoTa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
