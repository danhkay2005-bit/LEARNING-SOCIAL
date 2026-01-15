using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin thông báo
    /// </summary>
    public class ThongBaoResponse
    {
        public int MaThongBao { get; set; }

        public Guid MaNguoiNhan { get; set; }

        public LoaiThongBaoEnum LoaiThongBao { get; set; }

        public string NoiDung { get; set; } = null!;

        public bool DaDoc { get; set; }

        public DateTime ThoiGian { get; set; }

        // Tham chiếu mềm
        public int? MaBaiDang { get; set; }
        public int? MaBinhLuan { get; set; }
        public int? MaThachDau { get; set; }

        public Guid? MaNguoiGayRa { get; set; }
    }

    /// <summary>
    /// Response rút gọn (badge / dropdown)
    /// </summary>
    public class ThongBaoSummaryResponse
    {
        public int MaThongBao { get; set; }

        public LoaiThongBaoEnum LoaiThongBao { get; set; }

        public string NoiDung { get; set; } = null!;

        public bool DaDoc { get; set; }

        public DateTime ThoiGian { get; set; }
    }
}
