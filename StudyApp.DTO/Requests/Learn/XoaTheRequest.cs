using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class XoaTheRequest
    {
        [Required]
        public int MaThe { get; set; }
        public int MaBoDe { get; set; }
    }
}