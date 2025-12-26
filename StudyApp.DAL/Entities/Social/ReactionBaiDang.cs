using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ReactionBaiDang
{
    public int MaBaiDang { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string? LoaiReaction { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BaiDang MaBaiDangNavigation { get; set; } = null!;
}
