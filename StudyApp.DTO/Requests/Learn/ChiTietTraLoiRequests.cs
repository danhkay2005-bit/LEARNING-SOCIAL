using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class ChiTietTraLoiRequest
    {
        [Required]
        public int MaPhien { get; set; }

        [Required]
        public int MaThe { get; set; }
        public string? CauTraLoiUser { get; set; }
        public string? DapAnDung { get; set; }

        public bool TraLoiDung { get; set; }
        public int? ThoiGianTraLoiGiay { get; set; }
    }
}
