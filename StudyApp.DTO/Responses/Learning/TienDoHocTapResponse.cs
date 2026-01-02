using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response tiến độ thẻ chi tiết
    public class TienDoTheResponse
    {
        public int MaTienDo { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaThe { get; set; }
        public TrangThaiSRSEnum TrangThai { get; set; }
        public double HeSoDe { get; set; }
        public int KhoangCachNgay { get; set; }
        public int SoLanLap { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
        public int SoLanDung { get; set; }
        public int SoLanSai { get; set; }
        public double TyLeDung { get; set; }
        public int ThoiGianTraLoiTbGiay { get; set; }
        public MucDoKhoEnum? DoKhoCanNhan { get; set; }
        public DateTime? LanHocCuoi { get; set; }
    }

    // Response danh sách thẻ cần ôn tập
    public class DanhSachTheCanOnTapResponse
    {
        public int TongSoTheCanOnTap { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheQuaHan { get; set; }
        public int SoTheSapDen { get; set; }
        public List<TheHocResponse> DanhSachThe { get; set; } = [];
    }

    // Response cập nhật tiến độ
    public class CapNhatTienDoResponse
    {
        public bool ThanhCong { get; set; }
        public TrangThaiSRSEnum TrangThaiMoi { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
        public int KhoangCachNgayMoi { get; set; }
        public double HeSoDeMoi { get; set; }
    }

    // Response tổng quan tiến độ
    public class TongQuanTienDoResponse
    {
        public int TongSoThe { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheDangHoc { get; set; }
        public int SoTheCanOnTap { get; set; }
        public int SoTheThanhThao { get; set; }
        public double TyLeDungTrungBinh { get; set; }
        public int ChuoiNgayHienTai { get; set; }
        public int ChuoiNgayCaoNhat { get; set; }
        public Dictionary<TrangThaiSRSEnum, int>? ThongKeTheoTrangThai { get; set; }
    }
}