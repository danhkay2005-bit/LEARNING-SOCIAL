using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response thông tin theo dõi
    public class TheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public DateTime? ThoiGian { get; set; }
        public bool ThongBaoBaiMoi { get; set; }
        public bool DangTheoDoi { get; set; }
        public bool DuocTheoDoi { get; set; }
    }

    // Response danh sách theo dõi
    public class DanhSachTheoDoiResponse
    {
        public List<TheoDoiResponse> NguoiDungs { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
    }

    // Response thống kê theo dõi
    public class ThongKeTheoDoiResponse
    {
        public Guid MaNguoiDung { get; set; }
        public int SoNguoiTheoDoi { get; set; }
        public int SoNguoiDangTheoDoi { get; set; }
    }

    // Response kết quả theo dõi
    public class KetQuaTheoDoiResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public bool DangTheoDoi { get; set; }
    }
}