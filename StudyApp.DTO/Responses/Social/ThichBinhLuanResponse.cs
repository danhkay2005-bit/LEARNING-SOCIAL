using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response người thích bình luận
    public class NguoiThichBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách người thích
    public class DanhSachNguoiThichBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public List<NguoiThichBinhLuanResponse> NguoiThichs { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả thích
    public class KetQuaThichBinhLuanResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public int SoLuotThichMoi { get; set; }
        public bool DaThich { get; set; }
    }
}