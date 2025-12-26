using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class ThanhTuuDatDuoc
{
    public Guid MaNguoiDung { get; set; }

    public int MaThanhTuu { get; set; }

    public DateTime? NgayDat { get; set; }

    public bool? DaXem { get; set; }

    public bool? DaChiaSe { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual ThanhTuu MaThanhTuuNavigation { get; set; } = null!;
}
