using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin thành tựu đạt được của người dùng
    /// </summary>
    public class ThanhTuuDatDuocResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int MaThanhTuu { get; set; }

        public DateTime? NgayDat { get; set; }

        public bool DaXem { get; set; }
        public bool DaChiaSe { get; set; }
    }
}
