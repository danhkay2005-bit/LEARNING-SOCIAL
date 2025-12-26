using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class DaXemTinNhan
{
    public int MaTinNhan { get; set; }

    public Guid MaNguoiXem { get; set; }

    public DateTime? ThoiGianXem { get; set; }

    public virtual TinNhan MaTinNhanNavigation { get; set; } = null!;
}
