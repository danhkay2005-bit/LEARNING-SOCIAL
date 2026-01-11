using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    public class BoDeHocResponse
    {
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; }
        public bool LaCongKhai { get; set; }

        public int SoLuongThe { get; set; }
        public int SoLuotHoc { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}