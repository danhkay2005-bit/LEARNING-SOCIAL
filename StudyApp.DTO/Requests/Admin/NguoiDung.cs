using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests.Admin
{
    public class TaoNguoiDungAdminRequest
    {
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public GioiTinhEnum? GioiTinh { get; set; }
        public VaiTroEnum VaiTro { get; set; }
        public int? MaCapDo { get; set; }
        public int? Vang { get; set; }
        public int? KimCuong { get; set; }
        public int? DiemXP { get; set; }
    }

    public class CapNhatNguoiDungAdminRequest
    {
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public VaiTroEnum VaiTro { get; set; }
        public int? MaCapDo { get; set; }
        public int? Vang { get; set; }
        public int? KimCuong { get; set; }
        public int? DiemXP { get; set; }
        public bool? DaXoa { get; set; }
    }

    public class XoaNguoiDungAdminRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public string? LyDoXoa { get; set; }
    }

    public class LayDanhSachNguoiDungAdmin
    {
        public int? Trang { get; set; } = 1;
        public int? KichCoTrang { get; set; } = 10;
        public VaiTroEnum? LocTheoVaiTro { get; set; }
        public int? LocTheoCapDo { get; set; }
        public string? TuKhoaTimKiem { get; set; }
    }
}
