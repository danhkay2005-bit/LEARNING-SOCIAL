using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class CaiDatNguoiDung
{
    public Guid MaNguoiDung { get; set; }

    public bool? CheDoToi { get; set; }

    public bool? CoHieuUng { get; set; }

    public bool? ThongBaoNhacHoc { get; set; }

    public TimeOnly? GioNhacHoc { get; set; }

    public bool? ThongBaoThanhTuu { get; set; }

    public bool? ThongBaoXaHoi { get; set; }

    public bool? ThongBaoThachDau { get; set; }

    public bool? HienThiTrangThai { get; set; }

    public bool? HienThiThongKe { get; set; }

    public bool? ChoPhepThachDau { get; set; }

    public bool? ChoPhepNhanTin { get; set; }

    public bool? ChoPhepTagTrongBaiDang { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
