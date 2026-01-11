using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ thông tin phiên học
    /// </summary>
    public class PhienHocResponse
    {
        public int MaPhien { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int? MaBoDe { get; set; }

        public int? MaThachDau { get; set; }

        public LoaiPhienHocEnum LoaiPhien { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public int? ThoiGianHocGiay { get; set; }

        public int? TongSoThe { get; set; }

        public int? SoTheMoi { get; set; }

        public int? SoTheOnTap { get; set; }

        public int? SoTheDung { get; set; }

        public int? SoTheSai { get; set; }

        public double? TyLeDung { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho danh sách phiên học
    /// </summary>
    public class PhienHocSummaryResponse
    {
        public int MaPhien { get; set; }

        public LoaiPhienHocEnum LoaiPhien { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public double? TyLeDung { get; set; }
    }
}
