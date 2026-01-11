using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin vai trò
    /// </summary>
    public class VaiTroResponse
    {
        public int MaVaiTro { get; set; }

        public string TenVaiTro { get; set; } = null!;
        public string? MoTa { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }
}
