using StudyApp.DTO.Enums;
using System;

namespace StudyApp.DTO.Responses.NguoiDung;

// Response lịch sử giao dịch
public class LichSuGiaoDichResponse
{
    public int MaGiaoDich { get; set; }
    public LoaiGiaoDichEnum LoaiGiaoDich { get; set; }
    public string TenLoaiGiaoDich => LoaiGiaoDich switch
    {
        LoaiGiaoDichEnum.MuaVatPham => "Mua vật phẩm",
        LoaiGiaoDichEnum.NhanThuong => "Nhận thưởng",
        LoaiGiaoDichEnum.HoanThanhNhiemVu => "Hoàn thành nhiệm vụ",
        LoaiGiaoDichEnum.DiemDanh => "Điểm danh",
        LoaiGiaoDichEnum.ThachDau => "Thách đấu",
        LoaiGiaoDichEnum.SuDungBoost => "Sử dụng boost",
        _ => "Khác"
    };
    public LoaiTienTeGiaoDichEnum LoaiTien { get; set; }
    public string TenLoaiTien => LoaiTien switch
    {
        LoaiTienTeGiaoDichEnum.Vang => "Vàng",
        LoaiTienTeGiaoDichEnum.KimCuong => "Kim cương",
        _ => "Không xác định"
    };
    public int SoLuong { get; set; }
    public bool LaChi => SoLuong < 0 || SoDuSau < SoDuTruoc;
    public int SoDuTruoc { get; set; }
    public int SoDuSau { get; set; }
    public int ThayDoi => SoDuSau - SoDuTruoc;
    public string? MoTa { get; set; }
    public VatPhamTomTatResponse? VatPham { get; set; }
    public DateTime? ThoiGian { get; set; }
}

// Response danh sách giao dịch
public class DanhSachGiaoDichResponse
{
    public List<LichSuGiaoDichResponse> GiaoDichs { get; set; } = [];
    public int TongGiaoDich { get; set; }
    public int TongTrang { get; set; }
    public int TrangHienTai { get; set; }
}

// Response thống kê giao dịch
public class ThongKeGiaoDichResponse
{
    public int TongVangNhan { get; set; }
    public int TongVangChi { get; set; }
    public int ChenhLechVang => TongVangNhan - TongVangChi;
    public int TongKimCuongNhan { get; set; }
    public int TongKimCuongChi { get; set; }
    public int ChenhLechKimCuong => TongKimCuongNhan - TongKimCuongChi;
    public int SoGiaoDich { get; set; }
    public Dictionary<string, int> ThongKeTheoLoai { get; set; } = [];
}