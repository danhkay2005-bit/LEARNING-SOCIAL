using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo thư mục mới
    /// </summary>
    public class TaoThuMucRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Tên thư mục là bắt buộc")]
        [MaxLength(200)]
        public string TenThuMuc { get; set; } = null!;

        public string? MoTa { get; set; }

        /// <summary>
        /// Thư mục cha (null nếu là thư mục gốc)
        /// </summary>
        public int? MaThuMucCha { get; set; }

        public int? ThuTu { get; set; }
    }

    /// <summary>
    /// Request cập nhật thư mục
    /// </summary>
    public class CapNhatThuMucRequest
    {
        [Required]
        public int MaThuMuc { get; set; }

        [Required(ErrorMessage = "Tên thư mục là bắt buộc")]
        [MaxLength(200)]
        public string TenThuMuc { get; set; } = null!;

        public string? MoTa { get; set; }

        public int? MaThuMucCha { get; set; }

        public int? ThuTu { get; set; }
    }

    /// <summary>
    /// Request xóa thư mục
    /// </summary>
    public class XoaThuMucRequest
    {
        [Required]
        public int MaThuMuc { get; set; }
    }
}
