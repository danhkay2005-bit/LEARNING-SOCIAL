using System;
using System.ComponentModel.DataAnnotations;

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
}
