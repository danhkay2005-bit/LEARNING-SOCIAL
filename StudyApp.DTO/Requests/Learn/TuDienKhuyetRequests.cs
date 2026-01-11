using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo từ điền khuyết cho thẻ
    /// </summary>
    public class TaoTuDienKhuyetRequest
    {
        [Required]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Từ cần điền là bắt buộc")]
        [MaxLength(200)]
        public string TuCanDien { get; set; } = null!;

        /// <summary>
        /// Vị trí của từ trong câu (index)
        /// </summary>
        [Required]
        public int ViTriTrongCau { get; set; }
    }

    /// <summary>
    /// Request cập nhật từ điền khuyết
    /// </summary>
    public class CapNhatTuDienKhuyetRequest
    {
        [Required]
        public int MaTuDienKhuyet { get; set; }

        [Required(ErrorMessage = "Từ cần điền là bắt buộc")]
        [MaxLength(200)]
        public string TuCanDien { get; set; } = null!;

        [Required]
        public int ViTriTrongCau { get; set; }
    }

    /// <summary>
    /// Request xóa từ điền khuyết
    /// </summary>
    public class XoaTuDienKhuyetRequest
    {
        [Required]
        public int MaTuDienKhuyet { get; set; }
    }
}
