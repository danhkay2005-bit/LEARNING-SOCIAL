using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO kích hoạt boost
public class KichHoatBoostRequest
{
    [Required(ErrorMessage = "Mã vật phẩm là bắt buộc")]
    public int MaVatPham { get; set; }
}

// DTO hủy boost (nếu cần)
public class HuyBoostRequest
{
    [Required(ErrorMessage = "Mã boost là bắt buộc")]
    public int MaBoost { get; set; }
}