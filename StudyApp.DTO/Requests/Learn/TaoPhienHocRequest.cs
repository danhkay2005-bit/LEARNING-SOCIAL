using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoPhienHocRequest
    {
        [Required] public Guid MaNguoiDung { get; set; }

        public int? MaBoDe { get; set; }
        public int? MaThachDau { get; set; }

        public LoaiPhienHocEnum LoaiPhien { get; set; } = LoaiPhienHocEnum.HocMoi;
    }
}