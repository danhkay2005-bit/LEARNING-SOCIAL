using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response thông tin bộ đề học
    /// </summary>
    public class BoDeHocResponse
    {
        public int MaBoDe { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int? MaChuDe { get; set; }
        public int? MaThuMuc { get; set; }

        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; }

        public bool LaCongKhai { get; set; }
        public bool ChoPhepBinhLuan { get; set; }

        public int SoLuongThe { get; set; }
        public int SoLuotHoc { get; set; }
        public int SoLuotChiaSe { get; set; }

        public bool DaXoa { get; set; }

        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
    }
}
