using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.NguoiDung
{
    // ============================
    // ADMIN – QUẢN LÝ VẬT PHẨM
    // ============================

    /// <summary>
    /// Tạo vật phẩm mới (Admin)
    /// </summary>
    public class TaoVatPhamRequest
    {
        [Required(ErrorMessage = "Tên vật phẩm là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên vật phẩm tối đa 200 ký tự")]
        public string TenVatPham { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Giá vật phẩm là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá không được âm")]
        public int Gia { get; set; }

        [Required(ErrorMessage = "Loại tiền tệ là bắt buộc")]
        public LoaiTienTeEnum LoaiTienTe { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public int MaDanhMuc { get; set; }

        public string? DuongDanHinh { get; set; }

        /// <summary>
        /// Thời hạn sử dụng (phút). Null = vĩnh viễn
        /// </summary>
        public int? ThoiHanPhut { get; set; }

        /// <summary>
        /// Giá trị hiệu ứng (XP boost, vàng boost, v.v.)
        /// </summary>
        public double? GiaTriHieuUng { get; set; }

        /// <summary>
        /// Độ hiếm (1–5)
        /// </summary>
        [Range(1, 5, ErrorMessage = "Độ hiếm từ 1 đến 5")]
        public byte? DoHiem { get; set; }

        /// <summary>
        /// Còn được bán hay không
        /// </summary>
        public bool? ConHang { get; set; } = true;
    }

    /// <summary>
    /// Cập nhật vật phẩm (Admin – PATCH)
    /// </summary>
    public class CapNhatVatPhamRequest
    {
        [StringLength(200, ErrorMessage = "Tên vật phẩm tối đa 200 ký tự")]
        public string? TenVatPham { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? MoTa { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá không được âm")]
        public int? Gia { get; set; }

        public LoaiTienTeEnum? LoaiTienTe { get; set; }

        public int? MaDanhMuc { get; set; }

        public string? DuongDanHinh { get; set; }

        public int? ThoiHanPhut { get; set; }

        public double? GiaTriHieuUng { get; set; }

        [Range(1, 5, ErrorMessage = "Độ hiếm từ 1 đến 5")]
        public byte? DoHiem { get; set; }

        public bool? ConHang { get; set; }
    }

    // ============================
    // USER – NGHIỆP VỤ
    // ============================

    /// <summary>
    /// Mua vật phẩm
    /// </summary>
    public class MuaVatPhamRequest
    {
        [Required(ErrorMessage = "Mã vật phẩm là bắt buộc")]
        public int MaVatPham { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
        public int SoLuong { get; set; } = 1;
    }

    /// <summary>
    /// Lấy danh sách cửa hàng (filter)
    /// </summary>
    public class LayCuaHangRequest
    {
        public int? MaDanhMuc { get; set; }

        public byte? DoHiem { get; set; }

        /// <summary>
        /// Chỉ lấy vật phẩm còn hàng
        /// </summary>
        public bool? ChiConHang { get; set; } = true;
    }
}
