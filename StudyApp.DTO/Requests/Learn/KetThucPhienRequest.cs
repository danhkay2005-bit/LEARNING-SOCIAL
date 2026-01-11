using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class KetThucPhienRequest
    {
        [Required] public int MaPhien { get; set; }
        public int ThoiGianHocTongCongGiay { get; set; }
    }
}