using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class ThongKeNgay
{
    public int MaThongKe { get; set; }

    public Guid MaNguoiDung { get; set; }

    public DateOnly NgayHoc { get; set; }

    public int? TongTheHoc { get; set; }

    public int? SoTheMoi { get; set; }

    public int? SoTheOnTap { get; set; }

    public int? SoTheDung { get; set; }

    public int? SoTheSai { get; set; }

    public double? TyLeDung { get; set; }

    public int? ThoiGianHocPhut { get; set; }

    public int? SoPhienHoc { get; set; }

    public int? SoBoDeHoc { get; set; }

    public int? Xpnhan { get; set; }

    public int? VangNhan { get; set; }

    public bool? DaHoanThanhMucTieu { get; set; }
}
