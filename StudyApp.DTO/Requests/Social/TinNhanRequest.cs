using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request gửi tin nhắn
    public class GuiTinNhanRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [MaxLength(5000, ErrorMessage = "Nội dung không được vượt quá 5000 ký tự")]
        public string? NoiDung { get; set; }

        [Required(ErrorMessage = "Loại tin nhắn là bắt buộc")]
        public LoaiTinNhanEnum LoaiTinNhan { get; set; }

        public string? DuongDanFile { get; set; }

        public string? TenFile { get; set; }

        public int? KichThuocFile { get; set; }

        public int? MaStickerDung { get; set; }

        public int? MaBoDeDinhKem { get; set; }

        public int? MaThachDauDinhKem { get; set; }

        public int? ReplyToId { get; set; }
    }

    // Request thu hồi tin nhắn
    public class ThuHoiTinNhanRequest
    {
        [Required(ErrorMessage = "Mã tin nhắn là bắt buộc")]
        public int MaTinNhan { get; set; }
    }

    // Request lấy danh sách tin nhắn
    public class LayTinNhanRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        public int? TruocTinNhanId { get; set; }
        public int? SauTinNhanId { get; set; }
        public int PageSize { get; set; } = 50;
    }

    // Request tìm kiếm tin nhắn
    public class TimKiemTinNhanRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [Required(ErrorMessage = "Từ khóa tìm kiếm là bắt buộc")]
        [MinLength(2, ErrorMessage = "Từ khóa phải có ít nhất 2 ký tự")]
        public string TuKhoa { get; set; } = null!;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}