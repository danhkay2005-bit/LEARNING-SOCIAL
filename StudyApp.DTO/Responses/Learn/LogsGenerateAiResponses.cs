using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response log sinh AI
    /// </summary>
    public class LogsGenerateAiResponse
    {
        public int MaLog { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int MaThe { get; set; }

        public string Prompt { get; set; } = null!;

        public string? UrlHinhAnh { get; set; }

        public TrangThaiAIEnum TrangThai { get; set; }

        public string? Loi { get; set; }

        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho danh sách log
    /// </summary>
    public class LogsGenerateAiSummaryResponse
    {
        public int MaLog { get; set; }

        public int MaThe { get; set; }

        public TrangThaiAIEnum TrangThai { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
