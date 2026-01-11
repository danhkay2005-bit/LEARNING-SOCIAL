using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request truy vấn lịch sử giao dịch của người dùng
    /// </summary>
    public class LichSuGiaoDichQueryRequest
    {
        public Guid MaNguoiDung { get; set; }

        public LoaiGiaoDichEnum? LoaiGiaoDich { get; set; }

        public LoaiTienGiaoDichEnum? LoaiTien { get; set; }

        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
