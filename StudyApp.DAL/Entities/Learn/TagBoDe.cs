using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class TagBoDe
{
    public int MaBoDe { get; set; }

    public int MaTag { get; set; }

    public DateTime? ThoiGianThem { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual Tag MaTagNavigation { get; set; } = null!;
}
