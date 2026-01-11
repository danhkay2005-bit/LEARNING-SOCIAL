using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    public class TienDoTheResponse
    {
        public int MaThe { get; set; }
        public TrangThaiHocEnum TrangThai { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
    }
}