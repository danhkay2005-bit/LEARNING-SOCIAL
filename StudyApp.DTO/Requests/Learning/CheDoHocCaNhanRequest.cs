using StudyApp.DTO.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request cập nhật chế độ học cá nhân
    public class CapNhatCheDoHocRequest
    {
        [Range(1, 200, ErrorMessage = "Số thẻ mới mỗi ngày phải từ 1-200")]
        public int? SoTheMoiMoiNgay { get; set; }

        [Range(1, 500, ErrorMessage = "Số thẻ ôn tập tối đa phải từ 1-500")]
        public int? SoTheOnTapToiDa { get; set; }

        [Range(1, 60, ErrorMessage = "Thời gian hiện câu hỏi phải từ 1-60 giây")]
        public int? ThoiGianHienCauHoi { get; set; }

        [Range(1, 60, ErrorMessage = "Thời gian hiện đáp án phải từ 1-60 giây")]
        public int? ThoiGianHienDapAn { get; set; }

        [Range(10, 300, ErrorMessage = "Thời gian mỗi thẻ tối đa phải từ 10-300 giây")]
        public int? ThoiGianMoiTheToiDa { get; set; }

        public ThuTuHocEnum? ThuTuHoc { get; set; }

        public bool? UuTienTheKho { get; set; }

        public bool? UuTienTheSapHetHan { get; set; }

        public bool? TronDapAnTracNghiem { get; set; }

        public bool? HienGoiY { get; set; }

        public bool? HienGiaiThich { get; set; }

        public bool? HienThongKeSauPhien { get; set; }

        public bool? BatAmThanh { get; set; }

        public bool? TuDongPhatAm { get; set; }

        public string? AmThanhDung { get; set; }

        public string? AmThanhSai { get; set; }
    }

    // Request reset chế độ học về mặc định
    public class ResetCheDoHocRequest
    {
        public bool XacNhan { get; set; } = false;
    }
}