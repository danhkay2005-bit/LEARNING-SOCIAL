using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class MucTieuCaNhan
{
    public int MaMucTieu { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string LoaiMucTieu { get; set; } = null!;

    public string? TenMucTieu { get; set; }

    public int GiaTriMucTieu { get; set; }

    public int? GiaTriHienTai { get; set; }

    public string? DonVi { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public bool? DaHoanThanh { get; set; }

    public DateOnly? NgayHoanThanh { get; set; }

    public DateTime? ThoiGianTao { get; set; }
}
