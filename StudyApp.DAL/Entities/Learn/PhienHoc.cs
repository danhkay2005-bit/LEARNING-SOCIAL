using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class PhienHoc
{
    public int MaPhien { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int? MaBoDe { get; set; }

    public int? MaThachDau { get; set; }

    public string? LoaiPhien { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime? ThoiGianKetThuc { get; set; }

    public int? ThoiGianHocGiay { get; set; }

    public int? TongSoThe { get; set; }

    public int? SoTheMoi { get; set; }

    public int? SoTheOnTap { get; set; }

    public int? SoTheDung { get; set; }

    public int? SoTheSai { get; set; }

    public double? TyLeDung { get; set; }

    public virtual ICollection<ChiTietTraLoi> ChiTietTraLois { get; set; } = new List<ChiTietTraLoi>();

    public virtual ICollection<LichSuHocBoDe> LichSuHocBoDes { get; set; } = new List<LichSuHocBoDe>();

    public virtual BoDeHoc? MaBoDeNavigation { get; set; }

    public virtual ThachDau? MaThachDauNavigation { get; set; }
}
