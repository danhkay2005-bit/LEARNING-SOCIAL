using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learn
{
    public class TagResponse
    {
        public int MaTag { get; set; }
        public string TenTag { get; set; } = null!;
        public int SoLuotDung { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Dùng cho ComboBox / AutoComplete
    public class TagSelectResponse
    {
        public int MaTag { get; set; }
        public string TenTag { get; set; } = null!;
    }

    // Response gợi ý hashtag
    public class GoiYTagResponse
    {
        public List<TagSelectResponse> Tags { get; set; } = new();
    }

    // Response sau khi xử lý nội dung có #
    public class XuLyTagTuNoiDungResponse
    {
        public List<TagSelectResponse> Tags { get; set; } = new();
    }
}
