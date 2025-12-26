using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ThichBinhLuan
{
    public int MaBinhLuan { get; set; }

    public Guid MaNguoiDung { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BinhLuanBaiDang MaBinhLuanNavigation { get; set; } = null!;
}
