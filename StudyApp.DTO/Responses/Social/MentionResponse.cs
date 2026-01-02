using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response mention trong bài đăng
    public class MentionBaiDangResponse
    {
        public int MaBaiDang { get; set; }
        public BaiDangTomTatResponse? BaiDang { get; set; }
        public Guid MaNguoiMention { get; set; }
        public NguoiDungTomTatResponse? NguoiMention { get; set; }
        public DateTime? ThoiGian { get; set; }
        public bool DaDoc { get; set; }
    }

    // Response mention trong bình luận
    public class MentionBinhLuanResponse
    {
        public int MaBinhLuan { get; set; }
        public BinhLuanTomTatResponse? BinhLuan { get; set; }
        public int MaBaiDang { get; set; }
        public Guid MaNguoiMention { get; set; }
        public NguoiDungTomTatResponse? NguoiMention { get; set; }
        public DateTime? ThoiGian { get; set; }
        public bool DaDoc { get; set; }
    }

    // Response danh sách mention
    public class DanhSachMentionResponse
    {
        public List<MentionBaiDangResponse> MentionBaiDangs { get; set; } = [];
        public List<MentionBinhLuanResponse> MentionBinhLuans { get; set; } = [];
        public int TongSo { get; set; }
        public int SoChuaDoc { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }
}