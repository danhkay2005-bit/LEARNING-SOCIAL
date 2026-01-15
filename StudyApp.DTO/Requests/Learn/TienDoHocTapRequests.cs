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
        // Cần thiết để định danh bản ghi (Upsert)
        public int MaThe { get; set; }
        public Guid MaNguoiDung { get; set; }

        public int? MaTienDo { get; set; } // Có thể null nếu là thẻ mới
        public TrangThaiHocEnum? TrangThai { get; set; } // Đóng vai trò là Quality (0-5)

        // Các trường phục vụ thuật toán SM-2
        public double? HeSoDe { get; set; }
        public int? KhoangCachNgay { get; set; }
        public int? SoLanLap { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
    }
}
