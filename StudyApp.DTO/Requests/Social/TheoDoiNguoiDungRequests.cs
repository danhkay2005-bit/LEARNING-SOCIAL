using System;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request theo dõi người dùng
    /// </summary>
    public class TheoDoiNguoiDungRequest
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }
    }

    /// <summary>
    /// Request bỏ theo dõi người dùng
    /// </summary>
    public class BoTheoDoiNguoiDungRequest
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }
    }

    /// <summary>
    /// Request kiểm tra trạng thái theo dõi
    /// </summary>
    public class KiemTraTheoDoiRequest
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }
    }
}
