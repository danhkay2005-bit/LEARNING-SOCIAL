using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin bài đăng
    /// </summary>
    public class BaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string? NoiDung { get; set; }

        public LoaiBaiDangEnum LoaiBaiDang { get; set; }

        public string? HinhAnh { get; set; }
        public string? Video { get; set; }

        // Liên kết
        public int? MaBoDeLienKet { get; set; }
        public int? MaThanhTuuLienKet { get; set; }
        public int? MaThachDauLienKet { get; set; }
        public int? SoChuoiNgay { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; }

        // Thống kê
        public int SoReaction { get; set; }
        public int SoBinhLuan { get; set; }

        // Trạng thái
        public bool GhimBaiDang { get; set; }
        public bool TatBinhLuan { get; set; }
        public bool DaChinhSua { get; set; }
        public bool DaXoa { get; set; }

        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianSua { get; set; }
    }
}
