using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ thông tin flashcard
    /// </summary>
    public class TheFlashcardResponse
    {
        public int MaThe { get; set; }

        public int MaBoDe { get; set; }

        public LoaiTheEnum LoaiThe { get; set; }

        public string MatTruoc { get; set; } = null!;

        public string MatSau { get; set; } = null!;

        public string? GiaiThich { get; set; }

        public string? HinhAnhTruoc { get; set; }

        public string? HinhAnhSau { get; set; }

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }

        public int SoLuongHoc { get; set; }

        public int SoLanDung { get; set; }

        public int SoLanSai { get; set; }

        public DateTime? NgayOnTapTiepTheo { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public DateTime? ThoiGianCapNhat { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho danh sách thẻ
    /// </summary>
    public class TheFlashcardSummaryResponse
    {
        public int MaThe { get; set; }

        public LoaiTheEnum LoaiThe { get; set; }

        public string MatTruoc { get; set; } = null!;

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }
    }
}
