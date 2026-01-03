using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers;

public class SocialMappingProfile : Profile
{
    public SocialMappingProfile()
    {
        #region BaiDang
        // Request -> Entity
        CreateMap<TaoBaiDangRequest, BaiDang>()
            .ForMember(d => d.MaBaiDang, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiBaiDang, o => o.MapFrom(s => s.LoaiBaiDang.ToString()))
            .ForMember(d => d.QuyenRiengTu, o => o.MapFrom(s => (byte)s.QuyenRiengTu))
            .ForMember(d => d.SoReaction, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoBinhLuan, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLuotXem, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLuotChiaSe, o => o.MapFrom(_ => 0))
            .ForMember(d => d.GhimBaiDang, o => o.MapFrom(_ => false))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
            .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CapNhatBaiDangRequest, BaiDang>()
            .ForMember(d => d.MaBaiDang, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiBaiDang, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
            .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.QuyenRiengTu, o => o.MapFrom((s, d) => s.QuyenRiengTu.HasValue ? (byte)s.QuyenRiengTu.Value : d.QuyenRiengTu))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        // Entity -> Response
        CreateMap<BaiDang, BaiDangResponse>()
            .ForMember(d => d.LoaiBaiDang, o => o.MapFrom(s => ParseEnum<LoaiBaiDangEnum>(s.LoaiBaiDang)))
            .ForMember(d => d.QuyenRiengTu, o => o.MapFrom(s => (QuyenRiengTuEnum)(s.QuyenRiengTu ?? 1)))
            .ForMember(d => d.SoReaction, o => o.MapFrom(s => s.SoReaction ?? 0))
            .ForMember(d => d.SoBinhLuan, o => o.MapFrom(s => s.SoBinhLuan ?? 0))
            .ForMember(d => d.SoLuotXem, o => o.MapFrom(s => s.SoLuotXem ?? 0))
            .ForMember(d => d.SoLuotChiaSe, o => o.MapFrom(s => s.SoLuotChiaSe ?? 0))
            .ForMember(d => d.GhimBaiDang, o => o.MapFrom(s => s.GhimBaiDang ?? false))
            .ForMember(d => d.TatBinhLuan, o => o.MapFrom(s => s.TatBinhLuan ?? false))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))
            .ForMember(d => d.Hashtags, o => o.MapFrom(s => s.MaHashtags))
            .ForMember(d => d.NguoiDang, o => o.Ignore())
            .ForMember(d => d.DaReaction, o => o.Ignore())
            .ForMember(d => d.LoaiReactionCuaToi, o => o.Ignore())
            .ForMember(d => d.DaLuu, o => o.Ignore())
            .ForMember(d => d.MentionNguoiDungs, o => o.Ignore())
            .ForMember(d => d.TopReactions, o => o.Ignore())
            .ForMember(d => d.BaiDangGoc, o => o.Ignore())
            .ForMember(d => d.BoDeLienKet, o => o.Ignore())
            .ForMember(d => d.ThanhTuuLienKet, o => o.Ignore())
            .ForMember(d => d.ThachDauLienKet, o => o.Ignore());

        CreateMap<BaiDang, BaiDangTomTatResponse>()
            .ForMember(d => d.LoaiBaiDang, o => o.MapFrom(s => ParseEnum<LoaiBaiDangEnum>(s.LoaiBaiDang)))
            .ForMember(d => d.NoiDungRutGon, o => o.MapFrom(s => Truncate(s.NoiDung, 200)))
            .ForMember(d => d.HinhAnhDauTien, o => o.MapFrom(s => GetFirstImage(s.HinhAnh)))
            .ForMember(d => d.SoReaction, o => o.MapFrom(s => s.SoReaction ?? 0))
            .ForMember(d => d.SoBinhLuan, o => o.MapFrom(s => s.SoBinhLuan ?? 0))
            .ForMember(d => d.NguoiDang, o => o.Ignore());

        CreateMap<BaiDang, BaiDangGocResponse>()
            .ForMember(d => d.LoaiBaiDang, o => o.MapFrom(s => ParseEnum<LoaiBaiDangEnum>(s.LoaiBaiDang)))
            .ForMember(d => d.DaXoa, o => o.MapFrom(s => s.DaXoa ?? false))
            .ForMember(d => d.NguoiDang, o => o.Ignore());
        #endregion

        #region BinhLuanBaiDang
        CreateMap<TaoBinhLuanRequest, BinhLuanBaiDang>()
            .ForMember(d => d.MaBinhLuan, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
            .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CapNhatBinhLuanRequest, BinhLuanBaiDang>()
            .ForMember(d => d.MaBinhLuan, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.MaBaiDang, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
            .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<BinhLuanBaiDang, BinhLuanResponse>()
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
            .ForMember(d => d.SoTraLoi, o => o.MapFrom(s => s.InverseMaBinhLuanChaNavigation.Count))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))
            .ForMember(d => d.NguoiBinhLuan, o => o.Ignore())
            .ForMember(d => d.Sticker, o => o.Ignore())
            .ForMember(d => d.DaThich, o => o.Ignore())
            .ForMember(d => d.MentionNguoiDungs, o => o.Ignore())
            .ForMember(d => d.TraLois, o => o.Ignore());

        CreateMap<BinhLuanBaiDang, BinhLuanTomTatResponse>()
            .ForMember(d => d.NoiDungRutGon, o => o.MapFrom(s => Truncate(s.NoiDung, 100)))
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
            .ForMember(d => d.NguoiBinhLuan, o => o.Ignore());
        #endregion

        #region BanBe
        CreateMap<BanBe, BanBeResponse>()
            .ForMember(d => d.TrangThai, o => o.MapFrom(s => ParseEnum<TrangThaiBanBeEnum>(s.TrangThai)))
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.NguoiDung, o => o.Ignore())
            .ForMember(d => d.LaNguoiGui, o => o.Ignore())
            .ForMember(d => d.SoBanChung, o => o.Ignore());

        CreateMap<BanBe, LoiMoiKetBanResponse>()
            .ForMember(d => d.NguoiGui, o => o.Ignore())
            .ForMember(d => d.SoBanChung, o => o.Ignore())
            .ForMember(d => d.BanChung, o => o.Ignore());
        #endregion

        #region TheoDoi
        CreateMap<TheoDoiRequest, TheoDoi>()
            .ForMember(d => d.MaNguoiTheoDoi, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<TheoDoi, TheoDoiResponse>()
            .ForMember(d => d.ThongBaoBaiMoi, o => o.MapFrom(s => s.ThongBaoBaiMoi ?? true))
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.NguoiDung, o => o.Ignore())
            .ForMember(d => d.DangTheoDoi, o => o.Ignore())
            .ForMember(d => d.DuocTheoDoi, o => o.Ignore());
        #endregion

        #region ChanNguoiDung
        CreateMap<ChanNguoiDungRequest, ChanNguoiDung>()
            .ForMember(d => d.MaNguoiChan, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ChanNguoiDung, NguoiBiChanResponse>()
            .ForMember(d => d.NguoiBiChan, o => o.Ignore());
        #endregion

        #region ChiaSeBaiDang
        CreateMap<ChiaSeBaiDangRequest, ChiaSeBaiDang>()
            .ForMember(d => d.MaChiaSe, o => o.Ignore())
            .ForMember(d => d.MaNguoiChiaSe, o => o.Ignore())
            .ForMember(d => d.MaBaiDangMoi, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ChiaSeBaiDang, ChiaSeBaiDangResponse>()
            .ForMember(d => d.BaiDangGoc, o => o.Ignore())
            .ForMember(d => d.NguoiChiaSe, o => o.Ignore());
        #endregion

        #region CuocTroChuyen
        CreateMap<TaoCuocTroChuyenNhomRequest, CuocTroChuyen>()
            .ForMember(d => d.MaCuocTroChuyen, o => o.Ignore())
            .ForMember(d => d.LoaiCuocTroChuyen, o => o.MapFrom(_ => LoaiCuocTroChuyenEnum.Nhom.ToString()))
            .ForMember(d => d.MaNguoiTaoNhom, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CapNhatNhomChatRequest, CuocTroChuyen>()
            .ForMember(d => d.MaCuocTroChuyen, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<CuocTroChuyen, CuocTroChuyenResponse>()
            .ForMember(d => d.LoaiCuocTroChuyen, o => o.MapFrom(s => ParseEnum<LoaiCuocTroChuyenEnum>(s.LoaiCuocTroChuyen)))
            .ForMember(d => d.SoThanhVien, o => o.MapFrom(s => s.ThanhVienCuocTroChuyens.Count(t => t.DaRoiNhom != true)))
            .ForMember(d => d.NguoiTaoNhom, o => o.Ignore())
            .ForMember(d => d.TinNhanCuoi, o => o.Ignore())
            .ForMember(d => d.ThanhViens, o => o.Ignore())
            .ForMember(d => d.NguoiNhan, o => o.Ignore())
            .ForMember(d => d.SoTinChuaDoc, o => o.Ignore())
            .ForMember(d => d.TatThongBao, o => o.Ignore())
            .ForMember(d => d.GhimCuocTro, o => o.Ignore())
            .ForMember(d => d.BiDanhCuaToi, o => o.Ignore())
            .ForMember(d => d.VaiTroCuaToi, o => o.Ignore())
            .ForMember(d => d.NguoiNhanOnline, o => o.Ignore())
            .ForMember(d => d.ThoiGianOnlineCuoi, o => o.Ignore());

        CreateMap<CuocTroChuyen, CuocTroChuyenTomTatResponse>()
            .ForMember(d => d.LoaiCuocTroChuyen, o => o.MapFrom(s => ParseEnum<LoaiCuocTroChuyenEnum>(s.LoaiCuocTroChuyen)))
            .ForMember(d => d.TenHienThi, o => o.MapFrom(s => s.TenNhomChat))
            .ForMember(d => d.AnhHienThi, o => o.MapFrom(s => s.AnhNhomChat))
            .ForMember(d => d.SoTinChuaDoc, o => o.Ignore())
            .ForMember(d => d.TatThongBao, o => o.Ignore())
            .ForMember(d => d.GhimCuocTro, o => o.Ignore())
            .ForMember(d => d.Online, o => o.Ignore());
        #endregion

        #region ThanhVienCuocTroChuyen
        CreateMap<ThanhVienCuocTroChuyen, ThanhVienCuocTroChuyenResponse>()
            .ForMember(d => d.VaiTro, o => o.MapFrom(s => ParseEnum<VaiTroThanhVienChatEnum>(s.VaiTro)))
            .ForMember(d => d.TatThongBao, o => o.MapFrom(s => s.TatThongBao ?? false))
            .ForMember(d => d.DaRoiNhom, o => o.MapFrom(s => s.DaRoiNhom ?? false))
            .ForMember(d => d.NguoiDung, o => o.Ignore())
            .ForMember(d => d.DangOnline, o => o.Ignore());

        CreateMap<ThanhVienCuocTroChuyen, ThanhVienTomTatResponse>()
            .ForMember(d => d.VaiTro, o => o.MapFrom(s => ParseEnum<VaiTroThanhVienChatEnum>(s.VaiTro)))
            .ForMember(d => d.TenHienThi, o => o.Ignore())
            .ForMember(d => d.AnhDaiDien, o => o.Ignore())
            .ForMember(d => d.DangOnline, o => o.Ignore());
        #endregion

        #region TinNhan
        CreateMap<GuiTinNhanRequest, TinNhan>()
            .ForMember(d => d.MaTinNhan, o => o.Ignore())
            .ForMember(d => d.MaNguoiGui, o => o.Ignore())
            .ForMember(d => d.LoaiTinNhan, o => o.MapFrom(s => s.LoaiTinNhan.ToString()))
            .ForMember(d => d.DaThuHoi, o => o.MapFrom(_ => false))
            .ForMember(d => d.ThoiGianGui, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<TinNhan, TinNhanResponse>()
            .ForMember(d => d.LoaiTinNhan, o => o.MapFrom(s => ParseEnum<LoaiTinNhanEnum>(s.LoaiTinNhan)))
            .ForMember(d => d.DaThuHoi, o => o.MapFrom(s => s.DaThuHoi ?? false))
            .ForMember(d => d.KichThuocFileHienThi, o => o.MapFrom(s => FormatFileSize(s.KichThuocFile)))
            .ForMember(d => d.SoNguoiDaXem, o => o.MapFrom(s => s.DaXemTinNhans.Count))
            .ForMember(d => d.TongReaction, o => o.MapFrom(s => s.ReactionTinNhans.Count))
            .ForMember(d => d.NguoiGui, o => o.Ignore())
            .ForMember(d => d.Sticker, o => o.Ignore())
            .ForMember(d => d.BoDeDinhKem, o => o.Ignore())
            .ForMember(d => d.ThachDauDinhKem, o => o.Ignore())
            .ForMember(d => d.ReplyTo, o => o.Ignore())
            .ForMember(d => d.NguoiDaXem, o => o.Ignore())
            .ForMember(d => d.Reactions, o => o.Ignore())
            .ForMember(d => d.DaXem, o => o.Ignore())
            .ForMember(d => d.LaTinCuaToi, o => o.Ignore())
            .ForMember(d => d.ReactionCuaToi, o => o.Ignore());

        CreateMap<TinNhan, TinNhanTomTatResponse>()
            .ForMember(d => d.LoaiTinNhan, o => o.MapFrom(s => ParseEnum<LoaiTinNhanEnum>(s.LoaiTinNhan)))
            .ForMember(d => d.NoiDungRutGon, o => o.MapFrom(s => Truncate(s.NoiDung, 50)))
            .ForMember(d => d.DaThuHoi, o => o.MapFrom(s => s.DaThuHoi ?? false))
            .ForMember(d => d.TenNguoiGui, o => o.Ignore());

        CreateMap<TinNhan, TinNhanReplyResponse>()
            .ForMember(d => d.LoaiTinNhan, o => o.MapFrom(s => ParseEnum<LoaiTinNhanEnum>(s.LoaiTinNhan)))
            .ForMember(d => d.NoiDungRutGon, o => o.MapFrom(s => Truncate(s.NoiDung, 100)))
            .ForMember(d => d.DaThuHoi, o => o.MapFrom(s => s.DaThuHoi ?? false))
            .ForMember(d => d.NguoiGui, o => o.Ignore());
        #endregion

        #region DaXemTinNhan
        CreateMap<DaXemTinNhan, NguoiXemTinNhanResponse>()
            .ForMember(d => d.NguoiXem, o => o.Ignore());

        CreateMap<DaXemTinNhan, NguoiXemTomTatResponse>()
            .ForMember(d => d.TenNguoiXem, o => o.Ignore())
            .ForMember(d => d.AnhDaiDien, o => o.Ignore());
        #endregion

        #region Reaction
        CreateMap<ReactionBaiDangRequest, ReactionBaiDang>()
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiReaction, o => o.MapFrom(s => s.LoaiReaction.ToString()))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ReactionBaiDang, ReactionBaiDangChiTietResponse>()
            .ForMember(d => d.LoaiReaction, o => o.MapFrom(s => ParseEnum<LoaiReactionEnum>(s.LoaiReaction)))
            .ForMember(d => d.NguoiDung, o => o.Ignore());

        CreateMap<ReactionTinNhanRequest, ReactionTinNhan>()
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ReactionTinNhan, ReactionTinNhanChiTietResponse>()
            .ForMember(d => d.NguoiDung, o => o.Ignore());
        #endregion

        #region Hashtag
        CreateMap<Hashtag, HashtagResponse>()
            .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.SoLuotDung ?? 0))
            .ForMember(d => d.DangThinhHanh, o => o.MapFrom(s => s.DangThinhHanh ?? false));

        CreateMap<Hashtag, HashtagThinhHanhResponse>()
            .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.SoLuotDung ?? 0))
            .ForMember(d => d.ThuHang, o => o.Ignore())
            .ForMember(d => d.PhanTramTangTruong, o => o.Ignore());
        #endregion

        #region Mention
        CreateMap<MentionBaiDang, MentionBaiDangResponse>()
            .ForMember(d => d.BaiDang, o => o.Ignore())
            .ForMember(d => d.MaNguoiMention, o => o.Ignore())
            .ForMember(d => d.NguoiMention, o => o.Ignore())
            .ForMember(d => d.DaDoc, o => o.Ignore());

        CreateMap<MentionBinhLuan, MentionBinhLuanResponse>()
            .ForMember(d => d.BinhLuan, o => o.Ignore())
            .ForMember(d => d.MaBaiDang, o => o.Ignore())
            .ForMember(d => d.MaNguoiMention, o => o.Ignore())
            .ForMember(d => d.NguoiMention, o => o.Ignore())
            .ForMember(d => d.DaDoc, o => o.Ignore());
        #endregion

        #region ThichBinhLuan
        CreateMap<ThichBinhLuan, NguoiThichBinhLuanResponse>()
            .ForMember(d => d.NguoiDung, o => o.Ignore());
        #endregion
    }
}