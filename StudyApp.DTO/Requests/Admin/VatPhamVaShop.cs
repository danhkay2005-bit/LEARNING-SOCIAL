using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests.Admin
{
    public class TaoVatPhamAdminRequest
    {
        public string? TenVatPham { get; set; }
        public string? MoTa { get; set; }
        public int Gia { get; set; }
        public LoaiTienTeEnum LoaiTienTe { get; set; }
        public int? MaDanhMuc { get; set; }
        public int? DuongDanFile { get; set; }
        public int? ThoiHanPhut { get; set; }
        public int? GiaTriHieuUng { get; set; }
        public byte? DoHiem { get; set; }
        public bool ConHang { get; set; }
    }

    public class XoaVatPhamAdminRequest
    {
        public int MaVatPham { get; set; }
    }

    public class  TaoDanhMucAdminRequest
    {
        public string? TenDanhMuc {get; set; }
        public string? MoTa { get; set; }
        public string? DuongDanHinh { get; set; }
        public int? ThuTuHienThi { get; set; }
    }
}
