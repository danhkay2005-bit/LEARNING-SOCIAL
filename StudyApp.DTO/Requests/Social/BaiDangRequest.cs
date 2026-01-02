using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request tạo bài đăng mới
    public class TaoBaiDangRequest
    {
        [MaxLength(5000, ErrorMessage = "Nội dung không được vượt quá 5000 ký tự")]
        public string? NoiDung { get; set; }

        [Required(ErrorMessage = "Loại bài đăng là bắt buộc")]
        public LoaiBaiDangEnum LoaiBaiDang { get; set; }

        public string? HinhAnh { get; set; }

        public string? Video { get; set; }

        public int? MaBoDeLienKet { get; set; }

        public int? MaThanhTuuLienKet { get; set; }

        public int? MaThachDauLienKet { get; set; }

        public int? SoChuoiNgay { get; set; }

        public QuyenRiengTuEnum QuyenRiengTu { get; set; } = QuyenRiengTuEnum.CongKhai;

        public bool TatBinhLuan { get; set; } = false;

        public List<string>? Hashtags { get; set; }

        public List<Guid>? MentionNguoiDungs { get; set; }
    }

    // Request cập nhật bài đăng
    public class CapNhatBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        [MaxLength(5000, ErrorMessage = "Nội dung không được vượt quá 5000 ký tự")]
        public string? NoiDung { get; set; }

        public string? HinhAnh { get; set; }

        public string? Video { get; set; }

        public QuyenRiengTuEnum? QuyenRiengTu { get; set; }

        public bool? TatBinhLuan { get; set; }

        public List<string>? Hashtags { get; set; }

        public List<Guid>? MentionNguoiDungs { get; set; }
    }

    // Request ghim/bỏ ghim bài đăng
    public class GhimBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }

        public bool GhimBaiDang { get; set; }
    }

    // Request xóa bài đăng
    public class XoaBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
        public int MaBaiDang { get; set; }
    }

    // Request lấy danh sách bài đăng với phân trang
    public class LayBaiDangRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public LoaiBaiDangEnum? LoaiBaiDang { get; set; }
        public string? Hashtag { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}