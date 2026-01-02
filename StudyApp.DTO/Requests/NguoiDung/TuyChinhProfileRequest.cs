using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO tùy chỉnh profile
public class CapNhatTuyChinhProfileRequest
{
    public int? MaAvatarDangDung { get; set; }
    public int? MaKhungDangDung { get; set; }
    public int? MaHinhNenDangDung { get; set; }
    public int? MaHieuUngDangDung { get; set; }
    public int? MaThemeDangDung { get; set; }
    public int? MaNhacNenDangDung { get; set; }
    public int? MaBadgeDangDung { get; set; }
    public int? MaHuyHieuHienThi { get; set; }

    [StringLength(300, ErrorMessage = "Câu châm ngôn tối đa 300 ký tự")]
    public string? CauChamNgon { get; set; }
}

// DTO trang bị vật phẩm cụ thể
public class TrangBiVatPhamRequest
{
    [Required(ErrorMessage = "Mã vật phẩm là bắt buộc")]
    public int MaVatPham { get; set; }

    [Required(ErrorMessage = "Loại trang bị là bắt buộc")]
    public string LoaiTrangBi { get; set; } = null!; // Avatar, Khung, HinhNen, HieuUng, Theme, NhacNen, Badge
}