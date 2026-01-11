using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class SuaTheFlashcardRequest : TaoTheFlashcardRequest
    {
        [Range(1, int.MaxValue)]
        public int MaThe { get; set; }
    }
}