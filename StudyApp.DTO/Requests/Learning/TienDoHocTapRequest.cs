using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request cập nhật tiến độ học tập (SRS)
    public class CapNhatTienDoRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }

        [Required(ErrorMessage = "Chất lượng trả lời là bắt buộc")]
        [Range(0, 5, ErrorMessage = "Chất lượng phải từ 0-5")]
        public int ChatLuongTraLoi { get; set; } // 0-5 theo thuật toán SM-2

        public int? ThoiGianTraLoiGiay { get; set; }
    }

    // Request reset tiến độ thẻ
    public class ResetTienDoTheRequest
    {
        [Required(ErrorMessage = "Mã thẻ là bắt buộc")]
        public int MaThe { get; set; }
    }

    // Request reset tiến độ bộ đề
    public class ResetTienDoBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }
    }

    // Request lấy thẻ cần ôn tập
    public class LayTheCanOnTapRequest
    {
        public int? MaBoDe { get; set; }

        public int SoLuongToiDa { get; set; } = 50;

        public bool BaoGomTheMoi { get; set; } = true;

        public int SoTheMoiToiDa { get; set; } = 20;
    }

    // Request lấy tiến độ học bộ đề
    public class LayTienDoBoDeRequest
    {
        [Required(ErrorMessage = "Mã bộ đề là bắt buộc")]
        public int MaBoDe { get; set; }
    }
}