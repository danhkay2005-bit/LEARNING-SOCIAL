using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class MentionBinhLuan
{
    public int MaBinhLuan { get; set; }

    public Guid MaNguoiDuocMention { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BinhLuanBaiDang MaBinhLuanNavigation { get; set; } = null!;
}
