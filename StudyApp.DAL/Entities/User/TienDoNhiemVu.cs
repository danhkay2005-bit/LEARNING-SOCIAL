using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class TienDoNhiemVu
{
    public Guid MaNguoiDung { get; set; }

    public int MaNhiemVu { get; set; }

    public int? TienDoHienTai { get; set; }

    public bool? DaHoanThanh { get; set; }

    public bool? DaNhanThuong { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateTime? NgayHoanThanh { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual NhiemVu MaNhiemVuNavigation { get; set; } = null!;
}
