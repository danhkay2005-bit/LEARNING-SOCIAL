using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class NhiemVu
{
    public int MaNhiemVu { get; set; }

    public string TenNhiemVu { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? BieuTuong { get; set; }

    public string? LoaiNhiemVu { get; set; }

    public string? LoaiDieuKien { get; set; }

    public int DieuKienDatDuoc { get; set; }

    public int? ThuongVang { get; set; }

    public int? ThuongKimCuong { get; set; }

    public int? ThuongXp { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public bool? ConHieuLuc { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<TienDoNhiemVu> TienDoNhiemVus { get; set; } = new List<TienDoNhiemVu>();
}
