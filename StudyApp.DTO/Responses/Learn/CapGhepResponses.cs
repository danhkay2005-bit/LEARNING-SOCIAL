using System;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response thông tin cặp ghép
    /// </summary>
    public class CapGhepResponse
    {
        public int MaCap { get; set; }

        public int MaThe { get; set; }

        public string VeTrai { get; set; } = null!;
        public string VePhai { get; set; } = null!;

        public int ThuTu { get; set; }
    }
}
