using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class ThanhTuu
{
    public int MaThanhTuu { get; set; }

    public string TenThanhTuu { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }

    public string? HinhHuy { get; set; }

    public string? LoaiThanhTuu { get; set; }

    public string? DieuKienLoai { get; set; }

    public int DieuKienGiaTri { get; set; }

    public int? ThuongXp { get; set; }

    public int? ThuongVang { get; set; }

    public int? ThuongKimCuong { get; set; }

    public byte? DoHiem { get; set; }

    public bool? BiAn { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<ThanhTuuDatDuoc> ThanhTuuDatDuocs { get; set; } = new List<ThanhTuuDatDuoc>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfiles { get; set; } = new List<TuyChinhProfile>();
}
