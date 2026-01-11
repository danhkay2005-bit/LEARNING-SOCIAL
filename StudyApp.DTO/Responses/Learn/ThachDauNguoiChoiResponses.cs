using System;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response thông tin người chơi trong thách đấu
    /// </summary>
    public class ThachDauNguoiChoiResponse
    {
        public int MaThachDau { get; set; }

        public Guid MaNguoiDung { get; set; }

        public int? Diem { get; set; }

        public int? SoTheDung { get; set; }

        public int? SoTheSai { get; set; }

        public int? ThoiGianLamBaiGiay { get; set; }

        public bool? LaNguoiThang { get; set; }
    }

    /// <summary>
    /// Response rút gọn cho bảng xếp hạng / kết quả
    /// </summary>
    public class ThachDauNguoiChoiSummaryResponse
    {
        public Guid MaNguoiDung { get; set; }

        public int? Diem { get; set; }

        public int? ThoiGianLamBaiGiay { get; set; }

        public bool? LaNguoiThang { get; set; }
    }
}
