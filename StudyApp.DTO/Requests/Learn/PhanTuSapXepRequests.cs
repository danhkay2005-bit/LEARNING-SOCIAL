using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo phần tử sắp xếp
    /// </summary>
    public class TaoPhanTuSapXepRequest
    {
        [Required]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Nội dung phần tử là bắt buộc")]
        [MaxLength(500)]
        public string NoiDung { get; set; } = null!;

        /// <summary>
        /// Thứ tự đúng của phần tử
        /// </summary>
        [Required]
        public int ThuTuDung { get; set; }
    }

    /// <summary>
    /// Request cập nhật phần tử sắp xếp
    /// </summary>
    public class CapNhatPhanTuSapXepRequest
    {
        [Required]
        public int MaPhanTu { get; set; }

        [Required(ErrorMessage = "Nội dung phần tử là bắt buộc")]
        [MaxLength(500)]
        public string NoiDung { get; set; } = null!;

        [Required]
        public int ThuTuDung { get; set; }
    }
}
