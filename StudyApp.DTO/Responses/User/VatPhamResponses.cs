using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin vật phẩm
    /// </summary>
    public class VatPhamResponse
    {
        public int MaVatPham { get; set; }

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

        public DateTime? ThoiGianTao { get; set; }
    }
}
