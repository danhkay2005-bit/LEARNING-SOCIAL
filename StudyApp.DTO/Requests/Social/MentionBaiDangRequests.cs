using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request truy vấn danh sách mention trong bài đăng
    /// </summary>
    public class MentionBaiDangQueryRequest
    {
        public int MaBaiDang { get; set; }
    }

    /// <summary>
    /// Request truy vấn các bài đăng có mention người dùng
    /// </summary>
    public class BaiDangDuocMentionQueryRequest
    {
        public Guid MaNguoiDung { get; set; }
    }

    /// <summary>
    /// Request thêm mention vào bài đăng
    /// </summary>
    public class ThemMentionBaiDangRequest
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
    }

    /// <summary>
    /// Request xóa mention khỏi bài đăng
    /// </summary>
    public class XoaMentionBaiDangRequest
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
    }
}
