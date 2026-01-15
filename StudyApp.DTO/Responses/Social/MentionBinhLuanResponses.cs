using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin mention trong bình luận
    /// </summary>
    public class MentionBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
        public DateTime? ThoiGian { get; set; }
    }
}
