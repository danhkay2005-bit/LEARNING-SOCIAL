using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Social
{
    /// Response chi tiết chia sẻ bài đăng
    public class ChiaSeBaiDangResponse
    {
        public int MaChiaSe { get; set; }

        public int MaBaiDangGoc { get; set; }

        public Guid MaNguoiChiaSe { get; set; }

        public string? NoiDungThem { get; set; }

        public int? MaBaiDangMoi { get; set; }

        public DateTime? ThoiGian { get; set; }

        // Thông tin bài đăng gốc
        public BaiDangResponse? BaiDangGoc { get; set; }

        // Thông tin bài đăng mới được tạo khi chia sẻ
        public BaiDangResponse? BaiDangMoi { get; set; }
    }

    /// Response thống kê chia sẻ
    public class ThongKeChiaSeBaiDangResponse
    {
        public int MaBaiDang { get; set; }

        public int TongSoChiaSe { get; set; }

        public DateTime? LanChiaSeMoiNhat { get; set; }
    }
}
