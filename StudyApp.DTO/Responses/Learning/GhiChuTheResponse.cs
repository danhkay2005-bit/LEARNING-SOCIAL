using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response ghi chú thẻ
    public class GhiChuTheResponse
    {
        public int MaGhiChu { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaThe { get; set; }
        public string? NoiDung { get; set; }
        public string? MauNen { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianSua { get; set; }
    }

    // Response danh sách ghi chú
    public class DanhSachGhiChuTheResponse
    {
        public List<GhiChuTheResponse> GhiChus { get; set; } = [];
        public int TongSo { get; set; }
    }

    // Response kết quả ghi chú
    public class KetQuaGhiChuTheResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public GhiChuTheResponse? GhiChu { get; set; }
    }
}