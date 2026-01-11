using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class DapAnTracNghiemRequest
    {
        public int? MaDapAn { get; set; }

        [Required]
        public string NoiDung { get; set; } = null!;

        public bool LaDapAnDung { get; set; }
        public int ThuTu { get; set; }
    }
}