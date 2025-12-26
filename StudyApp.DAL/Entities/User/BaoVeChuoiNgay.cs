using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class BaoVeChuoiNgay
{
    public int MaBaoVe { get; set; }

    public Guid MaNguoiDung { get; set; }

    public DateOnly NgaySuDung { get; set; }

    public string? LoaiBaoVe { get; set; }

    public int? ChuoiNgayTruocKhi { get; set; }

    public int? ChuoiNgaySauKhi { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
