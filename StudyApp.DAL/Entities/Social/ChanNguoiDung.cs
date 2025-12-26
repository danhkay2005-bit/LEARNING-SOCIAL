using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ChanNguoiDung
{
    public Guid MaNguoiChan { get; set; }

    public Guid MaNguoiBiChan { get; set; }

    public string? LyDo { get; set; }

    public DateTime? ThoiGian { get; set; }
}
