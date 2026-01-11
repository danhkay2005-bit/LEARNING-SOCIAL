using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo vai trò (Admin)
    /// </summary>
    public class TaoVaiTroRequest
    {
        public string TenVaiTro { get; set; } = null!;
        public string? MoTa { get; set; }
    }

    /// <summary>
    /// Request cập nhật vai trò (Admin)
    /// </summary>
    public class CapNhatVaiTroRequest
    {
        public string TenVaiTro { get; set; } = null!;
        public string? MoTa { get; set; }
    }
}
