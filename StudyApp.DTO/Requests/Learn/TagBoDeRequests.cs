using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    // Gắn tag vào bộ đề
    public class GanTagBoDeRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        [Required]
        public int MaTag { get; set; }
    }

    // Gỡ tag khỏi bộ đề
    public class GoTagBoDeRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        [Required]
        public int MaTag { get; set; }
    }
}
