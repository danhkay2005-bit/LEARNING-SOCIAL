using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response thành viên chi tiết
    public class ThanhVienCuocTroChuyenResponse
    {
        public int MaCuocTroChuyen { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public string? BiDanh { get; set; }
        public VaiTroThanhVienChatEnum VaiTro { get; set; }
        public bool TatThongBao { get; set; }
        public bool DaRoiNhom { get; set; }
        public DateTime? ThoiGianThamGia { get; set; }
        public DateTime? ThoiGianXemCuoi { get; set; }
        public bool DangOnline { get; set; }
    }

    // Response thành viên tóm tắt
    public class ThanhVienTomTatResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string? TenHienThi { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? BiDanh { get; set; }
        public VaiTroThanhVienChatEnum VaiTro { get; set; }
        public bool DangOnline { get; set; }
    }

    // Response danh sách thành viên
    public class DanhSachThanhVienResponse
    {
        public List<ThanhVienCuocTroChuyenResponse> ThanhViens { get; set; } = [];
        public int TongSo { get; set; }
        public int SoQuanTri { get; set; }
        public int SoDangOnline { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả thao tác thành viên
    public class KetQuaThanhVienResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public List<Guid>? ThanhVienThatBai { get; set; }
    }
}