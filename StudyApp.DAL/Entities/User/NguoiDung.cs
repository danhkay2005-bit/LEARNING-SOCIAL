using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class NguoiDung
{
    public Guid MaNguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhauMaHoa { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? HoVaTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public byte? GioiTinh { get; set; }

    public int? MaVaiTro { get; set; }

    public int? MaCapDo { get; set; }

    public string? HinhDaiDien { get; set; }

    public string? TieuSu { get; set; }

    public int? Vang { get; set; }

    public int? KimCuong { get; set; }

    public int? TongDiemXp { get; set; }

    public int? TongSoTheHoc { get; set; }

    public int? TongSoTheDung { get; set; }

    public int? TongThoiGianHocPhut { get; set; }

    public int? ChuoiNgayHocLienTiep { get; set; }

    public int? ChuoiNgayDaiNhat { get; set; }

    public int? SoStreakFreeze { get; set; }

    public int? SoStreakHoiSinh { get; set; }

    public DateOnly? NgayHoatDongCuoi { get; set; }

    public int? SoTranThachDau { get; set; }

    public int? SoTranThang { get; set; }

    public int? SoTranThua { get; set; }

    public bool? DaXacThucEmail { get; set; }

    public bool? TrangThaiOnline { get; set; }

    public DateTime? LanOnlineCuoi { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<BaoVeChuoiNgay> BaoVeChuoiNgays { get; set; } = new List<BaoVeChuoiNgay>();

    public virtual ICollection<DiemDanhHangNgay> DiemDanhHangNgays { get; set; } = new List<DiemDanhHangNgay>();

    public virtual ICollection<KhoNguoiDung> KhoNguoiDungs { get; set; } = new List<KhoNguoiDung>();

    public virtual ICollection<LichSuGiaoDich> LichSuGiaoDiches { get; set; } = new List<LichSuGiaoDich>();

    public virtual CapDo? MaCapDoNavigation { get; set; }

    public virtual VaiTro? MaVaiTroNavigation { get; set; }

    public virtual ICollection<ThanhTuuDatDuoc> ThanhTuuDatDuocs { get; set; } = new List<ThanhTuuDatDuoc>();

    public virtual ICollection<TienDoNhiemVu> TienDoNhiemVus { get; set; } = new List<TienDoNhiemVu>();
}
