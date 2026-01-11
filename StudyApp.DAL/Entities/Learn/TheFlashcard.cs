using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class TheFlashcard
{
    public int MaThe { get; set; }

    public int MaBoDe { get; set; }

    public string? LoaiThe { get; set; }

    public string MatTruoc { get; set; } = null!;

    public string MatSau { get; set; } = null!;

    public string? GiaiThich { get; set; }

    public string? HinhAnhTruoc { get; set; }

    public string? HinhAnhSau { get; set; }

    public int? ThuTu { get; set; }

    public byte? DoKho { get; set; }

    public int? SoLuongHoc { get; set; }

    public int? SoLanDung { get; set; }

    public int? SoLanSai { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual ICollection<CapGhep> CapGheps { get; set; } = new List<CapGhep>();

    public virtual ICollection<DapAnTracNghiem> DapAnTracNghiems { get; set; } = new List<DapAnTracNghiem>();

    public virtual ICollection<LogsGenerateAi> LogsGenerateAis { get; set; } = new List<LogsGenerateAi>();

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual ICollection<PhanTuSapXep> PhanTuSapXeps { get; set; } = new List<PhanTuSapXep>();

    public virtual ICollection<TienDoHocTap> TienDoHocTaps { get; set; } = new List<TienDoHocTap>();

    public virtual ICollection<TuDienKhuyet> TuDienKhuyets { get; set; } = new List<TuDienKhuyet>();
}
