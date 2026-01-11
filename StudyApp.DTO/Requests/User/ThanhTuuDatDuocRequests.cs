using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request đánh dấu thành tựu đã xem
    /// </summary>
    public class DanhDauThanhTuuDaXemRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaThanhTuu { get; set; }
    }

    /// <summary>
    /// Request đánh dấu thành tựu đã chia sẻ
    /// </summary>
    public class DanhDauThanhTuuDaChiaSeRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaThanhTuu { get; set; }
    }

    /// <summary>
    /// Request truy vấn danh sách thành tựu đạt được
    /// </summary>
    public class ThanhTuuDatDuocQueryRequest
    {
        public Guid MaNguoiDung { get; set; }

        /// <summary>
        /// Lọc theo trạng thái đã xem hay chưa
        /// </summary>
        public bool? DaXem { get; set; }
    }
}
