using System;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ thông tin chủ đề
    /// </summary>
    public class ChuDeResponse
    {
        public int MaChuDe { get; set; }

        public string TenChuDe { get; set; } = null!;

        public string? MoTa { get; set; }

        public int SoLuotDung { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }

    /// <summary>
    /// Response rút gọn (ComboBox / Select)
    /// </summary>
    public class ChuDeSelectResponse
    {
        public int MaChuDe { get; set; }

        public string TenChuDe { get; set; } = null!;
    }
}
