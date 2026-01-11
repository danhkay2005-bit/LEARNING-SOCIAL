using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request mua vật phẩm (thêm vào kho người dùng)
    /// </summary>
    public class MuaVatPhamRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
        public int SoLuong { get; set; } = 1;
    }

    /// <summary>
    /// Request sử dụng vật phẩm trong kho
    /// </summary>
    public class SuDungVatPhamRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
    }

    /// <summary>
    /// Request trang bị / gỡ trang bị vật phẩm
    /// </summary>
    public class TrangBiVatPhamRequest
    {
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
        public bool DaTrangBi { get; set; }
    }
}
