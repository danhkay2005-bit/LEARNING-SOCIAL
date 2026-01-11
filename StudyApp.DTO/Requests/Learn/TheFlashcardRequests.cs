using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo thẻ flashcard
    /// </summary>
    public class TaoTheFlashcardRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        [Required]
        public LoaiTheEnum LoaiThe { get; set; }

        [Required(ErrorMessage = "Mặt trước là bắt buộc")]
        public string MatTruoc { get; set; } = null!;

        [Required(ErrorMessage = "Mặt sau là bắt buộc")]
        public string MatSau { get; set; } = null!;

        public string? GiaiThich { get; set; }

        public string? HinhAnhTruoc { get; set; }

        public string? HinhAnhSau { get; set; }

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }
    }

    /// <summary>
    /// Request cập nhật thẻ flashcard
    /// </summary>
    public class CapNhatTheFlashcardRequest
    {
        [Required]
        public int MaThe { get; set; }

        [Required]
        public LoaiTheEnum LoaiThe { get; set; }

        [Required]
        public string MatTruoc { get; set; } = null!;

        [Required]
        public string MatSau { get; set; } = null!;

        public string? GiaiThich { get; set; }

        public string? HinhAnhTruoc { get; set; }

        public string? HinhAnhSau { get; set; }

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }
    }
}
