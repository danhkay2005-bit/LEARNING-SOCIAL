using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request gửi lời mời kết bạn
    public class GuiLoiMoiKetBanRequest
    {
        [Required(ErrorMessage = "Mã người nhận là bắt buộc")]
        public Guid MaNguoiNhan { get; set; }
    }

    // Request phản hồi lời mời kết bạn
    public class PhanHoiLoiMoiKetBanRequest
    {
        [Required(ErrorMessage = "Mã người gửi là bắt buộc")]
        public Guid MaNguoiGui { get; set; }

        [Required(ErrorMessage = "Trạng thái phản hồi là bắt buộc")]
        public bool ChapNhan { get; set; }
    }

    // Request hủy kết bạn
    public class HuyKetBanRequest
    {
        [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
        public Guid MaNguoiDung { get; set; }
    }

    // Request lấy danh sách bạn bè
    public class LayDanhSachBanBeRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public TrangThaiBanBeEnum? TrangThai { get; set; }
        public string? TuKhoa { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}