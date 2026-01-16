using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request truy vấn danh sách mention trong bình luận
    /// </summary>
    public class MentionBinhLuanQueryRequest
    {
        public int MaBinhLuan { get; set; }
    }

    /// <summary>
    /// Request truy vấn các bình luận có mention người dùng
    /// </summary>
    public class BinhLuanDuocMentionQueryRequest
    {
        public Guid MaNguoiDung { get; set; }
    }

    /// <summary>
    /// Request thêm mention vào bình luận
    /// </summary>
    public class ThemMentionBinhLuanRequest
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
    }

    /// <summary>
    /// Request xóa mention khỏi bình luận
    /// </summary>
    public class XoaMentionBinhLuanRequest
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
    }
}
