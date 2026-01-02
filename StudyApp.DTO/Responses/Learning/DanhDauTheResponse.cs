using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response đánh dấu thẻ
    public class DanhDauTheResponse
    {
        public int MaDanhDau { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaThe { get; set; }
        public TheFlashcardTomTatResponse? The { get; set; }
        public LoaiDanhDauTheEnum LoaiDanhDau { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách thẻ đánh dấu
    public class DanhSachTheDanhDauResponse
    {
        public List<DanhDauTheResponse> DanhDaus { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public Dictionary<LoaiDanhDauTheEnum, int>? ThongKeTheoLoai { get; set; }
    }

    // Response kết quả đánh dấu
    public class KetQuaDanhDauTheResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public DanhDauTheResponse? DanhDau { get; set; }
    }
}