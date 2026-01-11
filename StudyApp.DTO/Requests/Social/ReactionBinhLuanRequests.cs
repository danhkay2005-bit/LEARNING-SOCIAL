using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request thêm hoặc cập nhật reaction cho bình luận
    /// (1 user – 1 reaction trên 1 bình luận)
    /// </summary>
    public class TaoHoacCapNhatReactionBinhLuanRequest
    {
        [Required]
        public int MaBinhLuan { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required]
        public LoaiReactionEnum LoaiReaction { get; set; }
    }

    /// <summary>
    /// Request xóa reaction khỏi bình luận
    /// </summary>
    public class XoaReactionBinhLuanRequest
    {
        [Required]
        public int MaBinhLuan { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }
    }
}
