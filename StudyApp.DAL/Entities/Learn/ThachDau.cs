using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class ThachDau
{
    public int MaThachDau { get; set; }

    public int MaBoDe { get; set; }

    public Guid NguoiTao { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime? ThoiGianKetThuc { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;

    public virtual ICollection<PhienHoc> PhienHocs { get; set; } = new List<PhienHoc>();

    public virtual ICollection<ThachDauNguoiChoi> ThachDauNguoiChois { get; set; } = new List<ThachDauNguoiChoi>();
}
