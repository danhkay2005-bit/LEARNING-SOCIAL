using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class TuDienKhuyetRequest
    {
        public int? MaTuDienKhuyet { get; set; }
        public string TuCanDien { get; set; } = null!;
        public int ViTriTrongCau { get; set; }
    }
}