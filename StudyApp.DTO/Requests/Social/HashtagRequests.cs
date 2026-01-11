using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request tìm kiếm hashtag
    /// </summary>
    public class TimKiemHashtagRequest
    {
        public string TuKhoa { get; set; } = null!;
    }

    /// <summary>
    /// Request cập nhật trạng thái trending (Admin / System)
    /// </summary>
    public class CapNhatTrangThaiHashtagRequest
    {
        public int MaHashtag { get; set; }
        public bool DangThinhHanh { get; set; }
    }
}
