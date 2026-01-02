using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request reaction bài đăng
    public class ReactionBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        [Required(ErrorMessage = "Loại reaction là bắt buộc")]
        public LoaiReactionEnum LoaiReaction { get; set; }
    }

    // Request xóa reaction bài đăng
    public class XoaReactionBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }
    }

    // Request reaction tin nhắn
    public class ReactionTinNhanRequest
    {
        [Required(ErrorMessage = "Mã tin nhắn là bắt buộc")]
        public int MaTinNhan { get; set; }

        [Required(ErrorMessage = "Emoji là bắt buộc")]
        [MaxLength(10, ErrorMessage = "Emoji không hợp lệ")]
        public string Emoji { get; set; } = null!;
    }

    // Request xóa reaction tin nhắn
    public class XoaReactionTinNhanRequest
    {
        [Required(ErrorMessage = "Mã tin nhắn là bắt buộc")]
        public int MaTinNhan { get; set; }
    }

    // Request lấy danh sách reaction
    public class LayReactionBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        public LoaiReactionEnum? LoaiReaction { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}