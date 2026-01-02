using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO cập nhật cài đặt người dùng
public class CapNhatCaiDatNguoiDungRequest
{
    public bool? CheDoToi { get; set; }
    public bool? CoHieuUng { get; set; }
    public bool? ThongBaoNhacHoc { get; set; }
    public TimeOnly? GioNhacHoc { get; set; }
    public bool? ThongBaoThanhTuu { get; set; }
    public bool? ThongBaoXaHoi { get; set; }
    public bool? ThongBaoThachDau { get; set; }
    public bool? HienThiTrangThai { get; set; }
    public bool? HienThiThongKe { get; set; }
    public bool? ChoPhepThachDau { get; set; }
    public bool? ChoPhepNhanTin { get; set; }
    public bool? ChoPhepTagTrongBaiDang { get; set; }
}