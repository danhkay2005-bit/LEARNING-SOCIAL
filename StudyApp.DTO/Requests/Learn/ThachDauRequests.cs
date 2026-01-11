using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo thách đấu mới
    /// </summary>
    public class TaoThachDauRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        [Required]
        public Guid NguoiTao { get; set; }
    }

    /// <summary>
    /// Request bắt đầu thách đấu
    /// </summary>
    public class BatDauThachDauRequest
    {
        [Required]
        public int MaThachDau { get; set; }
    }

    /// <summary>
    /// Request kết thúc thách đấu
    /// </summary>
    public class KetThucThachDauRequest
    {
        [Required]
        public int MaThachDau { get; set; }
    }

    /// <summary>
    /// Request hủy thách đấu
    /// </summary>
    public class HuyThachDauRequest
    {
        [Required]
        public int MaThachDau { get; set; }
    }
}
