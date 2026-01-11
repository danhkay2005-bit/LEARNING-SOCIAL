using System;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo cặp ghép cho thẻ GhepCap
    /// </summary>
    public class TaoCapGhepRequest
    {
        public int MaThe { get; set; }

        public string VeTrai { get; set; } = null!;
        public string VePhai { get; set; } = null!;

        public int ThuTu { get; set; } = 0;
    }

    /// <summary>
    /// Request cập nhật cặp ghép
    /// </summary>
    public class CapNhatCapGhepRequest
    {
        public string VeTrai { get; set; } = null!;
        public string VePhai { get; set; } = null!;

        public int ThuTu { get; set; }
    }
}
