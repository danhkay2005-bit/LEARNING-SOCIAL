using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoChuDeRequest
    {
        [Required]
        public string TenChuDe { get; set; } = null!;

        public string? MoTa { get; set; }
    }
}