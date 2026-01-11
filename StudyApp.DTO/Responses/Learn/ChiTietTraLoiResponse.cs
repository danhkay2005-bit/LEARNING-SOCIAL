using System;

namespace StudyApp.DTO.Responses.Learn
{
    public class ChiTietTraLoiResponse
    {
        public int MaTraLoi { get; set; }

        public int MaPhien { get; set; }

        public int MaThe { get; set; }

        public bool TraLoiDung { get; set; }

        public string? CauTraLoiUser { get; set; }

        public string? DapAnDung { get; set; }

        public int? ThoiGianTraLoiGiay { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
