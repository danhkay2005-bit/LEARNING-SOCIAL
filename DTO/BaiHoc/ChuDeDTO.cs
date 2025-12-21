using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BaiHoc
{
    /// <summary>
    /// DTO cho bảng ChuDe
    /// Danh mục chủ đề (Tiếng Anh, Toán học, Lập trình...)
    /// </summary>
    public class ChuDeDTO
    {
        public int MaChuDe { get; set; }
        public string? TenChuDe { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public string? BieuTuong { get; set; }
        public string MauSac { get; set; } = "#3498db";
        public int SoLuotDung { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}
