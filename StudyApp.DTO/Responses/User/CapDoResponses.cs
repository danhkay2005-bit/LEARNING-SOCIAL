using System;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin cấp độ
    /// </summary>
    public class CapDoResponse
    {
        public int MaCapDo { get; set; }

        public string TenCapDo { get; set; } = null!;
        public string? BieuTuong { get; set; }

        public int MucXpToiThieu { get; set; }
        public int MucXpToiDa { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }
}
