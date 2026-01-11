using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request thích bình luận
    /// </summary>
    public class ThichBinhLuanRequest
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDung { get; set; }
    }

    /// <summary>
    /// Request bỏ thích bình luận
    /// </summary>
    public class BoThichBinhLuanRequest
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDung { get; set; }
    }
}
