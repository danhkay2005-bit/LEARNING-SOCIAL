using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin mention trong bài đăng
    /// </summary>
    public class MentionBaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDuocMention { get; set; }
        public DateTime? ThoiGian { get; set; }
    }
}
