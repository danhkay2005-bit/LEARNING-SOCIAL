using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin thành tựu
    /// </summary>
    public class ThanhTuuResponse
    {
        public int MaThanhTuu { get; set; }

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

        public DateTime? ThoiGianTao { get; set; }
    }
}
