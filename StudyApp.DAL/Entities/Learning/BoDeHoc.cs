using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class BoDeHoc
{
    public int MaBoDe { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int? MaChuDe { get; set; }

    public int? MaThuMuc { get; set; }

    public string TieuDe { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? AnhBia { get; set; }

    public byte? MucDoKho { get; set; }

    public bool? LaCongKhai { get; set; }

    public bool? ChoPhepBinhLuan { get; set; }

    public int? MaBoDeGoc { get; set; }

    public int? SoLuongThe { get; set; }

    public int? SoLuotHoc { get; set; }

    public int? SoLuotChiaSe { get; set; }

    public double? DiemDanhGiaTb { get; set; }

    public int? SoDanhGia { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual ICollection<BaoCaoBoDe> BaoCaoBoDes { get; set; } = new List<BaoCaoBoDe>();

    public virtual ICollection<BinhLuanBoDe> BinhLuanBoDes { get; set; } = new List<BinhLuanBoDe>();

    public virtual ICollection<BoDeYeuThich> BoDeYeuThiches { get; set; } = new List<BoDeYeuThich>();

    public virtual ICollection<DanhGiaBoDe> DanhGiaBoDes { get; set; } = new List<DanhGiaBoDe>();

    public virtual ICollection<BoDeHoc> InverseMaBoDeGocNavigation { get; set; } = new List<BoDeHoc>();

    public virtual ICollection<LichSuClone> LichSuCloneMaBoDeCloneNavigations { get; set; } = new List<LichSuClone>();

    public virtual ICollection<LichSuClone> LichSuCloneMaBoDeGocNavigations { get; set; } = new List<LichSuClone>();

    public virtual ICollection<LichSuHocBoDe> LichSuHocBoDes { get; set; } = new List<LichSuHocBoDe>();

    public virtual BoDeHoc? MaBoDeGocNavigation { get; set; }

    public virtual ChuDe? MaChuDeNavigation { get; set; }

    public virtual ThuMuc? MaThuMucNavigation { get; set; }

    public virtual ICollection<PhienHoc> PhienHocs { get; set; } = new List<PhienHoc>();

    public virtual ICollection<TagBoDe> TagBoDes { get; set; } = new List<TagBoDe>();

    public virtual ICollection<TheFlashcard> TheFlashcards { get; set; } = new List<TheFlashcard>();
}
