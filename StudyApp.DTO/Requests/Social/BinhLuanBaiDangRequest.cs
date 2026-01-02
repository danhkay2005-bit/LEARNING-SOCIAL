using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request tạo bình luận mới
    public class TaoBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string NoiDung { get; set; } = null!;

        public string? HinhAnh { get; set; }

        public int? MaStickerDung { get; set; }

        public int? MaBinhLuanCha { get; set; }

        public List<Guid>? MentionNguoiDungs { get; set; }
    }

    // Request cập nhật bình luận
    public class CapNhatBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Nội dung không được vượt quá 2000 ký tự")]
        public string NoiDung { get; set; } = null!;

        public string? HinhAnh { get; set; }

        public int? MaStickerDung { get; set; }

        public List<Guid>? MentionNguoiDungs { get; set; }
    }

    // Request xóa bình luận
    public class XoaBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bình luận là bắt buộc")]
        public int MaBinhLuan { get; set; }
    }

    // Request lấy danh sách bình luận
    public class LayBinhLuanRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        public int? MaBinhLuanCha { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}