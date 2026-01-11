using System;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin danh mục sản phẩm
    /// </summary>
    public class DanhMucSanPhamResponse
    {
        public int MaDanhMuc { get; set; }

        public string TenDanhMuc { get; set; } = null!;
        public string? MoTa { get; set; }

        public int? ThuTuHienThi { get; set; }

        public DateTime? ThoiGianTao { get; set; }
    }
}
