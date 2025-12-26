using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Entities
{
    public class NguoiDungDTO
    {
        public Guid MaNguoiDung { get; set; }
        public string? TenDangNhap { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; }  // 0: Khác, 1: Nam, 2: Nữ
        public int? MaVaiTro { get; set; }
        public int? MaCapDo { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? AnhBia { get; set; }
        public string? TieuSu { get; set; }
        public int? Vang { get; set; }
        public int? KimCuong { get; set; }
        public int? TongDiemXP { get; set; }
        public int? TongSoTheHoc { get; set; }
        public int? TongSoTheDung { get; set; }
        public int? TongThoiGianHocPhut { get; set; }
        public int? ChuoiNgayHocLienTiep { get; set; }
        public int? ChuoiNgayDaiNhat { get; set; }
        public int? SoStreakFreeze { get; set; }
        public int? SoStreakHoiSinh { get; set; }
        public DateTime? NgayHoatDongCuoi { get; set; }
        public int? SoTranThachDau { get; set; }
        public int? SoTranThang { get; set; }
        public int? SoTranThua { get; set; }
        public bool? DaXacThucEmail { get; set; }
        public bool? TrangThaiOnline { get; set; }
        public bool? DaXoa { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }
}
