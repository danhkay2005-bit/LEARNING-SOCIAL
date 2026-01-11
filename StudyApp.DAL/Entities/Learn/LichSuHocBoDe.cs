using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class LichSuHocBoDe
{
    public int MaLichSu { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaBoDe { get; set; }

    public int? MaPhien { get; set; }

    public int? SoTheHoc { get; set; }

    public double? TyLeDung { get; set; }

    public int? ThoiGianHocPhut { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual PhienHoc? MaPhienNavigation { get; set; }
}
