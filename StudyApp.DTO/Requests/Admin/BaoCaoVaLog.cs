using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests.Admin
{
    public class LayBaoCaoAdminRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public LoaiGiaoDichEnum? LoaiGiaoDich { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? Trang { get; set; } = 1;
        public int? KichCoTrang { get; set; } = 10;
    }

    public class LayLogHeThongAdminRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public LoaiHoatDongEnum? LoaiHoatDong { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? Trang { get; set; } = 1;
        public int? KichCoTrang { get; set; } = 10;
    }
}
