using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response thông tin bạn bè
    public class BanBeResponse
    {
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public TrangThaiBanBeEnum TrangThai { get; set; }
        public bool LaNguoiGui { get; set; }
        public DateTime? ThoiGianGui { get; set; }
        public DateTime? ThoiGianChapNhan { get; set; }
        public int SoBanChung { get; set; }
    }

    // Response danh sách bạn bè
    public class DanhSachBanBeResponse
    {
        public List<BanBeResponse> BanBes { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
        public bool CoTrangTruoc { get; set; }
    }

    // Response lời mời kết bạn
    public class LoiMoiKetBanResponse
    {
        public Guid MaNguoiGui { get; set; }
        public NguoiDungTomTatResponse? NguoiGui { get; set; }
        public DateTime? ThoiGianGui { get; set; }
        public int SoBanChung { get; set; }
        public List<NguoiDungTomTatResponse>? BanChung { get; set; }
    }

    // Response danh sách lời mời kết bạn
    public class DanhSachLoiMoiKetBanResponse
    {
        public List<LoiMoiKetBanResponse> LoiMois { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response gợi ý kết bạn
    public class GoiYKetBanResponse
    {
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public int SoBanChung { get; set; }
        public List<NguoiDungTomTatResponse>? BanChung { get; set; }
        public string? LyDoGoiY { get; set; }
    }

    // Response kết quả thao tác bạn bè
    public class KetQuaBanBeResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public TrangThaiBanBeEnum? TrangThaiMoi { get; set; }
    }
}