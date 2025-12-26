using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class ThuMuc
{
    public int MaThuMuc { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string TenThuMuc { get; set; } = null!;

    public string? MoTa { get; set; }

    public int? MaThuMucCha { get; set; }

    public int? ThuTu { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual ICollection<BoDeHoc> BoDeHocs { get; set; } = new List<BoDeHoc>();

    public virtual ICollection<ThuMuc> InverseMaThuMucChaNavigation { get; set; } = new List<ThuMuc>();

    public virtual ThuMuc? MaThuMucChaNavigation { get; set; }
}
