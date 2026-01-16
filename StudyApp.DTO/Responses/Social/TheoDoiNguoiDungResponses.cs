using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response cho thông tin theo dõi người dùng
    /// </summary>
    public class TheoDoiNguoiDungResponse
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response cho danh sách người theo dõi (followers)
    /// </summary>
    public class NguoiTheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string? HoVaTen { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public DateTime? ThoiGianTheoDoi { get; set; }
        public bool DangTheoDoiLai { get; set; } // Người này có đang theo dõi lại mình không
    }

    /// <summary>
    /// Response cho danh sách người đang theo dõi (following)
    /// </summary>
    public class NguoiDangTheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string? HoVaTen { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public DateTime? ThoiGianTheoDoi { get; set; }
        public bool TheoDoiLai { get; set; } // Người này có theo dõi lại mình không
    }

    /// <summary>
    /// Response cho thống kê theo dõi
    /// </summary>
    public class ThongKeTheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int SoNguoiTheoDoi { get; set; } // Số người đang theo dõi (followers)
        public int SoDangTheoDoi { get; set; } // Số người mình đang theo dõi (following)
    }

    /// <summary>
    /// Response kiểm tra trạng thái theo dõi
    /// </summary>
    public class TrangThaiTheoDoiResponse
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }
        public bool DangTheoDoi { get; set; }
        public DateTime? ThoiGianTheoDoi { get; set; }
    }

    /// <summary>
    /// Response cho gợi ý người dùng theo dõi
    /// </summary>
    public class GoiYTheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string? HoVaTen { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public int SoNguoiTheoDoiChung { get; set; } // Số người theo dõi chung
        public int TongSoNguoiTheoDoi { get; set; }
    }
}
