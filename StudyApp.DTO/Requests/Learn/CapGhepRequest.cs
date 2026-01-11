using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class CapGhepRequest
    {
        public int? MaCap { get; set; }
        public string VeTrai { get; set; } = null!;
        public string VePhai { get; set; } = null!;
        public int ThuTu { get; set; }
    }
}