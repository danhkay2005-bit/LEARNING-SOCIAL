using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Entities
{
    public class ThanhTuuDto
    {
        public int MaThanhTuu { get; set; }
        public string? TenThanhTuu { get; set; }
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }
        public string? HinhHuy { get; set; }
        public string? LoaiThanhTuu { get; set; }  // 'HocTap', etc.
        public string? DieuKienLoai { get; set; }
        public int DieuKienGiaTri { get; set; }
        public int ThuongXP { get; set; }
        public int ThuongVang { get; set; }
        public int ThuongKimCuong { get; set; }
        public int DoHiem { get; set; }
        public bool BiAn { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }

    /// <summary>
    /// DTO cho bảng NhiemVu
    /// </summary>
    public class NhiemVuDto
    {
        public int MaNhiemVu { get; set; }
        public string? TenNhiemVu { get; set; }
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }
        public string? LoaiNhiemVu { get; set; }  // 'HangNgay', etc.
        public string? LoaiDieuKien { get; set; }
        public int DieuKienDatDuoc { get; set; }
        public int ThuongVang { get; set; }
        public int ThuongKimCuong { get; set; }
        public int ThuongXP { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public bool ConHieuLuc { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }

    /// <summary>
    /// DTO cho bảng DiemDanhHangNgay
    /// </summary>
    public class DiemDanhHangNgayDto
    {
        public int MaDiemDanh { get; set; }
        public Guid MaNguoiDung { get; set; }
        public DateTime NgayDiemDanh { get; set; }
        public int NgayThuMay { get; set; }
        public int ThuongVang { get; set; }
        public int ThuongXP { get; set; }
        public string? ThuongDacBiet { get; set; }
    }

    public class BaoVeDto
    {
        public LoaiBaoVeStreakEnum LoaiBaoVe { get; set; }
        public int ChuoiNgaySau { get; set; }
    }
}
