using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request tạo thẻ flashcard mới
    public class TaoTheFlashcardRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Loại thẻ là bắt buộc")]
        public LoaiTheEnum LoaiThe { get; set; }

        [Required(ErrorMessage = "Mặt trước là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Mặt trước không được vượt quá 2000 ký tự")]
        public string MatTruoc { get; set; } = null!;

        [Required(ErrorMessage = "Mặt sau là bắt buộc")]
        [MaxLength(2000, ErrorMessage = "Mặt sau không được vượt quá 2000 ký tự")]
        public string MatSau { get; set; } = null!;

        [MaxLength(2000, ErrorMessage = "Giải thích không được vượt quá 2000 ký tự")]
        public string? GiaiThich { get; set; }

        [MaxLength(500, ErrorMessage = "Gợi ý không được vượt quá 500 ký tự")]
        public string? GoiY { get; set; }

        [MaxLength(100, ErrorMessage = "Viết tắt không được vượt quá 100 ký tự")]
        public string? VietTat { get; set; }

        public string? HinhAnhTruoc { get; set; }

        public string? HinhAnhSau { get; set; }

        public string? AmThanhTruoc { get; set; }

        public string? AmThanhSau { get; set; }

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }

        // Cho thẻ trắc nghiệm
        public List<TaoDapAnTracNghiemRequest>? DapAnTracNghiems { get; set; }

        // Cho thẻ ghép cặp
        public List<TaoCapGhepRequest>? CapGheps { get; set; }

        // Cho thẻ sắp xếp
        public List<TaoPhanTuSapXepRequest>? PhanTuSapXeps { get; set; }

        // Cho thẻ điền khuyết
        public List<TaoTuDienKhuyetRequest>? TuDienKhuyets { get; set; }
    }

    // Request cập nhật thẻ flashcard
    public class CapNhatTheFlashcardRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }

        public LoaiTheEnum? LoaiThe { get; set; }

        [MaxLength(2000, ErrorMessage = "Mặt trước không được vượt quá 2000 ký tự")]
        public string? MatTruoc { get; set; }

        [MaxLength(2000, ErrorMessage = "Mặt sau không được vượt quá 2000 ký tự")]
        public string? MatSau { get; set; }

        [MaxLength(2000, ErrorMessage = "Giải thích không được vượt quá 2000 ký tự")]
        public string? GiaiThich { get; set; }

        [MaxLength(500, ErrorMessage = "Gợi ý không được vượt quá 500 ký tự")]
        public string? GoiY { get; set; }

        [MaxLength(100, ErrorMessage = "Viết tắt không được vượt quá 100 ký tự")]
        public string? VietTat { get; set; }

        public string? HinhAnhTruoc { get; set; }

        public string? HinhAnhSau { get; set; }

        public string? AmThanhTruoc { get; set; }

        public string? AmThanhSau { get; set; }

        public int? ThuTu { get; set; }

        public MucDoKhoEnum? DoKho { get; set; }

        // Cập nhật đáp án, cặp ghép, phần tử sắp xếp
        public List<TaoDapAnTracNghiemRequest>? DapAnTracNghiems { get; set; }

        public List<TaoCapGhepRequest>? CapGheps { get; set; }

        public List<TaoPhanTuSapXepRequest>? PhanTuSapXeps { get; set; }

        public List<TaoTuDienKhuyetRequest>? TuDienKhuyets { get; set; }
    }

    // Request xóa thẻ flashcard
    public class XoaTheFlashcardRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }
    }

    // Request xóa nhiều thẻ
    public class XoaNhieuTheRequest
    {
        [Required(ErrorMessage = "Danh sách mã thẻ là bắt buộc")]
        [MinLength(1, ErrorMessage = "Phải chọn ít nhất 1 thẻ")]
        public List<int> MaThes { get; set; } = [];
    }

    // Request sắp xếp lại thứ tự thẻ
    public class SapXepLaiTheRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Danh sách thứ tự là bắt buộc")]
        public List<ThuTuTheItem> DanhSachThuTu { get; set; } = [];
    }

    public class ThuTuTheItem
    {
        public int MaThe { get; set; }
        public int ThuTu { get; set; }
    }

    // Request import thẻ từ text
    public class ImportTheRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Nội dung import là bắt buộc")]
        public string NoiDung { get; set; } = null!;

        public string KyTuNganCach { get; set; } = "\t"; // Tab mặc định

        public string KyTuXuongDong { get; set; } = "\n";

        public LoaiTheEnum LoaiTheMacDinh { get; set; } = LoaiTheEnum.CoBan;
    }
}