using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request ghi nhận lịch sử học bộ đề
    /// </summary>
    public class TaoLichSuHocBoDeRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        [Required]
        public int MaBoDe { get; set; }

        /// <summary>
        /// Phiên học liên quan (có thể null)
        /// </summary>
        public int? MaPhien { get; set; }

        /// <summary>
        /// Số thẻ đã học trong bộ đề
        /// </summary>
        public int? SoTheHoc { get; set; }

        /// <summary>
        /// Tỷ lệ trả lời đúng (0.0 – 1.0 hoặc 0 – 100 tùy quy ước BLL)
        /// </summary>
        public double? TyLeDung { get; set; }

        /// <summary>
        /// Tổng thời gian học (phút)
        /// </summary>
        public int? ThoiGianHocPhut { get; set; }
    }
}
