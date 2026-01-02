using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request chia sẻ bài đăng
    public class ChiaSeBaiDangRequest
    {
        [Required(ErrorMessage = "Mã bài đăng gốc là bắt buộc")]
        public int MaBaiDangGoc { get; set; }

        [MaxLength(2000, ErrorMessage = "Nội dung thêm không được vượt quá 2000 ký tự")]
        public string? NoiDungThem { get; set; }

        public byte QuyenRiengTu { get; set; } = 1; // Mặc định công khai
    }

    // Request hủy chia sẻ
    public class HuyChiaSeBaiDangRequest
    {
        [Required(ErrorMessage = "Mã chia sẻ là bắt buộc")]
        public int MaChiaSe { get; set; }
    }
}