using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class LichSuClone
{
    public int MaClone { get; set; }

    public int MaBoDeGoc { get; set; }

    public int MaBoDeClone { get; set; }

    public Guid MaNguoiClone { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BoDeHoc MaBoDeCloneNavigation { get; set; } = null!;

    public virtual BoDeHoc MaBoDeGocNavigation { get; set; } = null!;
}
