using System;

namespace StudyApp.DTO.Responses.Learning
{
    // Response đáp án trắc nghiệm chi tiết
    public class DapAnTracNghiemResponse
    {
        public int MaDapAn { get; set; }
        public int MaThe { get; set; }
        public string NoiDung { get; set; } = null!;
        public bool LaDapAnDung { get; set; }
        public int ThuTu { get; set; }
        public string? GiaiThich { get; set; }
    }

    // Response đáp án trắc nghiệm khi học (ẩn đáp án đúng)
    public class DapAnTracNghiemHocResponse
    {
        public int MaDapAn { get; set; }
        public string NoiDung { get; set; } = null!;
        public int ThuTu { get; set; }
    }
}