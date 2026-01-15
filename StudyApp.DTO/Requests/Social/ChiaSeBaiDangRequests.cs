using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request để chia sẻ bài đăng
    /// </summary>
    public class ChiaSeBaiDangReques
    {
        public int MaBaiDangGoc { get; set; }
        public Guid MaNguoiChiaSe { get; set; }
        public string? NoiDungThem { get; set; }
    }

    /// <summary>
    /// Request để hủy chia sẻ bài đăng
    /// </summary>
    public class HuyChiaSeBaiDangRequest
    {
        public int MaChiaSe { get; set; }
        public Guid MaNguoiChiaSe { get; set; }
    }

    /// <summary>
    /// Request để lấy danh sách chia sẻ của bài đăng
    /// </summary>
    public class LayDanhSachChiaSeRequest
    {
        public int MaBaiDangGoc { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }

    /// <summary>
    /// Request để kiểm tra đã chia sẻ bài đăng chưa
    /// </summary>
    public class KiemTraChiaSeRequest
    {
        public int MaBaiDangGoc { get; set; }
        public Guid MaNguoiDung { get; set; }
    }
}
