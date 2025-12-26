using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class BaoCaoBoDe
{
    public int MaBaoCao { get; set; }

    public int MaBoDe { get; set; }

    public Guid MaNguoiBaoCao { get; set; }

    public string LyDo { get; set; } = null!;

    public string? MoTaChiTiet { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;
}
