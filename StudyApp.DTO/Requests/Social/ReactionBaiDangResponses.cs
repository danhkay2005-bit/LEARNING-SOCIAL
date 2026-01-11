using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin reaction bài đăng
    /// </summary>
    public class ReactionBaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }

        public LoaiReactionEnum LoaiReaction { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
