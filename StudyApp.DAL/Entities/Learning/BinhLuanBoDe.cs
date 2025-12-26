using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class BinhLuanBoDe
{
    public int MaBinhLuan { get; set; }

    public int MaBoDe { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string NoiDung { get; set; } = null!;

    public int? MaBinhLuanCha { get; set; }

    public int? SoLuotThich { get; set; }

    public bool? DaChinhSua { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGian { get; set; }

    public DateTime? ThoiGianSua { get; set; }

    public virtual ICollection<BinhLuanBoDe> InverseMaBinhLuanChaNavigation { get; set; } = new List<BinhLuanBoDe>();

    public virtual BinhLuanBoDe? MaBinhLuanChaNavigation { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual ICollection<ThichBinhLuanBoDe> ThichBinhLuanBoDes { get; set; } = new List<ThichBinhLuanBoDe>();
}
