using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO lọc lịch sử giao dịch
public class LocLichSuGiaoDichRequest
{
    public LoaiGiaoDichEnum LoaiGiaoDich { get; set; } // Mua, Bán, Thưởng, etc.
    public LoaiTienTeEnum LoaiTien { get; set; } // Vang, KimCuong
    public DateOnly? TuNgay { get; set; }
    public DateOnly? DenNgay { get; set; }

    [Range(1, 100, ErrorMessage = "Số lượng mỗi trang từ 1-100")]
    public int PageSize { get; set; } = 20;

    [Range(1, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn 0")]
    public int PageNumber { get; set; } = 1;
}