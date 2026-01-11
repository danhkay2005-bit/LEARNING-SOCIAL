using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Responses
{
    public class NguoiDungInfo
    {
        public Guid MaNguoiDung { get; set; }
        public string HoTen { get; set; } = "";
        public string? Email { get; set; }
        public string? AnhDaiDien { get; set; }
    }
}
