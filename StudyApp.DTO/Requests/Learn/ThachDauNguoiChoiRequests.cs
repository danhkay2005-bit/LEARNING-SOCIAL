using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tham gia thách đấu
    /// </summary>
    public class ThamGiaThachDauRequest
    {
        [Required]
        public int MaThachDau { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }
    }

    /// <summary>
    /// Request cập nhật kết quả người chơi trong thách đấu
    /// </summary>
    public class CapNhatKetQuaThachDauRequest
    {
        [Required]
        public int MaThachDau { get; set; }

        [Required]
        public Guid MaNguoiDung { get; set; }

        public int? Diem { get; set; }

        public int? SoTheDung { get; set; }

        public int? SoTheSai { get; set; }

        public int? ThoiGianLamBaiGiay { get; set; }

        /// <summary>
        /// Người chơi có phải là người thắng không
        /// </summary>
        public bool? LaNguoiThang { get; set; }
    }
}
