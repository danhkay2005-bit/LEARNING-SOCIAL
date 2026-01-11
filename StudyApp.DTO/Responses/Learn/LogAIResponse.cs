using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    public class LogAIResponse
    {
        public int MaLog { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string Prompt { get; set; } = null!;
        public TrangThaiAIEnum TrangThai { get; set; }
        public string? Loi { get; set; }

        public DateTime ThoiGian { get; set; }
    }
}