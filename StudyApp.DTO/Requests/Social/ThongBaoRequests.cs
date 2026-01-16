using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Social
{
    /// <summary>
    /// Request lấy danh sách thông báo của người dùng
    /// </summary>
    public class DanhSachThongBaoRequest
    {
        [Required]
        public Guid MaNguoiNhan { get; set; }

        /// <summary>
        /// Chỉ lấy thông báo chưa đọc
        /// </summary>
        public bool? ChiChuaDoc { get; set; }

        /// <summary>
        /// Lọc theo loại thông báo
        /// </summary>
        public LoaiThongBaoEnum? LoaiThongBao { get; set; }

        /// <summary>
        /// Phân trang
        /// </summary>
        public int Trang { get; set; } = 1;

        public int KichThuocTrang { get; set; } = 20;
    }

    /// <summary>
    /// Request đánh dấu một thông báo là đã đọc
    /// </summary>
    public class DanhDauThongBaoDaDocRequest
    {
        [Required]
        public int MaThongBao { get; set; }
    }

    /// <summary>
    /// Request đánh dấu tất cả thông báo là đã đọc
    /// </summary>
    public class DanhDauTatCaThongBaoDaDocRequest
    {
        [Required]
        public Guid MaNguoiNhan { get; set; }
    }

    /// <summary>
    /// Request tạo thông báo thủ công (hệ thống)
    /// </summary>
    public class TaoThongBaoRequest
    {
        [Required]
        public Guid MaNguoiNhan { get; set; }

        [Required]
        public LoaiThongBaoEnum LoaiThongBao { get; set; }

        [Required]
        [StringLength(500)]
        public string NoiDung { get; set; } = null!;

        public int? MaBaiDang { get; set; }

        public int? MaBinhLuan { get; set; }

        public int? MaThachDau { get; set; }

        public Guid? MaNguoiGayRa { get; set; }
    }

    /// <summary>
    /// Request xóa thông báo
    /// </summary>
    public class XoaThongBaoRequest
    {
        [Required]
        public int MaThongBao { get; set; }

        [Required]
        public Guid MaNguoiNhan { get; set; }
    }
}
