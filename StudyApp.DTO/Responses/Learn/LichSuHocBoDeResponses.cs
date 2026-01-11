using System;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response lịch sử học bộ đề
    /// </summary>
    public class LichSuHocBoDeResponse
    {
        public int MaLichSu { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int MaBoDe { get; set; }

        public int? MaPhien { get; set; }

        public int? SoTheHoc { get; set; }

        public double? TyLeDung { get; set; }

        public int? ThoiGianHocPhut { get; set; }

        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho danh sách / thống kê nhanh
    /// </summary>
    public class LichSuHocBoDeSummaryResponse
    {
        public int MaBoDe { get; set; }

        public int? SoTheHoc { get; set; }

        public double? TyLeDung { get; set; }

        public int? ThoiGianHocPhut { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
