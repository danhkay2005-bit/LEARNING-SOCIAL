using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class ChuDe
{
    public int MaChuDe { get; set; }

    public string TenChuDe { get; set; } = null!;

    public string? MoTa { get; set; }

    public int? SoLuotDung { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<BoDeHoc> BoDeHocs { get; set; } = new List<BoDeHoc>();
}
