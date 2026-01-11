using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoThuMucRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }
        [Required]
        public string TenThuMuc { get; set; } = string.Empty;

        public int? MaThuMucCha { get; set; }
        public string MoTa { get; set; } = string.Empty;
    }
}