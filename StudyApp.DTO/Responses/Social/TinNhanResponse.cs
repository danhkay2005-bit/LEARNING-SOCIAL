using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response tin nhắn chi tiết
    public class TinNhanResponse
    {
        public int MaTinNhan { get; set; }
        public int MaCuocTroChuyen { get; set; }
        public Guid MaNguoiGui { get; set; }
        public NguoiDungTomTatResponse? NguoiGui { get; set; }
        public string? NoiDung { get; set; }
        public LoaiTinNhanEnum LoaiTinNhan { get; set; }
        public string? DuongDanFile { get; set; }
        public string? TenFile { get; set; }
        public int? KichThuocFile { get; set; }
        public string? KichThuocFileHienThi { get; set; }
        public int? MaStickerDung { get; set; }
        public StickerResponse? Sticker { get; set; }
        public int? MaBoDeDinhKem { get; set; }
        public BoDeToTamResponse? BoDeDinhKem { get; set; }
        public int? MaThachDauDinhKem { get; set; }
        public ThachDauTomTatResponse? ThachDauDinhKem { get; set; }
        public int? ReplyToId { get; set; }
        public TinNhanReplyResponse? ReplyTo { get; set; }
        public bool DaThuHoi { get; set; }
        public DateTime? ThoiGianThuHoi { get; set; }
        public DateTime? ThoiGianGui { get; set; }

        // Thông tin đã xem
        public bool DaXem { get; set; }
        public int SoNguoiDaXem { get; set; }
        public List<NguoiXemTomTatResponse>? NguoiDaXem { get; set; }

        // Reactions
        public List<ReactionTinNhanTomTatResponse>? Reactions { get; set; }
        public int TongReaction { get; set; }

        // Trạng thái của người dùng hiện tại
        public bool LaTinCuaToi { get; set; }
        public string? ReactionCuaToi { get; set; }
    }

    // Response tin nhắn reply
    public class TinNhanReplyResponse
    {
        public int MaTinNhan { get; set; }
        public NguoiDungTomTatResponse? NguoiGui { get; set; }
        public string? NoiDungRutGon { get; set; }
        public LoaiTinNhanEnum LoaiTinNhan { get; set; }
        public bool DaThuHoi { get; set; }
    }

    // Response tin nhắn tóm tắt
    public class TinNhanTomTatResponse
    {
        public int MaTinNhan { get; set; }
        public Guid MaNguoiGui { get; set; }
        public string? TenNguoiGui { get; set; }
        public string? NoiDungRutGon { get; set; }
        public LoaiTinNhanEnum LoaiTinNhan { get; set; }
        public DateTime? ThoiGianGui { get; set; }
        public bool DaThuHoi { get; set; }
    }

    // Response danh sách tin nhắn
    public class DanhSachTinNhanResponse
    {
        public List<TinNhanResponse> TinNhans { get; set; } = [];
        public int TongSo { get; set; }
        public bool ConTinTruoc { get; set; }
        public bool ConTinSau { get; set; }
        public int? TinNhanDauTienId { get; set; }
        public int? TinNhanCuoiCungId { get; set; }
    }

    // Response sau khi gửi tin nhắn
    public class GuiTinNhanResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public TinNhanResponse? TinNhan { get; set; }
    }

    // Response kết quả tìm kiếm tin nhắn
    public class KetQuaTimKiemTinNhanResponse
    {
        public List<TinNhanResponse> TinNhans { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }
}