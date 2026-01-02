using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response chia sẻ bài đăng
    public class ChiaSeBaiDangResponse
    {
        public int MaChiaSe { get; set; }
        public int MaBaiDangGoc { get; set; }
        public BaiDangGocResponse? BaiDangGoc { get; set; }
        public Guid MaNguoiChiaSe { get; set; }
        public NguoiDungTomTatResponse? NguoiChiaSe { get; set; }
        public string? NoiDungThem { get; set; }
        public int? MaBaiDangMoi { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách chia sẻ
    public class DanhSachChiaSeResponse
    {
        public List<ChiaSeBaiDangResponse> ChiaSes { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả chia sẻ
    public class KetQuaChiaSeResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BaiDangResponse? BaiDangMoi { get; set; }
    }
}