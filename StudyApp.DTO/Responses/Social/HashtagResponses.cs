using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin hashtag
    /// </summary>
    public class HashtagResponse
    {
        public int MaHashtag { get; set; }

        public string TenHashtag { get; set; } = null!;

        public int SoLuotDung { get; set; }

        public TrangThaiHashtagEnum TrangThai { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }
}
