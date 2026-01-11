using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO
{
    public class ThongBaoDTO
    {
        public LoaiThongBao Loai { get; set; }
        public string NoiDung { get; set; } = string.Empty;
        public DateTime ThoiGian { get; set; }

        public int? MaBaiDang { get; set; }
        public int? MaBinhLuan { get; set; }
        public Guid? MaNguoiGui { get; set; }
    }
}
