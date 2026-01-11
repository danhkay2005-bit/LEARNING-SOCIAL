using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo thành tựu (Admin / System)
    /// </summary>
    public class TaoThanhTuuRequest
    {
        public string TenThanhTuu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }
        public string? HinhHuy { get; set; }

        public LoaiThanhTuuEnum LoaiThanhTuu { get; set; }
        public LoaiDieuKienEnum DieuKienLoai { get; set; }
        public int DieuKienGiaTri { get; set; }

        public int? ThuongXp { get; set; }
        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }

        public DoHiemEnum DoHiem { get; set; } = DoHiemEnum.PhoBien;
        public bool BiAn { get; set; } = false;
    }

    /// <summary>
    /// Request cập nhật thành tựu
    /// </summary>
    public class CapNhatThanhTuuRequest
    {
        public string TenThanhTuu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }
        public string? HinhHuy { get; set; }

        public LoaiThanhTuuEnum LoaiThanhTuu { get; set; }
        public LoaiDieuKienEnum DieuKienLoai { get; set; }
        public int DieuKienGiaTri { get; set; }

        public int? ThuongXp { get; set; }
        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }

        public DoHiemEnum DoHiem { get; set; }
        public bool BiAn { get; set; }
    }
}
