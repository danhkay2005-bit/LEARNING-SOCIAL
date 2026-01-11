using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class PhanTuSapXepRequest
    {
        public int? MaPhanTu { get; set; }

        [Required]
        public string NoiDung { get; set; } = null!;

        public int ThuTuDung { get; set; }
    }
}