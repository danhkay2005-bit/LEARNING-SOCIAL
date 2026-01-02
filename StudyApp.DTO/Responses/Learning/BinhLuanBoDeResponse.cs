using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response bình luận bộ đề
    public class BinhLuanBoDeResponse
    {
        public int MaBinhLuan { get; set; }
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiBinhLuan { get; set; }
        public string NoiDung { get; set; } = null!;
        public int? MaBinhLuanCha { get; set; }
        public int SoLuotThich { get; set; }
        public int SoTraLoi { get; set; }
        public bool DaChinhSua { get; set; }
        public DateTime? ThoiGian { get; set; }
        public DateTime? ThoiGianSua { get; set; }
        public bool DaThich { get; set; }
        public bool LaCuaToi { get; set; }
        public List<BinhLuanBoDeResponse>? TraLois { get; set; }
    }

    // Response danh sách bình luận
    public class DanhSachBinhLuanBoDeResponse
    {
        public List<BinhLuanBoDeResponse> BinhLuans { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả bình luận
    public class KetQuaBinhLuanBoDeResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BinhLuanBoDeResponse? BinhLuan { get; set; }
    }
}