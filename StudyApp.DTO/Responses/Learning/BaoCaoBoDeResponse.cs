using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response báo cáo bộ đề
    public class BaoCaoBoDeResponse
    {
        public int MaBaoCao { get; set; }
        public int MaBoDe { get; set; }
        public BoDeHocTomTatResponse? BoDe { get; set; }
        public Guid MaNguoiBaoCao { get; set; }
        public NguoiDungTomTatResponse? NguoiBaoCao { get; set; }
        public LyDoBaoCaoEnum LyDo { get; set; }
        public string? MoTaChiTiet { get; set; }
        public TrangThaiBaoCaoEnum TrangThai { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách báo cáo (Admin)
    public class DanhSachBaoCaoBoDeResponse
    {
        public List<BaoCaoBoDeResponse> BaoCaos { get; set; } = [];
        public int TongSo { get; set; }
        public int SoChoDuyet { get; set; }
        public int SoDaXuLy { get; set; }
        public int SoTuChoi { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả báo cáo
    public class KetQuaBaoCaoResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
    }
}