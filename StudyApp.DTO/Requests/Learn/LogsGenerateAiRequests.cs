using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request ghi log sinh nội dung bằng AI
    /// </summary>
    public class TaoLogsGenerateAiRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Prompt là bắt buộc")]
        public string Prompt { get; set; } = null!;

        /// <summary>
        /// URL hình ảnh (nếu AI sinh ảnh)
        /// </summary>
        public string? UrlHinhAnh { get; set; }

        /// <summary>
        /// Trạng thái xử lý AI
        /// </summary>
        public TrangThaiAIEnum TrangThai { get; set; }

        /// <summary>
        /// Thông tin lỗi (nếu có)
        /// </summary>
        public string? Loi { get; set; }
    }
}
