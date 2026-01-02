using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response bình luận chi tiết
    public class BinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiBinhLuan { get; set; }
        public string NoiDung { get; set; } = null!;
        public string? HinhAnh { get; set; }
        public int? MaStickerDung { get; set; }
        public StickerResponse? Sticker { get; set; }
        public int? MaBinhLuanCha { get; set; }
        public int SoLuotThich { get; set; }
        public int SoTraLoi { get; set; }
        public bool DaChinhSua { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianSua { get; set; }

        // Thông tin tương tác của người dùng hiện tại
        public bool DaThich { get; set; }

        // Danh sách mention
        public List<NguoiDungTomTatResponse>? MentionNguoiDungs { get; set; }

        // Danh sách trả lời (chỉ load khi cần)
        public List<BinhLuanResponse>? TraLois { get; set; }
    }

    // Response bình luận tóm tắt
    public class BinhLuanTomTatResponse
    {
        public int MaBinhLuan { get; set; }
        public NguoiDungTomTatResponse? NguoiBinhLuan { get; set; }
        public string NoiDungRutGon { get; set; } = null!;
        public int SoLuotThich { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response danh sách bình luận
    public class DanhSachBinhLuanResponse
    {
        public List<BinhLuanResponse> BinhLuans { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
        public bool CoTrangTruoc { get; set; }
    }

    // Response sau khi tạo bình luận
    public class TaoBinhLuanResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BinhLuanResponse? BinhLuan { get; set; }
    }
}