using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response phần tử sắp xếp chi tiết
    public class PhanTuSapXepResponse
    {
        public int MaPhanTu { get; set; }
        public int MaThe { get; set; }
        public string NoiDung { get; set; } = null!;
        public int ThuTuDung { get; set; }
    }

    // Response phần tử sắp xếp khi học (đảo thứ tự)
    public class PhanTuSapXepHocResponse
    {
        public int MaPhanTu { get; set; }
        public string NoiDung { get; set; } = null!;
        public int ThuTuHienThi { get; set; } // Thứ tự đã trộn
    }
}