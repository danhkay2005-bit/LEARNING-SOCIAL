using StudyApp.DTO.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request báo cáo bộ đề
    public class BaoCaoBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }

        [Required(ErrorMessage = "Lý do báo cáo là bắt buộc")]
        public LyDoBaoCaoEnum LyDo { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả chi tiết không được vượt quá 1000 ký tự")]
        public string? MoTaChiTiet { get; set; }
    }

    // Request xử lý báo cáo (Admin)
    public class XuLyBaoCaoRequest
    {
        [Required(ErrorMessage = "Mã báo cáo là bắt buộc")]
        public int MaBaoCao { get; set; }

        [Required(ErrorMessage = "Trạng thái xử lý là bắt buộc")]
        public TrangThaiBaoCaoEnum TrangThai { get; set; }

        [MaxLength(1000, ErrorMessage = "Ghi chú không được vượt quá 1000 ký tự")]
        public string? GhiChu { get; set; }
    }
}