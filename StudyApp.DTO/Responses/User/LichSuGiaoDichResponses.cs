using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin lịch sử giao dịch
    /// </summary>
    public class LichSuGiaoDichResponse
    {
        public int MaGiaoDich { get; set; }

        public Guid MaNguoiDung { get; set; }

        public LoaiGiaoDichEnum LoaiGiaoDich { get; set; }

        public LoaiTienGiaoDichEnum LoaiTien { get; set; }

        public int SoLuong { get; set; }

        public int SoDuTruoc { get; set; }
        public int SoDuSau { get; set; }

        public string? MoTa { get; set; }

        public int? MaVatPham { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
