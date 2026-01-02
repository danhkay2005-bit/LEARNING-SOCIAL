using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request chặn người dùng
    public class ChanNguoiDungRequest
    {
        [Required(ErrorMessage = "Mã người bị chặn là bắt buộc")]
        public Guid MaNguoiBiChan { get; set; }

        [MaxLength(500, ErrorMessage = "Lý do không được vượt quá 500 ký tự")]
        public string? LyDo { get; set; }
    }

    // Request bỏ chặn người dùng
    public class BoChanNguoiDungRequest
    {
        [Required(ErrorMessage = "Mã người bị chặn là bắt buộc")]
        public Guid MaNguoiBiChan { get; set; }
    }

    // Request lấy danh sách người bị chặn
    public class LayDanhSachChanRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}