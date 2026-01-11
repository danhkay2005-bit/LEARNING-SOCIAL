using System;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request tạo danh mục sản phẩm
    /// </summary>
    public class TaoDanhMucSanPhamRequest
    {
        public string TenDanhMuc { get; set; } = null!;
        public string? MoTa { get; set; }
        public int? ThuTuHienThi { get; set; }
    }

    /// <summary>
    /// Request cập nhật danh mục sản phẩm
    /// </summary>
    public class CapNhatDanhMucSanPhamRequest
    {
        public string TenDanhMuc { get; set; } = null!;
        public string? MoTa { get; set; }
        public int? ThuTuHienThi { get; set; }
    }
}
