using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ tiến độ học tập của một thẻ
    /// </summary>
    public class TienDoHocTapResponse
    {
        public int MaTienDo { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int MaThe { get; set; }

        public TrangThaiHocEnum TrangThai { get; set; }

        public double? HeSoDe { get; set; }

        public int? KhoangCachNgay { get; set; }

        public int? SoLanLap { get; set; }

        public DateTime? NgayOnTapTiepTheo { get; set; }

        public int? SoLanDung { get; set; }

        public int? SoLanSai { get; set; }

        public DateTime? LanHocCuoi { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho lịch ôn tập / danh sách cần học
    /// </summary>
    public class TienDoHocTapSummaryResponse
    {
        public int MaThe { get; set; }

        public TrangThaiHocEnum TrangThai { get; set; }

        public DateTime? NgayOnTapTiepTheo { get; set; }
    }
}
