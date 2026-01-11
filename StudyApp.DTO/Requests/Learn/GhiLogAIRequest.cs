using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    public class GhiLogAIRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        [Range(1, int.MaxValue)]
        public int MaThe { get; set; }

        [Required]
        public string Prompt { get; set; } = null!;

        public string? UrlHinhAnh { get; set; }

        public TrangThaiAIEnum TrangThai { get; set; }

        public string? Loi { get; set; }
    }
}