
using StudyApp.DTO.Responses;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin reaction bài đăng
    /// </summary>
    public class ReactionBaiDangResponses 
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }
        public LoaiReactionEnum LoaiReaction { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response thông tin reaction bình luận
    /// </summary>
    public class ReactionBinhLuanResponses
    {
        public int MaBinhLuan { get; set; }
        public Guid MaNguoiDung { get; set; }
        public LoaiReactionEnum LoaiReaction { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response thống kê reaction theo loại
    /// </summary>
    public class ThongKeReactionResponse
    {
        public LoaiReactionEnum LoaiReaction { get; set; }
        public int SoLuong { get; set; }
    }
}