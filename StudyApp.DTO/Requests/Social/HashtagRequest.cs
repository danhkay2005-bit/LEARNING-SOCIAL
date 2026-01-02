using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request tìm kiếm hashtag
    public class TimKiemHashtagRequest
    {
        [Required(ErrorMessage = "Từ khóa tìm kiếm là bắt buộc")]
        [MinLength(1, ErrorMessage = "Từ khóa phải có ít nhất 1 ký tự")]
        public string TuKhoa { get; set; } = null!;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    // Request lấy hashtag thịnh hành
    public class LayHashtagThinhHanhRequest
    {
        public int SoLuong { get; set; } = 10;
    }

    // Request lấy bài đăng theo hashtag
    public class LayBaiDangTheoHashtagRequest
    {
        [Required(ErrorMessage = "Tên hashtag là bắt buộc")]
        public string TenHashtag { get; set; } = null!;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}