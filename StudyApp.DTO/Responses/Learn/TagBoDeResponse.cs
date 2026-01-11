using System;

namespace StudyApp.DTO.Responses.Learn
{
    // Response quan hệ Tag - Bộ đề
    public class TagBoDeResponse
    {
        public int MaBoDe { get; set; }
        public int MaTag { get; set; }
        public DateTime? ThoiGianThem { get; set; }
    }

    // Danh sách tag của 1 bộ đề
    public class TagBoDeViewResponse
    {
        public int MaTag { get; set; }
        public string TenTag { get; set; } = null!;
    }
}
