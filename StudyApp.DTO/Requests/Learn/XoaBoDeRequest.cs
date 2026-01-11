using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class XoaBoDeRequest
    {
        [Required]
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; } // Kiểm tra quyền chủ sở hữu
    }
}