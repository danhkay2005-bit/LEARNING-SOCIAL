using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class VatPham
{
    public int MaVatPham { get; set; }

    public string TenVatPham { get; set; } = null!;

    public string? MoTa { get; set; }

    public int Gia { get; set; }

    public int LoaiTienTe { get; set; }

    public int MaDanhMuc { get; set; }

    public string? DuongDanHinh { get; set; }

    public string? DuongDanFile { get; set; }

    public int? ThoiHanPhut { get; set; }

    public double? GiaTriHieuUng { get; set; }

    public byte? DoHiem { get; set; }

    public int? SoLuongBanRa { get; set; }

    public bool? ConHang { get; set; }

    public int? GioiHanSoLuong { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<BoostDangHoatDong> BoostDangHoatDongs { get; set; } = new List<BoostDangHoatDong>();

    public virtual ICollection<KhoNguoiDung> KhoNguoiDungs { get; set; } = new List<KhoNguoiDung>();

    public virtual ICollection<LichSuGiaoDich> LichSuGiaoDiches { get; set; } = new List<LichSuGiaoDich>();

    public virtual DanhMucSanPham MaDanhMucNavigation { get; set; } = null!;

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaAvatarDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaBadgeDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaHieuUngDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaHinhNenDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaKhungDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaNhacNenDangDungNavigations { get; set; } = new List<TuyChinhProfile>();

    public virtual ICollection<TuyChinhProfile> TuyChinhProfileMaThemeDangDungNavigations { get; set; } = new List<TuyChinhProfile>();
}
