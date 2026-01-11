using System;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response tiến độ nhiệm vụ của người dùng
    /// </summary>
    public class TienDoNhiemVuResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int MaNhiemVu { get; set; }

        public int TienDoHienTai { get; set; }

        public bool DaHoanThanh { get; set; }
        public bool DaNhanThuong { get; set; }

        public DateOnly? NgayBatDau { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
    }
}
