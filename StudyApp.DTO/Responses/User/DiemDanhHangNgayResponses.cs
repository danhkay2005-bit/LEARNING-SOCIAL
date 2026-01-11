using System;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin điểm danh hàng ngày
    /// </summary>
    public class DiemDanhHangNgayResponse
    {
        public int MaDiemDanh { get; set; }

        public Guid MaNguoiDung { get; set; }

        public DateOnly NgayDiemDanh { get; set; }

        /// <summary>
        /// Ngày thứ mấy trong chuỗi điểm danh
        /// </summary>
        public int? NgayThuMay { get; set; }

        public int? ThuongVang { get; set; }
        public int? ThuongXp { get; set; }

        public string? ThuongDacBiet { get; set; }
    }
}
