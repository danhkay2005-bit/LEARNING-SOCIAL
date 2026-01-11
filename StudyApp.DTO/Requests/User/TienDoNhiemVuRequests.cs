using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request truy vấn tiến độ nhiệm vụ của người dùng
    /// </summary>
    public class TienDoNhiemVuQueryRequest
    {
        public Guid MaNguoiDung { get; set; }

        /// <summary>
        /// Lọc theo trạng thái hoàn thành
        /// </summary>
        public bool? DaHoanThanh { get; set; }
    }

    /// <summary>
    /// Request nhận thưởng nhiệm vụ
    /// </summary>
    public class NhanThuongNhiemVuRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaNhiemVu { get; set; }
    }
}
