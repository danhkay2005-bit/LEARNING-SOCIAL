using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class DapAnTracNghiem
{
    public int MaDapAn { get; set; }

    public int MaThe { get; set; }

    public string NoiDung { get; set; } = null!;

    public bool? LaDapAnDung { get; set; }

    public int? ThuTu { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
