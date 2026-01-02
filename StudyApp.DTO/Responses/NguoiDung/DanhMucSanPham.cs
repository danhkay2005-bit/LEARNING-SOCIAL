using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response danh mục sản phẩm
public class DanhMucSanPhamResponse
{
    public int MaDanhMuc { get; set; }
    public string TenDanhMuc { get; set; } = null!;
    public string? BieuTuong { get; set; }
    public string? MoTa { get; set; }
    public int? ThuTuHienThi { get; set; }
    public DateTime? ThoiGianTao { get; set; }
    public int SoVatPham { get; set; }
}