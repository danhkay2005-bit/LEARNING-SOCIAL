using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request tạo bài đăng
    /// </summary>
    public class TaoBaiDangRequest
    {
        public Guid MaNguoiDung { get; set; }

        public string? NoiDung { get; set; }

        public LoaiBaiDangEnum LoaiBaiDang { get; set; } = LoaiBaiDangEnum.VanBan;

        public string? HinhAnh { get; set; }
        public string? Video { get; set; }

        // Liên kết hệ thống
        public int? MaBoDeLienKet { get; set; }
        public int? MaThanhTuuLienKet { get; set; }
        public int? MaThachDauLienKet { get; set; }

        public int? SoChuoiNgay { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; } = QuyenRiengTuEnum.CongKhai;
    }

    /// <summary>
    /// Request chỉnh sửa bài đăng
    /// </summary>
    public class CapNhatBaiDangRequest
    {
        public string? NoiDung { get; set; }

        public string? HinhAnh { get; set; }
        public string? Video { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; }

        public bool TatBinhLuan { get; set; }
    }

    /// <summary>
    /// Request ghim / bỏ ghim bài đăng
    /// </summary>
    public class GhimBaiDangRequest
    {
        public bool GhimBaiDang { get; set; }
    }
}
