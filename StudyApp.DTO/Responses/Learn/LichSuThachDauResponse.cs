using System;

namespace StudyApp.DTO.Responses.Learn
{
    public class LichSuThachDauResponse
    {
        public int MaLichSu { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string TenNguoiDung { get; set; } = string.Empty;

        // Thông tin bộ đề
        public int? MaBoDe { get; set; }
        public string? TenBoDe { get; set; } // Thêm trường này để hiển thị lên UI


        public int ThoiGianLamBaiGiay { get; set; }

        public int MaThachDauGoc { get; set; }
        public int Diem { get; set; }
        public int SoTheDung { get; set; }
        public int SoTheSai { get; set; }
        public bool LaNguoiThang { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        // Bạn có thể thêm trường này để tính % chính xác nhanh trên UI
        public double TyLeDung => (SoTheDung + SoTheSai) > 0
            ? Math.Round((double)SoTheDung / (SoTheDung + SoTheSai) * 100, 1)
            : 0;
    }

    public class ThachDauNguoiChoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string? TenNguoiDung { get; set; } // Để hiện lên BXH

        public int ThoiGianLamBaiGiay { get; set; }
        public int? Diem { get; set; }
        public int? SoTheDung { get; set; }
        public int? SoTheSai { get; set; }
        public bool? LaNguoiThang { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}