using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response hashtag
    public class HashtagResponse
    {
        public int MaHashtag { get; set; }
        public string TenHashtag { get; set; } = null!;
        public int SoLuotDung { get; set; }
        public bool DangThinhHanh { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response hashtag thịnh hành
    public class HashtagThinhHanhResponse
    {
        public int MaHashtag { get; set; }
        public string TenHashtag { get; set; } = null!;
        public int SoLuotDung { get; set; }
        public int ThuHang { get; set; }
        public double PhanTramTangTruong { get; set; }
    }

    // Response danh sách hashtag
    public class DanhSachHashtagResponse
    {
        public List<HashtagResponse> Hashtags { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response danh sách hashtag thịnh hành
    public class DanhSachHashtagThinhHanhResponse
    {
        public List<HashtagThinhHanhResponse> Hashtags { get; set; } = [];
        public DateTime ThoiGianCapNhat { get; set; }
    }
}