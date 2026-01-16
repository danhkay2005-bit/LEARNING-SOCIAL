using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response tiến độ nhiệm vụ của người dùng
    /// </summary>
    public class TienDoNhiemVuResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int MaNhiemVu { get; set; }
        public string? TenNhiemVu { get; set; }
        public string? MoTa { get; set; }
        public int TienDoHienTai { get; set; }

        public string? BieuTuong { get; set; }

        public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }

        public int DieuKienDatDuoc { get; set; }
        public bool DaHoanThanh { get; set; }
        public bool DaNhanThuong { get; set; }
        public int ThuongVang { get; set; }
        public int ThuongKimCuong { get; set; }
        public int ThuongXP { get; set; }

        public int PhanTramHoanThanh
        {
            get
            {
                if (DieuKienDatDuoc == 0) return 100;
                int percent = (TienDoHienTai * 100) / DieuKienDatDuoc;
                return percent > 100 ? 100 : percent;
            }
        }

        public DateOnly? NgayBatDau { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
    }
}