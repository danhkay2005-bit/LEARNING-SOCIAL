using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo đáp án trắc nghiệm
    /// </summary>
    public class TaoDapAnTracNghiemRequest
    {
        [Required]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Nội dung đáp án là bắt buộc")]
        [MaxLength(500)]
        public string NoiDung { get; set; } = null!;

        /// <summary>
        /// Có phải đáp án đúng hay không (chỉ admin / giáo viên)
        /// </summary>
        public bool LaDapAnDung { get; set; }

        /// <summary>
        /// Thứ tự hiển thị đáp án
        /// </summary>
        public int? ThuTu { get; set; }
    }

    /// <summary>
    /// Request cập nhật đáp án trắc nghiệm
    /// </summary>
    public class CapNhatDapAnTracNghiemRequest
    {
        [Required]
        public int MaDapAn { get; set; }

        [Required(ErrorMessage = "Nội dung đáp án là bắt buộc")]
        [MaxLength(500)]
        public string NoiDung { get; set; } = null!;

        public bool LaDapAnDung { get; set; }

        public int? ThuTu { get; set; }
    }
}
