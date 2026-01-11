using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request điểm danh hàng ngày
    /// </summary>
    public class DiemDanhHangNgayRequest
    {
        public Guid MaNguoiDung { get; set; }

        /// <summary>
        /// Ngày điểm danh (thường là ngày hiện tại)
        /// </summary>
        public DateOnly NgayDiemDanh { get; set; }
    }
}
