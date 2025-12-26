using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class PhanTuSapXep
{
    public int MaPhanTu { get; set; }

    public int MaThe { get; set; }

    public string NoiDung { get; set; } = null!;

    public int ThuTuDung { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
