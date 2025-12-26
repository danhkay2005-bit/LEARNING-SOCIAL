using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class BoDeYeuThich
{
    public Guid MaNguoiDung { get; set; }

    public int MaBoDe { get; set; }

    public DateTime? ThoiGianLuu { get; set; }

    public string? GhiChu { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;
}
