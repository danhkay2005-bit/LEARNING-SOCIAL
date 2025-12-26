using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class TuDienKhuyet
{
    public int MaTuDienKhuyet { get; set; }

    public int MaThe { get; set; }

    public string TuCanDien { get; set; } = null!;

    public int ViTriTrongCau { get; set; }

    public string? GoiY { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
