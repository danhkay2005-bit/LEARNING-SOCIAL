using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class GuiCauTraLoiRequest
    {
        [Required] public int MaPhien { get; set; }
        [Required] public int MaThe { get; set; }

        public bool TraLoiDung { get; set; }

        // Lưu lại log câu trả lời
        public string CauTraLoiUser { get; set; } = null!;
        public string DapAnDungSnapshot { get; set; } = null!;

        public int ThoiGianTraLoiGiay { get; set; }
    }
}