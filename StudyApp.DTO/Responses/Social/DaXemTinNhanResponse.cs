using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response người đã xem tin nhắn
    public class NguoiXemTinNhanResponse
    {
        public Guid MaNguoiXem { get; set; }
        public NguoiDungTomTatResponse? NguoiXem { get; set; }
        public DateTime? ThoiGianXem { get; set; }
    }

    // Response người xem tóm tắt
    public class NguoiXemTomTatResponse
    {
        public Guid MaNguoiXem { get; set; }
        public string? TenNguoiXem { get; set; }
        public string? AnhDaiDien { get; set; }
        public DateTime? ThoiGianXem { get; set; }
    }

    // Response danh sách người đã xem
    public class DanhSachNguoiXemResponse
    {
        public int MaTinNhan { get; set; }
        public List<NguoiXemTinNhanResponse> NguoiXems { get; set; } = [];
        public int TongSo { get; set; }
    }

    // Response kết quả đánh dấu đã xem
    public class KetQuaDanhDauDaXemResponse
    {
        public bool ThanhCong { get; set; }
        public int SoTinDaDanhDau { get; set; }
    }
}