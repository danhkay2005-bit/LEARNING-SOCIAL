using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class ThichBinhLuanBoDe
{
    public Guid MaNguoiDung { get; set; }

    public int MaBinhLuan { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BinhLuanBoDe MaBinhLuanNavigation { get; set; } = null!;
}
