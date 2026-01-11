using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin thích bình luận
    /// </summary>
    public class ThichBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDung { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
