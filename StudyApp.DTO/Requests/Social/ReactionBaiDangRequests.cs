using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request thêm hoặc cập nhật reaction cho bài đăng
    /// (1 người dùng – 1 reaction trên 1 bài đăng)
    /// </summary>
    public class TaoHoacCapNhatReactionBaiDangRequest
    {
        [Required]
        public int MaBaiDang { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required]
        public LoaiReactionEnum LoaiReaction { get; set; }
    }

    /// <summary>
    /// Request xóa reaction khỏi bài đăng
    /// </summary>
    public class XoaReactionBaiDangRequest
    {
        [Required]
        public int MaBaiDang { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }
    }

    public class ThaReactionRequest
    {
        [Required]
        public int MaBaiDang { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }

        // Các giá trị: "Thich", "Tim", "HaHa", "Wow", "Buon", "TucGian"
        // Khớp với CHECK constraint trong SQL
        public string LoaiReaction { get; set; } = "Thich";
    }
}
