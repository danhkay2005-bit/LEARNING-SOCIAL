using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response phiên học chi tiết
    public class PhienHocResponse
    {
        public int MaPhien { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int? MaBoDe { get; set; }
        public BoDeHocTomTatResponse? BoDe { get; set; }
        public LoaiPhienHocEnum LoaiPhien { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public int ThoiGianHocGiay { get; set; }
        public string ThoiGianHocHienThi { get; set; } = null!;
        public int TongSoThe { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheOnTap { get; set; }
        public int SoTheDung { get; set; }
        public int SoTheSai { get; set; }
        public int SoTheBo { get; set; }
        public int DiemDat { get; set; }
        public int DiemToiDa { get; set; }
        public double TyLeDung { get; set; }
        public int XpNhan { get; set; }
        public int VangNhan { get; set; }
        public CamXucHocEnum? CamXuc { get; set; }
        public string? GhiChu { get; set; }
        public int? MaThachDau { get; set; }

        // Thống kê chi tiết
        public ThongKePhienHocResponse? ThongKe { get; set; }
    }

    // Response thống kê phiên học
    public class ThongKePhienHocResponse
    {
        public int ThoiGianTrungBinhMoiThe { get; set; }
        public int TheSaiNhieuNhat { get; set; }
        public double TyLeDungTheMoi { get; set; }
        public double TyLeDungTheOnTap { get; set; }
        public Dictionary<MucDoKhoEnum, int>? ThongKeTheoDoKho { get; set; }
        public Dictionary<LoaiTheEnum, ThongKeLoaiTheItem>? ThongKeTheoLoaiThe { get; set; }
    }

    public class ThongKeLoaiTheItem
    {
        public int SoThe { get; set; }
        public int SoDung { get; set; }
        public int SoSai { get; set; }
        public double TyLeDung { get; set; }
    }

    // Response bắt đầu phiên học
    public class BatDauPhienHocResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public int MaPhien { get; set; }
        public int TongSoThe { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheOnTap { get; set; }
        public List<TheHocResponse>? DanhSachThe { get; set; }
    }

    // Response kết thúc phiên học
    public class KetThucPhienHocResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public PhienHocResponse? PhienHoc { get; set; }
        public PhanThuongPhienResponse? PhanThuong { get; set; }
        public List<ThanhTuuMoKhoaResponse>? ThanhTuuMoKhoa { get; set; }
        public bool DatMucTieuNgay { get; set; }
        public int? ChuoiNgayHienTai { get; set; }
    }

    // Response phần thưởng phiên học
    public class PhanThuongPhienResponse
    {
        public int XpNhan { get; set; }
        public int XpBonus { get; set; }
        public string? LyDoBonus { get; set; }
        public int VangNhan { get; set; }
        public int TongXpHienTai { get; set; }
        public int TongVangHienTai { get; set; }
        public bool LenCap { get; set; }
        public int? CapMoi { get; set; }
    }

    // Response thành tựu mở khóa
    public class ThanhTuuMoKhoaResponse
    {
        public int MaThanhTuu { get; set; }
        public string TenThanhTuu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? Icon { get; set; }
        public int? XpThuong { get; set; }
    }

    // Response danh sách phiên học
    public class DanhSachPhienHocResponse
    {
        public List<PhienHocTomTatResponse> PhienHocs { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response phiên học tóm tắt
    public class PhienHocTomTatResponse
    {
        public int MaPhien { get; set; }
        public BoDeHocTomTatResponse? BoDe { get; set; }
        public LoaiPhienHocEnum LoaiPhien { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public int ThoiGianHocGiay { get; set; }
        public int TongSoThe { get; set; }
        public int SoTheDung { get; set; }
        public double TyLeDung { get; set; }
        public int XpNhan { get; set; }
    }
}