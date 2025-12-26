using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests
{
    public class PhanThangRequest
    {
        public int Trang { get; set; } = 1;
        public int KichCoTrang { get; set; } = 10;
        public string? SapXepTheo { get; set; }
        public bool? TangDan { get; set; } = true;
    }
}
