using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response chi tiết trả lời
    public class ChiTietTraLoiResponse
    {
        public int MaTraLoi { get; set; }
        public int MaPhien { get; set; }
        public int MaThe { get; set; }
        public TheFlashcardTomTatResponse? The { get; set; }
        public bool TraLoiDung { get; set; }
        public string? CauTraLoiUser { get; set; }
        public string? DapAnDung { get; set; }
        public int ThoiGianTraLoiGiay { get; set; }
        public MucDoKhoEnum? DoKhoUserDanhGia { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách chi tiết trả lời
    public class DanhSachChiTietTraLoiResponse
    {
        public int MaPhien { get; set; }
        public List<ChiTietTraLoiResponse> ChiTietTraLois { get; set; } = [];
        public int TongSo { get; set; }
        public int SoDung { get; set; }
        public int SoSai { get; set; }
    }

    // Response gửi câu trả lời
    public class GuiCauTraLoiResponse
    {
        public bool ThanhCong { get; set; }
        public bool TraLoiDung { get; set; }
        public string? DapAnDung { get; set; }
        public string? GiaiThich { get; set; }
        public int DiemNhan { get; set; }
        public TrangThaiSRSEnum? TrangThaiSRSMoi { get; set; }
        public int? TheIndex { get; set; }
        public int? TongSoThe { get; set; }
    }
}