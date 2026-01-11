using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin bình luận bài đăng
    /// </summary>
    public class BinhLuanBaiDangResponse
    {
        public int MaBinhLuan { get; set; }
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string NoiDung { get; set; } = null!;
        public string? HinhAnh { get; set; }

        public int? MaBinhLuanCha { get; set; }

        public int SoLuotThich { get; set; }

        public bool DaChinhSua { get; set; }
        public bool DaXoa { get; set; }

        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianSua { get; set; }
    }
}
