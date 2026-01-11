using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo vật phẩm (Admin)
    /// </summary>
    public class TaoVatPhamRequest
    {
        public string TenVatPham { get; set; } = null!;
        public string? MoTa { get; set; }

        public int Gia { get; set; }
        public LoaiTienTeEnum LoaiTienTe { get; set; }

        public int MaDanhMuc { get; set; }

        public string? DuongDanHinh { get; set; }

        /// <summary>
        /// Thời hạn sử dụng (phút), null = không giới hạn
        /// </summary>
        public int? ThoiHanPhut { get; set; }

        /// <summary>
        /// Giá trị hiệu ứng (ví dụ: Freeze = 1, HoiSinh = 1)
        /// </summary>
        public double? GiaTriHieuUng { get; set; }

        public DoHiemEnum DoHiem { get; set; } = DoHiemEnum.PhoBien;
    }

    /// <summary>
    /// Request cập nhật vật phẩm (Admin)
    /// </summary>
    public class CapNhatVatPhamRequest
    {
        public string TenVatPham { get; set; } = null!;
        public string? MoTa { get; set; }

        public int Gia { get; set; }
        public LoaiTienTeEnum LoaiTienTe { get; set; }

        public int MaDanhMuc { get; set; }

        public string? DuongDanHinh { get; set; }

        public int? ThoiHanPhut { get; set; }
        public double? GiaTriHieuUng { get; set; }

        public DoHiemEnum DoHiem { get; set; }

        public bool ConHang { get; set; }
    }
}
