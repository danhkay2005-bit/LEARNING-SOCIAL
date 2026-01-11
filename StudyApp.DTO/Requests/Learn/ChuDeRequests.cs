using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo mới chủ đề
    /// </summary>
    public class TaoChuDeRequest
    {
        [Required(ErrorMessage = "Tên chủ đề là bắt buộc")]
        [MaxLength(200)]
        public string TenChuDe { get; set; } = null!;

        [MaxLength(500)]
        public string? MoTa { get; set; }
    }

    /// <summary>
    /// Request cập nhật chủ đề
    /// </summary>
    public class CapNhatChuDeRequest
    {
        [Required]
        public int MaChuDe { get; set; }

        [Required(ErrorMessage = "Tên chủ đề là bắt buộc")]
        [MaxLength(200)]
        public string TenChuDe { get; set; } = null!;

        [MaxLength(500)]
        public string? MoTa { get; set; }
    }
}
