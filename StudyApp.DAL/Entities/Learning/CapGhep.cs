using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class CapGhep
{
    public int MaCap { get; set; }

    public int MaThe { get; set; }

    public string VeTrai { get; set; } = null!;

    public string VePhai { get; set; } = null!;

    public int? ThuTu { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
