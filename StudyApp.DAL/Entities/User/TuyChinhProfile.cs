using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class TuyChinhProfile
{
    public Guid MaNguoiDung { get; set; }

    public int? MaAvatarDangDung { get; set; }

    public int? MaKhungDangDung { get; set; }

    public int? MaHinhNenDangDung { get; set; }

    public int? MaHieuUngDangDung { get; set; }

    public int? MaThemeDangDung { get; set; }

    public int? MaNhacNenDangDung { get; set; }

    public int? MaBadgeDangDung { get; set; }

    public int? MaHuyHieuHienThi { get; set; }

    public string? CauChamNgon { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public virtual VatPham? MaAvatarDangDungNavigation { get; set; }

    public virtual VatPham? MaBadgeDangDungNavigation { get; set; }

    public virtual VatPham? MaHieuUngDangDungNavigation { get; set; }

    public virtual VatPham? MaHinhNenDangDungNavigation { get; set; }

    public virtual ThanhTuu? MaHuyHieuHienThiNavigation { get; set; }

    public virtual VatPham? MaKhungDangDungNavigation { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual VatPham? MaNhacNenDangDungNavigation { get; set; }

    public virtual VatPham? MaThemeDangDungNavigation { get; set; }
}
