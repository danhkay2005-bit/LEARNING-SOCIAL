using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response mục tiêu cá nhân
    public class MucTieuCaNhanResponse
    {
        public int MaMucTieu { get; set; }
        public Guid MaNguoiDung { get; set; }
        public LoaiMucTieuEnum LoaiMucTieu { get; set; }
        public string? TenMucTieu { get; set; }
        public int GiaTriMucTieu { get; set; }
        public int GiaTriHienTai { get; set; }
        public string? DonVi { get; set; }
        public double TienDoPhanTram { get; set; }
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public int? SoNgayConLai { get; set; }
        public bool DaHoanThanh { get; set; }
        public DateOnly? NgayHoanThanh { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response danh sách mục tiêu
    public class DanhSachMucTieuResponse
    {
        public List<MucTieuCaNhanResponse> MucTieus { get; set; } = [];
        public int TongSo { get; set; }
        public int SoDangThucHien { get; set; }
        public int SoDaHoanThanh { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response mục tiêu hôm nay
    public class MucTieuHomNayResponse
    {
        public int SoTheCanHoc { get; set; }
        public int SoTheDaHoc { get; set; }
        public int SoPhutCanHoc { get; set; }
        public int SoPhutDaHoc { get; set; }
        public bool DaHoanThanhMucTieu { get; set; }
        public double TienDoPhanTram { get; set; }
        public List<MucTieuCaNhanResponse>? MucTieusDangThucHien { get; set; }
    }

    // Response kết quả mục tiêu
    public class KetQuaMucTieuResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public MucTieuCaNhanResponse? MucTieu { get; set; }
    }
}