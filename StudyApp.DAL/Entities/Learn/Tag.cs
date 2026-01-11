using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class Tag
{
    public int MaTag { get; set; }

    public string TenTag { get; set; } = null!;

    public int? SoLuotDung { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<TagBoDe> TagBoDes { get; set; } = new List<TagBoDe>();
}
