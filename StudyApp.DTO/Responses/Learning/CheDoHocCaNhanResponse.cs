using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.Learning
{
    // Response chế độ học cá nhân
    public class CheDoHocCaNhanResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int SoTheMoiMoiNgay { get; set; }
        public int SoTheOnTapToiDa { get; set; }
        public int ThoiGianHienCauHoi { get; set; }
        public int ThoiGianHienDapAn { get; set; }
        public int ThoiGianMoiTheToiDa { get; set; }
        public ThuTuHocEnum ThuTuHoc { get; set; }
        public bool UuTienTheKho { get; set; }
        public bool UuTienTheSapHetHan { get; set; }
        public bool TronDapAnTracNghiem { get; set; }
        public bool HienGoiY { get; set; }
        public bool HienGiaiThich { get; set; }
        public bool HienThongKeSauPhien { get; set; }
        public bool BatAmThanh { get; set; }
        public bool TuDongPhatAm { get; set; }
        public string? AmThanhDung { get; set; }
        public string? AmThanhSai { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
    }

    // Response kết quả cập nhật chế độ học
    public class KetQuaCheDoHocResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public CheDoHocCaNhanResponse? CheDo { get; set; }
    }
}