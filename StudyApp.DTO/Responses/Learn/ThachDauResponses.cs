using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ thông tin thách đấu
    /// </summary>
    public class ThachDauResponse
    {
        public int MaThachDau { get; set; }

        public int MaBoDe { get; set; }

        public Guid NguoiTao { get; set; }

        public TrangThaiThachDauEnum TrangThai { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho danh sách thách đấu
    /// </summary>
    public class ThachDauSummaryResponse
    {
        public int MaThachDau { get; set; }

        public TrangThaiThachDauEnum TrangThai { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }
    }
}
