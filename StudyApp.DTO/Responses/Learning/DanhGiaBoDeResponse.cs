using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response đánh giá bộ đề chi tiết
    public class DanhGiaBoDeResponse
    {
        public int MaDanhGia { get; set; }
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDanhGia { get; set; }
        public byte SoSao { get; set; }
        public string? NoiDung { get; set; }
        public MucDoKhoEnum? DoKhoThucTe { get; set; }
        public byte? ChatLuongNoiDung { get; set; }
        public int SoLuotThich { get; set; }
        public DateTime? ThoiGian { get; set; }
        public bool DaThich { get; set; }
        public bool LaCuaToi { get; set; }
    }

    // Response danh sách đánh giá
    public class DanhSachDanhGiaBoDeResponse
    {
        public List<DanhGiaBoDeResponse> DanhGias { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public ThongKeDanhGiaResponse? ThongKe { get; set; }
    }

    // Response thống kê đánh giá
    public class ThongKeDanhGiaResponse
    {
        public double DiemTrungBinh { get; set; }
        public int TongSoDanhGia { get; set; }
        public int So5Sao { get; set; }
        public int So4Sao { get; set; }
        public int So3Sao { get; set; }
        public int So2Sao { get; set; }
        public int So1Sao { get; set; }
        public double PhanTram5Sao { get; set; }
        public double PhanTram4Sao { get; set; }
        public double PhanTram3Sao { get; set; }
        public double PhanTram2Sao { get; set; }
        public double PhanTram1Sao { get; set; }
    }

    // Response kết quả đánh giá
    public class KetQuaDanhGiaResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public DanhGiaBoDeResponse? DanhGia { get; set; }
    }
}