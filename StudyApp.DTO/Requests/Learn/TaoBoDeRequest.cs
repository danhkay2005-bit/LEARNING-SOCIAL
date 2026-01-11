using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoBoDeRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        public int? MaChuDe { get; set; }
        public int? MaThuMuc { get; set; }

        // Hỗ trợ tính năng Clone/Fork bộ đề
        public int? MaBoDeGoc { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200)]
        public string TieuDe { get; set; } = string.Empty;

        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; } = MucDoKhoEnum.De;
        public bool LaCongKhai { get; set; } = true;
        public bool ChoPhepBinhLuan { get; set; } = true;

        public List<int> DanhSachMaTag { get; set; } = [];
    }

    
}