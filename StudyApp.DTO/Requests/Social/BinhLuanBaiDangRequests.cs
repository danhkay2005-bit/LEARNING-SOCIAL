using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request tạo bình luận bài đăng
    /// </summary>
    public class TaoBinhLuanRequest
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string NoiDung { get; set; } = null!;
        public string? HinhAnh { get; set; }

        /// <summary>
        /// Bình luận cha (reply), null nếu là bình luận gốc
        /// </summary>
        public int? MaBinhLuanCha { get; set; }
    }

    /// <summary>
    /// Request cập nhật bình luận
    /// </summary>
    public class CapNhatBinhLuanRequest
    {
        public string NoiDung { get; set; } = null!;
        public string? HinhAnh { get; set; }
    }

    /// <summary>
    /// Request xóa mềm bình luận
    /// </summary>
    public class XoaBinhLuanRequest
    {
        public bool Xoa { get; set; } = true;
    }
}
