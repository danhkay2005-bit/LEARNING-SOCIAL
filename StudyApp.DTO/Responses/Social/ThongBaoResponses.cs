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

        // Thời gian tương đối (optional)
        public string? ThoiGianTuongDoi { get; set; }
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

        public string? ThoiGianTuongDoi { get; set; }
    }

    /// <summary>
    /// Response thống kê thông báo
    /// </summary>
    public class ThongKeThongBaoResponse
    {
        public int TongSoThongBao { get; set; }

        public int SoThongBaoChuaDoc { get; set; }

        public int SoThongBaoDaDoc { get; set; }

        public DateTime? ThongBaoMoiNhat { get; set; }
    }

    /// <summary>
    /// Response phân trang thông báo
    /// </summary>
    public class PhanTrangThongBaoResponse
    {
        public int TrangHienTai { get; set; }

        public int KichThuocTrang { get; set; }

        public int TongSoTrang { get; set; }

        public int TongSoDong { get; set; }

        public System.Collections.Generic.List<ThongBaoResponse> DanhSach { get; set; } = new();
    }
}
