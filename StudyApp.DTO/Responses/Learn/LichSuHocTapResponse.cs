using System;

namespace StudyApp.DTO.Responses.Learn
{
    public class LichSuHocTapResponse
    {
        public int MaLichSu { get; set; }
        public string TenBoDe { get; set; } = null!;
        public DateTime ThoiGianHoc { get; set; }

        public int SoTheHoc { get; set; }
        public double TyLeDung { get; set; }
        public int ThoiGianHocPhut { get; set; }
    }
}