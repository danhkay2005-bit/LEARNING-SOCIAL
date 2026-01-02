using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response reaction bài đăng
    public class ReactionBaiDangChiTietResponse
    {
        public int MaBaiDang { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public LoaiReactionEnum LoaiReaction { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response reaction tóm tắt
    public class ReactionTomTatResponse
    {
        public LoaiReactionEnum LoaiReaction { get; set; }
        public int SoLuong { get; set; }
        public List<NguoiDungTomTatResponse>? NguoiReactionMau { get; set; }
    }

    // Response reaction tin nhắn
    public class ReactionTinNhanChiTietResponse
    {
        public int MaTinNhan { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiDung { get; set; }
        public string Emoji { get; set; } = null!;
        public DateTime? ThoiGian { get; set; }
    }

    // Response reaction tin nhắn tóm tắt
    public class ReactionTinNhanTomTatResponse
    {
        public string Emoji { get; set; } = null!;
        public int SoLuong { get; set; }
        public List<string>? TenNguoiReaction { get; set; }
    }

    // Response danh sách reaction bài đăng
    public class DanhSachReactionBaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public List<ReactionBaiDangChiTietResponse> Reactions { get; set; } = [];
        public int TongSo { get; set; }
        public Dictionary<LoaiReactionEnum, int>? ThongKeTheoLoai { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả reaction
    public class KetQuaReactionResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public int SoReactionMoi { get; set; }
    }
}