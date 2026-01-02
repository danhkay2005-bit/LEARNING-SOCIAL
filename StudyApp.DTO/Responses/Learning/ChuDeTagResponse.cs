using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response chủ đề
    public class ChuDeResponse
    {
        public int MaChuDe { get; set; }
        public string TenChuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public int SoLuotDung { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response danh sách chủ đề
    public class DanhSachChuDeResponse
    {
        public List<ChuDeResponse> ChuDes { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response tag
    public class TagResponse
    {
        public int MaTag { get; set; }
        public string TenTag { get; set; } = null!;
        public int SoLuotDung { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response danh sách tag
    public class DanhSachTagResponse
    {
        public List<TagResponse> Tags { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response tag phổ biến
    public class TagPhooBienResponse
    {
        public List<TagResponse> Tags { get; set; } = [];
    }
}