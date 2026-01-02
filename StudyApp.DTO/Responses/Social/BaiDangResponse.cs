using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response thông tin bài đăng
    public class BaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDang { get; set; }
        public string? NoiDung { get; set; }
        public LoaiBaiDangEnum LoaiBaiDang { get; set; }
        public string? HinhAnh { get; set; }
        public string? Video { get; set; }
        public int? MaBoDeLienKet { get; set; }
        public BoDeToTamResponse? BoDeLienKet { get; set; }
        public int? MaThanhTuuLienKet { get; set; }
        public ThanhTuuTomTatResponse? ThanhTuuLienKet { get; set; }
        public int? MaThachDauLienKet { get; set; }
        public ThachDauTomTatResponse? ThachDauLienKet { get; set; }
        public int? SoChuoiNgay { get; set; }
        public QuyenRiengTuEnum QuyenRiengTu { get; set; }
        public int SoReaction { get; set; }
        public int SoBinhLuan { get; set; }
        public int SoLuotXem { get; set; }
        public int SoLuotChiaSe { get; set; }
        public bool GhimBaiDang { get; set; }
        public bool TatBinhLuan { get; set; }
        public bool DaChinhSua { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianSua { get; set; }

        // Thông tin tương tác của người dùng hiện tại
        public bool DaReaction { get; set; }
        public LoaiReactionEnum? LoaiReactionCuaToi { get; set; }
        public bool DaLuu { get; set; }

        // Danh sách liên quan
        public List<HashtagResponse>? Hashtags { get; set; }
        public List<NguoiDungTomTatResponse>? MentionNguoiDungs { get; set; }
        public List<ReactionTomTatResponse>? TopReactions { get; set; }

        // Thông tin chia sẻ (nếu là bài đăng được chia sẻ)
        public BaiDangGocResponse? BaiDangGoc { get; set; }
    }

    // Response bài đăng gốc (khi chia sẻ)
    public class BaiDangGocResponse
    {
        public int MaBaiDang { get; set; }
        public NguoiDungTomTatResponse? NguoiDang { get; set; }
        public string? NoiDung { get; set; }
        public LoaiBaiDangEnum LoaiBaiDang { get; set; }
        public string? HinhAnh { get; set; }
        public string? Video { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public bool DaXoa { get; set; }
    }

    // Response bài đăng tóm tắt (cho danh sách)
    public class BaiDangTomTatResponse
    {
        public int MaBaiDang { get; set; }
        public NguoiDungTomTatResponse? NguoiDang { get; set; }
        public string? NoiDungRutGon { get; set; }
        public LoaiBaiDangEnum LoaiBaiDang { get; set; }
        public string? HinhAnhDauTien { get; set; }
        public int SoReaction { get; set; }
        public int SoBinhLuan { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    // Response danh sách bài đăng có phân trang
    public class DanhSachBaiDangResponse
    {
        public List<BaiDangResponse> BaiDangs { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
        public bool CoTrangTruoc { get; set; }
    }

    // Response sau khi tạo/cập nhật bài đăng
    public class TaoBaiDangResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BaiDangResponse? BaiDang { get; set; }
    }
}