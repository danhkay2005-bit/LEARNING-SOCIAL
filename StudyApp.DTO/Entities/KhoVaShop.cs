using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Entities
{
    public class GiaoDichDto
    {
        public int MaGiaoDich { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
        public int SoLuong { get; set; }
        public int TongGia { get; set; }
        public string LoaiTienTe { get; set; }
        public DateTime ThoiGian { get; set; }
    }

    /// <summary>
    /// DTO cho bảng KhoNguoiDung
    /// </summary>
    public class KhoDto
    {
        public int MaKho { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
        public int SoLuong { get; set; }
        public bool DaTrangBi { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
        public DateTime ThoiGianMua { get; set; }
    }
}
