using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class ThachDauNguoiChoi
{
    public int MaThachDau { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int? Diem { get; set; }

    public int? SoTheDung { get; set; }

    public int? SoTheSai { get; set; }

    public int? ThoiGianLamBaiGiay { get; set; }

    public bool? LaNguoiThang { get; set; }

    public virtual ThachDau MaThachDauNavigation { get; set; } = null!;
}
