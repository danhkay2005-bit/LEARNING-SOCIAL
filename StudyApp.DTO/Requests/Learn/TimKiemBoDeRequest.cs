using System.Collections.Generic;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    public class TimKiemBoDeRequest
    {
        public string? TuKhoa { get; set; }
        public int? MaChuDe { get; set; }
        public int? MaThuMuc { get; set; }
        public MucDoKhoEnum? MucDoKho { get; set; }

        public List<int> DanhSachMaTag { get; set; } = [];
        public bool ChiHienCongKhai { get; set; } = true;

        // Phân trang & Sắp xếp
        public int TrangHienTai { get; set; } = 1;
        public int KichThuocTrang { get; set; } = 10;
        public string SapXepTheo { get; set; } = "MoiNhat";
    }
}