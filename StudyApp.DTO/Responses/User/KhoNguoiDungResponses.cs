using System;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin vật phẩm trong kho người dùng
    /// </summary>
    public class KhoNguoiDungResponse
    {
        public int MaKho { get; set; }

        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }

        public int SoLuong { get; set; }
        public bool DaTrangBi { get; set; }

        public DateTime? ThoiGianMua { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
    
        public string? TenVatPham { get; set; }
    }
}
