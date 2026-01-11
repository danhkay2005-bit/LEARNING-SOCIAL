using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request khởi tạo tiến độ học tập cho một thẻ
    /// </summary>
    public class TaoTienDoHocTapRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required]
        public int MaThe { get; set; }

        /// <summary>
        /// Trạng thái ban đầu của thẻ
        /// </summary>
        public TrangThaiHocEnum TrangThai { get; set; } = TrangThaiHocEnum.New;
    }

    /// <summary>
    /// Request cập nhật tiến độ học tập (sau mỗi lần học)
    /// </summary>
    public class CapNhatTienDoHocTapRequest
    {
        [Required]
        public int MaTienDo { get; set; }

        public TrangThaiHocEnum? TrangThai { get; set; }

        /// <summary>
        /// Hệ số dễ (SM-2)
        /// </summary>
        public double? HeSoDe { get; set; }

        /// <summary>
        /// Khoảng cách ôn tập (ngày)
        /// </summary>
        public int? KhoangCachNgay { get; set; }

        public int? SoLanLap { get; set; }

        public DateTime? NgayOnTapTiepTheo { get; set; }

        public int? SoLanDung { get; set; }

        public int? SoLanSai { get; set; }

        public DateTime? LanHocCuoi { get; set; }
    }
}
