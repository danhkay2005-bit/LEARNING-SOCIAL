using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response cuộc trò chuyện chi tiết
    public class CuocTroChuyenResponse
    {
        public int MaCuocTroChuyen { get; set; }
        public LoaiCuocTroChuyenEnum LoaiCuocTroChuyen { get; set; }
        public string? TenNhomChat { get; set; }
        public string? AnhNhomChat { get; set; }
        public Guid? MaNguoiTaoNhom { get; set; }
        public NguoiDungTomTatResponse? NguoiTaoNhom { get; set; }
        public TinNhanTomTatResponse? TinNhanCuoi { get; set; }
        public DateTime? ThoiGianTinCuoi { get; set; }
        public DateTime? ThoiGianTao { get; set; }

        // Thông tin cho người dùng hiện tại
        public int SoTinChuaDoc { get; set; }
        public bool TatThongBao { get; set; }
        public bool GhimCuocTro { get; set; }
        public string? BiDanhCuaToi { get; set; }
        public VaiTroThanhVienChatEnum VaiTroCuaToi { get; set; }

        // Danh sách thành viên (tóm tắt)
        public int SoThanhVien { get; set; }
        public List<ThanhVienTomTatResponse>? ThanhViens { get; set; }

        // Cho cuộc trò chuyện cá nhân
        public NguoiDungTomTatResponse? NguoiNhan { get; set; }
        public bool NguoiNhanOnline { get; set; }
        public DateTime? ThoiGianOnlineCuoi { get; set; }
    }

    // Response cuộc trò chuyện tóm tắt (cho danh sách)
    public class CuocTroChuyenTomTatResponse
    {
        public int MaCuocTroChuyen { get; set; }
        public LoaiCuocTroChuyenEnum LoaiCuocTroChuyen { get; set; }
        public string? TenHienThi { get; set; }
        public string? AnhHienThi { get; set; }
        public string? NoiDungTinCuoi { get; set; }
        public DateTime? ThoiGianTinCuoi { get; set; }
        public int SoTinChuaDoc { get; set; }
        public bool TatThongBao { get; set; }
        public bool GhimCuocTro { get; set; }
        public bool Online { get; set; }
    }

    // Response danh sách cuộc trò chuyện
    public class DanhSachCuocTroChuyenResponse
    {
        public List<CuocTroChuyenTomTatResponse> CuocTroChuyens { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
    }

    // Response sau khi tạo cuộc trò chuyện
    public class TaoCuocTroChuyenResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public CuocTroChuyenResponse? CuocTroChuyen { get; set; }
        public bool DaTonTai { get; set; }
    }
}