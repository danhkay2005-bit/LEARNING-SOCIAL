using System;

namespace StudyApp.DTO.Responses.Learning
{
    // Response từ điền khuyết chi tiết
    public class TuDienKhuyetResponse
    {
        public int MaTuDienKhuyet { get; set; }
        public int MaThe { get; set; }
        public string TuCanDien { get; set; } = null!;
        public int ViTriTrongCau { get; set; }
        public string? GoiY { get; set; }
    }

    // Response từ điền khuyết khi học
    public class TuDienKhuyetHocResponse
    {
        public int MaTuDienKhuyet { get; set; }
        public int ViTriTrongCau { get; set; }
        public int DoRongTu { get; set; } // Độ rộng ô input
        public string? GoiY { get; set; }
    }
}