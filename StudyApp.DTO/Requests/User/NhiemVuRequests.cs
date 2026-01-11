using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo nhiệm vụ (Admin / System)
    /// </summary>
    public class TaoNhiemVuRequest
    {
        public string TenNhiemVu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }

        public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }
        public LoaiDieuKienEnum LoaiDieuKien { get; set; }

        public int DieuKienDatDuoc { get; set; }

        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }
        public int? ThuongXp { get; set; }

        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
    }

    /// <summary>
    /// Request cập nhật nhiệm vụ
    /// </summary>
    public class CapNhatNhiemVuRequest
    {
        public string TenNhiemVu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }

        public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }
        public LoaiDieuKienEnum LoaiDieuKien { get; set; }

        public int DieuKienDatDuoc { get; set; }

        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }
        public int? ThuongXp { get; set; }

        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }

        public bool ConHieuLuc { get; set; }
    }
}
