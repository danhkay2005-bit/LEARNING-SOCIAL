using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo bộ đề mới
    public class TaoBoDeRequest
    {
        [Required(ErrorMessage = "Tiêu đề bộ đề là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string TieuDe { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        public string? MoTa { get; set; }

        public string? AnhBia { get; set; }

        public int? MaChuDe { get; set; }

        public int? MaThuMuc { get; set; }

        public MucDoKhoEnum? MucDoKho { get; set; }

        public bool LaCongKhai { get; set; } = true;

        public bool ChoPhepBinhLuan { get; set; } = true;

        public List<string>? Tags { get; set; }

        // Tạo kèm danh sách thẻ
        public List<TaoTheFlashcardRequest>? DanhSachThe { get; set; }
    }

    // Request cập nhật bộ đề
    public class CapNhatBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [MaxLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string? TieuDe { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        public string? MoTa { get; set; }

        public string? AnhBia { get; set; }

        public int? MaChuDe { get; set; }

        public int? MaThuMuc { get; set; }

        public MucDoKhoEnum? MucDoKho { get; set; }

        public bool? LaCongKhai { get; set; }

        public bool? ChoPhepBinhLuan { get; set; }

        public List<string>? Tags { get; set; }
    }

    // Request xóa bộ đề
    public class XoaBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }
    }

    // Request clone bộ đề
    public class CloneBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề gốc là bắt buộc")]
        public int MaBoDeGoc { get; set; }

        [MaxLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string? TieuDeMoi { get; set; }

        public int? MaThuMuc { get; set; }

        public bool GiuNguyenCaiDat { get; set; } = true;
    }

    // Request tìm kiếm bộ đề
    public class TimKiemBoDeRequest
    {
        public string? TuKhoa { get; set; }

        public int? MaChuDe { get; set; }

        public List<int>? MaTags { get; set; }

        public MucDoKhoEnum? MucDoKho { get; set; }

        public Guid? MaNguoiTao { get; set; }

        public bool? ChiCongKhai { get; set; } = true;

        public string? SapXepTheo { get; set; } // "moi_nhat", "pho_bien", "danh_gia"

        public bool SapXepGiam { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    // Request lấy bộ đề của tôi
    public class LayBoDeOfMeRequest
    {
        public int? MaThuMuc { get; set; }

        public string? TuKhoa { get; set; }

        public string? SapXepTheo { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    // Request di chuyển bộ đề vào thư mục
    public class DiChuyenBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        public int? MaThuMucMoi { get; set; }
    }
}