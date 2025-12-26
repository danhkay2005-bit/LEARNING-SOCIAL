using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests.Admin
{
    public class TaoThanhTuuAdminRequest
    {
        public string? TenThanhTuu { get; set; }
        public string? MoTa { get; set; }
        public string? DuongDanHinh { get; set; }
        public string? HinhHuy { get; set; }
        public LoaiThanhTuuEnum LoaiThanhTuu { get; set; }
        public LoaiDieuKienNhiemVuEnum DieuKien { get; set; }
        public int? GiaTri { get; set; }
        public int? DiemXP { get; set; }
        public int? ThuongVang { get; set; }
        public int? ThuongKiemCuong { get; set; }
        public byte? DoHiem { get; set; }
        public bool? BiAn   { get; set; }
    }

    public class TaoNhiemVuAdminRequest
    {
        public string? TenNhiemVu { get; set; }
        public string? MoTa { get; set; }
        public string? DuongDanHinh { get; set; }
        public LoaiNhiemVuEnum LoaiNhiemVu { get; set; }
        public LoaiDieuKienNhiemVuEnum DieuKien { get; set; }
        public int? DieuKienDatDuoc { get; set; }
        public int? ThuongVang { get; set; }
        public int? ThuongKimCuong { get; set; }
        public int? ThuongDiemXp { get; set; }
    }

    public class  TaoCapDoRequest
    {
        public string? TenCapDo { get; set; }
        public string? BieuTuong { get; set; }
        public int DiemXpToiThieu { get; set; }
        public int DiemXpToiDa { get; set; }
    }
}
