using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class Hashtag
{
    public int MaHashtag { get; set; }

    public string TenHashtag { get; set; } = null!;

    public int? SoLuotDung { get; set; }

    public bool? DangThinhHanh { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<BaiDang> MaBaiDangs { get; set; } = new List<BaiDang>();
}
