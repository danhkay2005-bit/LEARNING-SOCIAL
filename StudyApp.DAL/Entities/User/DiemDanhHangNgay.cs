using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class DiemDanhHangNgay
{
    public int MaDiemDanh { get; set; }

    public Guid MaNguoiDung { get; set; }

    public DateOnly NgayDiemDanh { get; set; }

    public int? NgayThuMay { get; set; }

    public int? ThuongVang { get; set; }

    public int? ThuongXp { get; set; }

    public string? ThuongDacBiet { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
