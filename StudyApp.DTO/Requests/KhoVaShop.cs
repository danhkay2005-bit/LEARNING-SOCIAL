using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests
{
    public class MuaVatPhamRequest
    {
        public string? MaVatPham { get; set; }
        public int? SoLuong { get; set; } 
    }

    public class TrangBiTuKhoRequest
    {
        public string? MaKho { get; set; }
    }

    public class LayKhoVatPhamRequest {
    public Guid? MaNguoiDung { get; set; }
    }

}
