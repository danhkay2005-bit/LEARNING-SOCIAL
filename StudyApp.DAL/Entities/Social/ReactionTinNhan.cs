using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ReactionTinNhan
{
    public int MaTinNhan { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string Emoji { get; set; } = null!;

    public DateTime? ThoiGian { get; set; }

    public virtual TinNhan MaTinNhanNavigation { get; set; } = null!;
}
