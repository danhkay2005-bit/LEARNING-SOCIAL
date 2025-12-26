using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class LichSuGiaoDich
{
    public int MaGiaoDich { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string LoaiGiaoDich { get; set; } = null!;

    public string LoaiTien { get; set; } = null!;

    public int SoLuong { get; set; }

    public int SoDuTruoc { get; set; }

    public int SoDuSau { get; set; }

    public string? MoTa { get; set; }

    public int? MaVatPham { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual VatPham? MaVatPhamNavigation { get; set; }
}
