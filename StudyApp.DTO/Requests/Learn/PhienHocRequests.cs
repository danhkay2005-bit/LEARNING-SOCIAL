using System;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request bắt đầu một phiên học
    /// </summary>
    public class BatDauPhienHocRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        public int? MaBoDe { get; set; }

        public int? MaThachDau { get; set; }

        [Required]
        public LoaiPhienHocEnum LoaiPhien { get; set; }
    }

    /// <summary>
    /// Request kết thúc phiên học
    /// </summary>
    public class KetThucPhienHocRequest
    {
        [Required]
        public int MaPhien { get; set; }

        public int? ThoiGianHocGiay { get; set; }

        public int? TongSoThe { get; set; }

        public int? SoTheMoi { get; set; }

        public int? SoTheOnTap { get; set; }

        public int? SoTheDung { get; set; }

        public int? SoTheSai { get; set; }

        public double? TyLeDung { get; set; }
    }
}
