using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request thích bình luận
    public class ThichBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }
    }

    // Request bỏ thích bình luận
    public class BoThichBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }
    }

    // Request lấy danh sách người thích
    public class LayNguoiThichBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}