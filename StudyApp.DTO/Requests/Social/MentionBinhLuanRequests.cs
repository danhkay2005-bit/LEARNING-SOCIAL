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
}
