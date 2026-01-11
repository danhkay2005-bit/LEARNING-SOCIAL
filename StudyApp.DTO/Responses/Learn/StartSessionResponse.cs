using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learn
{
    public class StartSessionResponse
    {
        public int MaPhien { get; set; }
        public int MaBoDe { get; set; }

        public string TieuDeBoDe { get; set; } = null!;
        public List<FlashcardResponse> DanhSachThe { get; set; } = [];
    }
}