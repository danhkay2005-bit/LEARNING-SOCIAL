using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class TheFlashcard
{
    public int MaThe { get; set; }

    public int MaBoDe { get; set; }

    public string? LoaiThe { get; set; }

    public string MatTruoc { get; set; } = null!;

    public string MatSau { get; set; } = null!;

    public string? GiaiThich { get; set; }

    public string? GoiY { get; set; }

    public string? VietTat { get; set; }

    public string? HinhAnhTruoc { get; set; }

    public string? HinhAnhSau { get; set; }

    public string? AmThanhTruoc { get; set; }

    public string? AmThanhSau { get; set; }

    public int? ThuTu { get; set; }

    public byte? DoKho { get; set; }

    public int? SoLuotHoc { get; set; }

    public int? SoLanDung { get; set; }

    public int? SoLanSai { get; set; }

    public double? TyLeDungTb { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual ICollection<CapGhep> CapGheps { get; set; } = new List<CapGhep>();

    public virtual ICollection<ChiTietTraLoi> ChiTietTraLois { get; set; } = new List<ChiTietTraLoi>();

    public virtual ICollection<DanhDauThe> DanhDauThes { get; set; } = new List<DanhDauThe>();

    public virtual ICollection<DapAnTracNghiem> DapAnTracNghiems { get; set; } = new List<DapAnTracNghiem>();

    public virtual ICollection<GhiChuThe> GhiChuThes { get; set; } = new List<GhiChuThe>();

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual ICollection<PhanTuSapXep> PhanTuSapXeps { get; set; } = new List<PhanTuSapXep>();

    public virtual ICollection<TienDoHocTap> TienDoHocTaps { get; set; } = new List<TienDoHocTap>();

    public virtual ICollection<TuDienKhuyet> TuDienKhuyets { get; set; } = new List<TuDienKhuyet>();
}
