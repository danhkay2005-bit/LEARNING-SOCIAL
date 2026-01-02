using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request bắt đầu phiên học
    public class BatDauPhienHocRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Loại phiên là bắt buộc")]
        public LoaiPhienHocEnum LoaiPhien { get; set; }

        public int? SoTheMuonHoc { get; set; }

        public int? MaThachDau { get; set; }

        // Tùy chọn cho phiên tùy chọn
        public TuyChonPhienHocRequest? TuyChon { get; set; }
    }

    // Request tùy chọn phiên học
    public class TuyChonPhienHocRequest
    {
        public bool ChiHocTheMoi { get; set; } = false;

        public bool ChiOnTap { get; set; } = false;

        public bool UuTienTheKho { get; set; } = false;

        public bool UuTienTheSapHetHan { get; set; } = false;

        public ThuTuHocEnum ThuTuHoc { get; set; } = ThuTuHocEnum.TuDong;

        public List<int>? ChiDinhMaThe { get; set; }

        public LoaiDanhDauTheEnum? ChiHocLoaiDanhDau { get; set; }
    }

    // Request kết thúc phiên học
    public class KetThucPhienHocRequest
    {
        [Required(ErrorMessage = "Mã phiên là bắt buộc")]
        public int MaPhien { get; set; }

        public CamXucHocEnum? CamXuc { get; set; }

        [MaxLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? GhiChu { get; set; }
    }

    // Request gửi câu trả lời
    public class GuiCauTraLoiRequest
    {
        [Required(ErrorMessage = "Mã phiên là bắt buộc")]
        public int MaPhien { get; set; }

        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }

        public bool TraLoiDung { get; set; }

        public string? CauTraLoiUser { get; set; }

        public int? ThoiGianTraLoiGiay { get; set; }

        public MucDoKhoEnum? DoKhoUserDanhGia { get; set; }
    }

    // Request gửi nhiều câu trả lời
    public class GuiNhieuCauTraLoiRequest
    {
        [Required(ErrorMessage = "Mã phiên là bắt buộc")]
        public int MaPhien { get; set; }

        [Required(ErrorMessage = "Danh sách câu trả lời là bắt buộc")]
        public List<CauTraLoiItem> CauTraLois { get; set; } = [];
    }

    public class CauTraLoiItem
    {
        public int MaThe { get; set; }
        public bool TraLoiDung { get; set; }
        public string? CauTraLoiUser { get; set; }
        public int? ThoiGianTraLoiGiay { get; set; }
        public MucDoKhoEnum? DoKhoUserDanhGia { get; set; }
    }

    // Request lấy lịch sử phiên học
    public class LayLichSuPhienHocRequest
    {
        public int? MaBoDe { get; set; }

        public LoaiPhienHocEnum? LoaiPhien { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}