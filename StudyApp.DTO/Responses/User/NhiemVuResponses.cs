using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin nhiệm vụ
    /// </summary>
    public class NhiemVuResponse
    {
        public int MaNhiemVu { get; set; }

        public string TenNhiemVu { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }

        public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }
        public LoaiDieuKienEnum LoaiDieuKien { get; set; }

        public int DieuKienDatDuoc { get; set; }

        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }
        public int? ThuongXp { get; set; }
        public int TienDoHienTai { get; set; }
        public bool DaHoanThanh { get; set; }
        public bool DaNhanThuong { get; set; }

        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }

        public bool ConHieuLuc { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }
}
