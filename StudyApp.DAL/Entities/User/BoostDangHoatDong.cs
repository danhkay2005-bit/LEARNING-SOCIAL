using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class BoostDangHoatDong
{
    public int MaBoost { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaVatPham { get; set; }

    public string? LoaiBoost { get; set; }

    public double HeSoNhan { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public bool? ConHieuLuc { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual VatPham MaVatPhamNavigation { get; set; } = null!;
}
