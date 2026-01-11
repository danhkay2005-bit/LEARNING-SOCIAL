using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class SuaBoDeRequest : TaoBoDeRequest
    {
        [Required]
        public int MaBoDe { get; set; }

        public bool DaXoa { get; set; } // Hỗ trợ Soft Delete
    }
}