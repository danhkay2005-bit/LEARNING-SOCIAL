using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class DanhMucSanPham
{
    public int MaDanhMuc { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string? BieuTuong { get; set; }

    public string? MoTa { get; set; }

    public int? ThuTuHienThi { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<VatPham> VatPhams { get; set; } = new List<VatPham>();
}
