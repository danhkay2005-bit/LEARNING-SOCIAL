using System;

namespace StudyApp.DTO.Responses.Learn
{
    public class KetQuaTraLoiResponse
    {
        public int MaThe { get; set; }
        public bool TraLoiDung { get; set; }

        public string DapAnDung { get; set; } = null!;
        public string? GiaiThich { get; set; }

        public int XPCongThem { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
    }
}