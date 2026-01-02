using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response bộ đề yêu thích
    public class BoDeYeuThichResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int MaBoDe { get; set; }
        public BoDeHocTomTatResponse? BoDe { get; set; }
        public DateTime? ThoiGianLuu { get; set; }
        public string? GhiChu { get; set; }
    }

    // Response danh sách bộ đề yêu thích
    public class DanhSachBoDeYeuThichResponse
    {
        public List<BoDeYeuThichResponse> BoDeYeuThichs { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả yêu thích
    public class KetQuaYeuThichResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public bool DaLuu { get; set; }
    }
}