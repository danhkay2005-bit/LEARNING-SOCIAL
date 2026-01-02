using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.DTO.Responses.Learning
{
    // Response thống kê ngày
    public class ThongKeNgayResponse
    {
        public int MaThongKe { get; set; }
        public Guid MaNguoiDung { get; set; }
        public DateOnly NgayHoc { get; set; }
        public int TongTheHoc { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheOnTap { get; set; }
        public int SoTheDung { get; set; }
        public int SoTheSai { get; set; }
        public double TyLeDung { get; set; }
        public int ThoiGianHocPhut { get; set; }
        public int SoPhienHoc { get; set; }
        public int SoBoDeHoc { get; set; }
        public int XpNhan { get; set; }
        public int VangNhan { get; set; }
        public bool DaHoanThanhMucTieu { get; set; }
    }

    // Response danh sách thống kê ngày
    public class DanhSachThongKeNgayResponse
    {
        public List<ThongKeNgayResponse> ThongKes { get; set; } = [];
        public ThongKeTongHopResponse? TongHop { get; set; }
    }

    // Response thống kê tổng hợp
    public class ThongKeTongHopResponse
    {
        public int TongSoTheHoc { get; set; }
        public int TongThoiGianHocPhut { get; set; }
        public int TongSoPhien { get; set; }
        public int TongSoBoDeHoc { get; set; }
        public double TyLeDungTrungBinh { get; set; }
        public int TongXpNhan { get; set; }
        public int TongVangNhan { get; set; }
        public int SoNgayHoc { get; set; }
        public double TrungBinhTheMoiNgay { get; set; }
        public double TrungBinhPhutMoiNgay { get; set; }
    }

    // Response thống kê tổng quan
    public class ThongKeTongQuanResponse
    {
        public TongQuanHocTapResponse HocTap { get; set; } = new TongQuanHocTapResponse();
        public TongQuanTienDoResponse TienDo { get; set; } = new TongQuanTienDoResponse();
        public TongQuanThanhTichResponse ThanhTich { get; set; } = new TongQuanThanhTichResponse();
        public List<ThongKeNgayResponse>? ThongKeTheoNgay { get; set; }
    }

    public class TongQuanHocTapResponse
    {
        public int TongSoThe { get; set; }
        public int TongThoiGianHocPhut { get; set; }
        public int TongSoPhien { get; set; }
        public double TyLeDungTrungBinh { get; set; }
    }

    public class TongQuanTienDoHocResponse
    {
        public int SoTheMoi { get; set; }
        public int SoTheDangHoc { get; set; }
        public int SoTheCanOnTap { get; set; }
        public int SoTheThanhThao { get; set; }
        public double TienDoPhanTram { get; set; }
    }

    public class TongQuanThanhTichResponse
    {
        public int ChuoiNgayHienTai { get; set; }
        public int ChuoiNgayCaoNhat { get; set; }
        public int TongXp { get; set; }
        public int CapHienTai { get; set; }
        public int XpDenCapTiep { get; set; }
        public int SoThanhTuuMoKhoa { get; set; }
    }

    // Response streak (chuỗi ngày)
    public class StreakResponse
    {
        public int ChuoiNgayHienTai { get; set; }
        public int ChuoiNgayCaoNhat { get; set; }
        public DateOnly? NgayBatDauChuoi { get; set; }
        public List<NgayHocItem>? LichHoc { get; set; }
    }

    public class NgayHocItem
    {
        public DateOnly Ngay { get; set; }
        public bool DaHoc { get; set; }
        public int SoTheHoc { get; set; }
        public int ThoiGianHocPhut { get; set; }
        public bool DatMucTieu { get; set; }
    }

    // Response lịch sử học bộ đề
    public class LichSuHocBoDeResponse
    {
        public int MaLichSu { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaBoDe { get; set; }
        public BoDeHocTomTatResponse? BoDe { get; set; }
        public int? MaPhien { get; set; }
        public int SoTheHoc { get; set; }
        public double TyLeDung { get; set; }
        public int ThoiGianHocPhut { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách lịch sử học
    public class DanhSachLichSuHocResponse
    {
        public List<LichSuHocBoDeResponse> LichSus { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response lịch sử clone
    public class LichSuCloneResponse
    {
        public int MaClone { get; set; }
        public BoDeHocTomTatResponse? BoDeGoc { get; set; }
        public BoDeHocTomTatResponse? BoDeClone { get; set; }
        public NguoiDungTomTatResponse? NguoiClone { get; set; }
        public DateTime? ThoiGian { get; set; }
    }
}