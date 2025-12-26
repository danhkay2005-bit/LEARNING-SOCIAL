using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class KhoNguoiDung
{
    public int MaKho { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int MaVatPham { get; set; }

    public int? SoLuong { get; set; }

    public bool? DaTrangBi { get; set; }

    public DateTime? ThoiGianMua { get; set; }

    public DateTime? ThoiGianHetHan { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual VatPham MaVatPhamNavigation { get; set; } = null!;
}
