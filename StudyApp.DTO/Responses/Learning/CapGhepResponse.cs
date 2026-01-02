using System;

namespace StudyApp.DTO.Responses.Learning
{
    // Response cặp ghép chi tiết
    public class CapGhepResponse
    {
        public int MaCap { get; set; }
        public int MaThe { get; set; }
        public string VeTrai { get; set; } = null!;
        public string VePhai { get; set; } = null!;
        public int ThuTu { get; set; }
    }

    // Response cặp ghép khi học (đảo thứ tự)
    public class CapGhepHocResponse
    {
        public int MaCap { get; set; }
        public string VeTrai { get; set; } = null!;
        public int ThuTuVeTrai { get; set; }
        // Vế phải sẽ được trộn ngẫu nhiên
    }

    // Response danh sách vế phải đã trộn
    public class DanhSachVePhaiResponse
    {
        public List<VePhaiItem> VePhais { get; set; } = [];
    }

    public class VePhaiItem
    {
        public int MaCap { get; set; }
        public string VePhai { get; set; } = null!;
        public int ThuTuHienThi { get; set; }
    }
}