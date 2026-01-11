using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin thích bình luận
    /// </summary>
    public class ReactionBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }

        public Guid MaNguoiDung { get; set; }

        public LoaiReactionEnum LoaiReaction { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
