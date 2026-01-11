using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Responses.Social
{
    public class ReactionBaiDangResponse
    {
        public int MaBaiDang { get; set; }

        public Guid MaNguoiDung { get; set; }

        public LoaiReactionEnum LoaiReaction { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
