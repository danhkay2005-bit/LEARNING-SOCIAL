using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoThachDauRequest
    {
        [Required] public int MaBoDe { get; set; }
        [Required] public Guid NguoiTao { get; set; }
    }
}