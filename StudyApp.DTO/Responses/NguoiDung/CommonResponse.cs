using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response cơ bản
public class BaseResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
}

// Response cơ bản với dữ liệu
public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
}

// Response phân trang
public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = [];
    public int TongSo { get; set; }
    public int TrangHienTai { get; set; }
    public int SoTrang { get; set; }
    public int SoMucMoiTrang { get; set; }
    public bool CoTrangTruoc => TrangHienTai > 1;
    public bool CoTrangSau => TrangHienTai < SoTrang;
}

// Response lỗi
public class ErrorResponse
{
    public bool ThanhCong { get; set; } = false;
    public string MaLoi { get; set; } = null!;
    public string ThongBao { get; set; } = null!;
    public List<string>? ChiTietLoi { get; set; }
    public DateTime ThoiGian { get; set; } = DateTime.Now;
}

// Response validation error
public class ValidationErrorResponse : ErrorResponse
{
    public Dictionary<string, List<string>> Errors { get; set; } = [];
}

// Response thống kê tổng quan (Admin Dashboard)
public class ThongKeTongQuanResponse
{
    public int TongNguoiDung { get; set; }
    public int NguoiDungMoi7Ngay { get; set; }
    public int NguoiDungMoi30Ngay { get; set; }
    public int NguoiDungOnline { get; set; }
    public int TongVatPham { get; set; }
    public int TongGiaoDichHomNay { get; set; }
    public long TongVangLuuThong { get; set; }
    public long TongKimCuongLuuThong { get; set; }
    public int TongNhiemVuDangHoatDong { get; set; }
    public int TongThanhTuu { get; set; }
}

// Response refresh token
public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}

// Response xác thực email
public class XacThucEmailResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
}

// Response quên mật khẩu
public class QuenMatKhauResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
}

// Response đặt lại mật khẩu
public class DatLaiMatKhauResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
}

// Response đổi mật khẩu
public class DoiMatKhauResponse
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = null!;
}