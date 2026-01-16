using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Social
{
    /// Request tạo bài đăng
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

    /// Request chỉnh sửa bài đăng
    public class CapNhatBaiDangRequest
    {
        public string? NoiDung { get; set; }

        public string? HinhAnh { get; set; }
        public string? Video { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; }

        public bool TatBinhLuan { get; set; }
    }

    /// Request ghim / bỏ ghim bài đăng
    public class GhimBaiDangRequest
    {
        public bool GhimBaiDang { get; set; }
    }
    /// Request chia sẻ bài đăng
    public class ChiaSeBaiDangRequest
    {
        public int MaBaiDangGoc { get; set; }

        public Guid MaNguoiChiaSe { get; set; }

        public string? NoiDungThem { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; } = QuyenRiengTuEnum.CongKhai;
    }
}
