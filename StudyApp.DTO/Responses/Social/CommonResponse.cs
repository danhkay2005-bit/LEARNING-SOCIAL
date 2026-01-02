using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response người dùng tóm tắt (dùng chung)
    public class NguoiDungTomTatResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string? TenHienThi { get; set; }
        public string? TenDangNhap { get; set; }
        public string? AnhDaiDien { get; set; }
        public bool DaXacThuc { get; set; }
        public bool DangOnline { get; set; }
    }

    // Response sticker
    public class StickerResponse
    {
        public int MaSticker { get; set; }
        public string? TenSticker { get; set; }
        public string? DuongDan { get; set; }
        public int? MaBoSticker { get; set; }
    }

    // Response bộ đề tóm tắt
    public class BoDeToTamResponse
    {
        public int MaBoDe { get; set; }
        public string? TenBoDe { get; set; }
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }
        public int SoCauHoi { get; set; }
        public int SoLuotLam { get; set; }
    }

    // Response thành tựu tóm tắt
    public class ThanhTuuTomTatResponse
    {
        public int MaThanhTuu { get; set; }
        public string? TenThanhTuu { get; set; }
        public string? MoTa { get; set; }
        public string? Icon { get; set; }
        public DateTime? ThoiGianDat { get; set; }
    }

    // Response thách đấu tóm tắt
    public class ThachDauTomTatResponse
    {
        public int MaThachDau { get; set; }
        public string? TenThachDau { get; set; }
        public NguoiDungTomTatResponse? NguoiThachDau { get; set; }
        public NguoiDungTomTatResponse? NguoiBiThachDau { get; set; }
        public string? TrangThai { get; set; }
        public int? DiemNguoiThachDau { get; set; }
        public int? DiemNguoiBiThachDau { get; set; }
    }

    // Response phân trang chung
    public class PhanTrangResponse
    {
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public int TongSoBanGhi { get; set; }
        public int SoBanGhiMoiTrang { get; set; }
        public bool CoTrangTruoc { get; set; }
        public bool CoTrangTiep { get; set; }
    }

    // Response API chung
    public class ApiResponse<T>
    {
        public bool ThanhCong { get; set; }
        public string? MaLoi { get; set; }
        public string? ThongBao { get; set; }
        public T? Data { get; set; }
        public DateTime ThoiGian { get; set; } = DateTime.UtcNow;
    }

    // Response API với phân trang
    public class ApiPhanTrangResponse<T> : ApiResponse<T>
    {
        public PhanTrangResponse? PhanTrang { get; set; }
    }
}