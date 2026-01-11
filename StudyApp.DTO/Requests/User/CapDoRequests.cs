using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo mới cấp độ
    /// </summary>
    public class TaoCapDoRequest
    {
        public string TenCapDo { get; set; } = null!;
        public string? BieuTuong { get; set; }

        public int MucXpToiThieu { get; set; }
        public int MucXpToiDa { get; set; }
    }

    /// <summary>
    /// Request cập nhật cấp độ
    /// </summary>
    public class CapNhatCapDoRequest
    {
        public string TenCapDo { get; set; } = null!;
        public string? BieuTuong { get; set; }

        public int MucXpToiThieu { get; set; }
        public int MucXpToiDa { get; set; }
    }
}
