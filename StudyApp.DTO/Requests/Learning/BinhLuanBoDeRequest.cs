using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo bình luận bộ đề
    public class TaoBinhLuanBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string NoiDung { get; set; } = null!;

        public int? MaBinhLuanCha { get; set; }
    }

    // Request cập nhật bình luận
    public class CapNhatBinhLuanBoDeRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string NoiDung { get; set; } = null!;
    }

    // Request xóa bình luận
    public class XoaBinhLuanBoDeRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }
    }

    // Request thích bình luận bộ đề
    public class ThichBinhLuanBoDeRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }
    }

    // Request lấy danh sách bình luận
    public class LayBinhLuanBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        public int? MaBinhLuanCha { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}