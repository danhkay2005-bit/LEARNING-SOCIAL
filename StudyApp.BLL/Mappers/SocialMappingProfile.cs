using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper Profile cho module Social
    /// </summary>
    public class SocialMappingProfile : Profile
    {
        public SocialMappingProfile()
        {
            ConfigureBaiDangMappings();
            ConfigureBinhLuanBaiDangMappings();
            ConfigureBanBeMappings();
            ConfigureTheoDoiMappings();
            ConfigureChanNguoiDungMappings();
            ConfigureChiaSeBaiDangMappings();
            ConfigureCuocTroChuyenMappings();
            ConfigureThanhVienCuocTroChuyenMappings();
            ConfigureTinNhanMappings();
            ConfigureDaXemTinNhanMappings();
            ConfigureReactionMappings();
            ConfigureHashtagMappings();
            ConfigureMentionMappings();
            ConfigureThichBinhLuanMappings();
        }

        #region BaiDang Mappings
        private void ConfigureBaiDangMappings()
        {
            // ========== Request -> Entity ==========

            // TaoBaiDangRequest -> BaiDang
            CreateMap<TaoBaiDangRequest, BaiDang>()
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.MapFrom(src => src.LoaiBaiDang.ToString()))
                .ForMember(dest => dest.QuyenRiengTu, opt => opt.MapFrom(src => (byte)src.QuyenRiengTu))
                .ForMember(dest => dest.SoReaction, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoBinhLuan, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLuotXem, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.GhimBaiDang, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())
                // Ignore navigation properties
                .ForMember(dest => dest.BinhLuanBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangGocNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangMoiNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.MaHashtags, opt => opt.Ignore());

            // CapNhatBaiDangRequest -> BaiDang (partial update)
            CreateMap<CapNhatBaiDangRequest, BaiDang>()
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.QuyenRiengTu, opt => opt.MapFrom((src, dest) =>
                    src.QuyenRiengTu.HasValue ? (byte)src.QuyenRiengTu.Value : dest.QuyenRiengTu))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ========== Entity -> Response ==========

            // BaiDang -> BaiDangResponse
            CreateMap<BaiDang, BaiDangResponse>()
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.MapFrom(src => ParseEnum<LoaiBaiDangEnum>(src.LoaiBaiDang)))
                .ForMember(dest => dest.QuyenRiengTu, opt => opt.MapFrom(src => (QuyenRiengTuEnum)(src.QuyenRiengTu ?? 1)))
                .ForMember(dest => dest.SoReaction, opt => opt.MapFrom(src => src.SoReaction ?? 0))
                .ForMember(dest => dest.SoBinhLuan, opt => opt.MapFrom(src => src.SoBinhLuan ?? 0))
                .ForMember(dest => dest.SoLuotXem, opt => opt.MapFrom(src => src.SoLuotXem ?? 0))
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.MapFrom(src => src.SoLuotChiaSe ?? 0))
                .ForMember(dest => dest.GhimBaiDang, opt => opt.MapFrom(src => src.GhimBaiDang ?? false))
                .ForMember(dest => dest.TatBinhLuan, opt => opt.MapFrom(src => src.TatBinhLuan ?? false))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(src => src.DaChinhSua ?? false))
                .ForMember(dest => dest.Hashtags, opt => opt.MapFrom(src => src.MaHashtags))
                // Fields cần set trong service
                .ForMember(dest => dest.NguoiDang, opt => opt.Ignore())
                .ForMember(dest => dest.DaReaction, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiReactionCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.DaLuu, opt => opt.Ignore())
                .ForMember(dest => dest.MentionNguoiDungs, opt => opt.Ignore())
                .ForMember(dest => dest.TopReactions, opt => opt.Ignore())
                .ForMember(dest => dest.BaiDangGoc, opt => opt.Ignore())
                .ForMember(dest => dest.BoDeLienKet, opt => opt.Ignore())
                .ForMember(dest => dest.ThanhTuuLienKet, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauLienKet, opt => opt.Ignore());

            // BaiDang -> BaiDangTomTatResponse
            CreateMap<BaiDang, BaiDangTomTatResponse>()
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.MapFrom(src => ParseEnum<LoaiBaiDangEnum>(src.LoaiBaiDang)))
                .ForMember(dest => dest.NoiDungRutGon, opt => opt.MapFrom(src => Truncate(src.NoiDung, 200)))
                .ForMember(dest => dest.HinhAnhDauTien, opt => opt.MapFrom(src => GetFirstImage(src.HinhAnh)))
                .ForMember(dest => dest.SoReaction, opt => opt.MapFrom(src => src.SoReaction ?? 0))
                .ForMember(dest => dest.SoBinhLuan, opt => opt.MapFrom(src => src.SoBinhLuan ?? 0))
                .ForMember(dest => dest.NguoiDang, opt => opt.Ignore());

            // BaiDang -> BaiDangGocResponse
            CreateMap<BaiDang, BaiDangGocResponse>()
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.MapFrom(src => ParseEnum<LoaiBaiDangEnum>(src.LoaiBaiDang)))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(src => src.DaXoa ?? false))
                .ForMember(dest => dest.NguoiDang, opt => opt.Ignore());
        }
        #endregion

        #region BinhLuanBaiDang Mappings
        private void ConfigureBinhLuanBaiDangMappings()
        {
            // Request -> Entity
            CreateMap<TaoBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(dest => dest.MaBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotThich, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBinhLuans, opt => opt.Ignore())
                .ForMember(dest => dest.ThichBinhLuans, opt => opt.Ignore());

            CreateMap<CapNhatBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(dest => dest.MaBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanCha, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Entity -> Response
            CreateMap<BinhLuanBaiDang, BinhLuanResponse>()
                .ForMember(dest => dest.SoLuotThich, opt => opt.MapFrom(src => src.SoLuotThich ?? 0))
                .ForMember(dest => dest.SoTraLoi, opt => opt.MapFrom(src => src.InverseMaBinhLuanChaNavigation.Count))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(src => src.DaChinhSua ?? false))
                .ForMember(dest => dest.NguoiBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.Sticker, opt => opt.Ignore())
                .ForMember(dest => dest.DaThich, opt => opt.Ignore())
                .ForMember(dest => dest.MentionNguoiDungs, opt => opt.Ignore())
                .ForMember(dest => dest.TraLois, opt => opt.Ignore());

            CreateMap<BinhLuanBaiDang, BinhLuanTomTatResponse>()
                .ForMember(dest => dest.NoiDungRutGon, opt => opt.MapFrom(src => Truncate(src.NoiDung, 100)))
                .ForMember(dest => dest.SoLuotThich, opt => opt.MapFrom(src => src.SoLuotThich ?? 0))
                .ForMember(dest => dest.NguoiBinhLuan, opt => opt.Ignore());
        }
        #endregion

        #region BanBe Mappings
        private void ConfigureBanBeMappings()
        {
            CreateMap<BanBe, BanBeResponse>()
                .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => ParseEnum<TrangThaiBanBeEnum>(src.TrangThai)))
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // Set trong service
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LaNguoiGui, opt => opt.Ignore())
                .ForMember(dest => dest.SoBanChung, opt => opt.Ignore());

            CreateMap<BanBe, LoiMoiKetBanResponse>()
                .ForMember(dest => dest.MaNguoiGui, opt => opt.MapFrom(src => src.MaNguoiGui))
                .ForMember(dest => dest.ThoiGianGui, opt => opt.MapFrom(src => src.ThoiGianGui))
                .ForMember(dest => dest.NguoiGui, opt => opt.Ignore())
                .ForMember(dest => dest.SoBanChung, opt => opt.Ignore())
                .ForMember(dest => dest.BanChung, opt => opt.Ignore());
        }
        #endregion

        #region TheoDoi Mappings
        private void ConfigureTheoDoiMappings()
        {
            CreateMap<TheoDoiRequest, TheoDoi>()
                .ForMember(dest => dest.MaNguoiTheoDoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<TheoDoi, TheoDoiResponse>()
                .ForMember(dest => dest.ThongBaoBaiMoi, opt => opt.MapFrom(src => src.ThongBaoBaiMoi ?? true))
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.DangTheoDoi, opt => opt.Ignore())
                .ForMember(dest => dest.DuocTheoDoi, opt => opt.Ignore());
        }
        #endregion

        #region ChanNguoiDung Mappings
        private void ConfigureChanNguoiDungMappings()
        {
            CreateMap<ChanNguoiDungRequest, ChanNguoiDung>()
                .ForMember(dest => dest.MaNguoiChan, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<ChanNguoiDung, NguoiBiChanResponse>()
                .ForMember(dest => dest.NguoiBiChan, opt => opt.Ignore());
        }
        #endregion

        #region ChiaSeBaiDang Mappings
        private void ConfigureChiaSeBaiDangMappings()
        {
            CreateMap<ChiaSeBaiDangRequest, ChiaSeBaiDang>()
                .ForMember(dest => dest.MaChiaSe, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiChiaSe, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangMoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaBaiDangGocNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangMoiNavigation, opt => opt.Ignore());

            CreateMap<ChiaSeBaiDang, ChiaSeBaiDangResponse>()
                .ForMember(dest => dest.BaiDangGoc, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiChiaSe, opt => opt.Ignore());
        }
        #endregion

        #region CuocTroChuyen Mappings
        private void ConfigureCuocTroChuyenMappings()
        {
            // Request -> Entity
            CreateMap<TaoCuocTroChuyenNhomRequest, CuocTroChuyen>()
                .ForMember(dest => dest.MaCuocTroChuyen, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiCuocTroChuyen, opt => opt.MapFrom(_ => LoaiCuocTroChuyenEnum.Nhom.ToString()))
                .ForMember(dest => dest.MaNguoiTaoNhom, opt => opt.Ignore())
                .ForMember(dest => dest.TinNhanCuoiId, opt => opt.Ignore())
                .ForMember(dest => dest.NoiDungTinCuoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTinCuoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThanhVienCuocTroChuyens, opt => opt.Ignore())
                .ForMember(dest => dest.TinNhans, opt => opt.Ignore());

            CreateMap<CapNhatNhomChatRequest, CuocTroChuyen>()
                .ForMember(dest => dest.MaCuocTroChuyen, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Entity -> Response
            CreateMap<CuocTroChuyen, CuocTroChuyenResponse>()
                .ForMember(dest => dest.LoaiCuocTroChuyen, opt => opt.MapFrom(src => ParseEnum<LoaiCuocTroChuyenEnum>(src.LoaiCuocTroChuyen)))
                .ForMember(dest => dest.SoThanhVien, opt => opt.MapFrom(src => src.ThanhVienCuocTroChuyens.Count(tv => tv.DaRoiNhom != true)))
                .ForMember(dest => dest.NguoiTaoNhom, opt => opt.Ignore())
                .ForMember(dest => dest.TinNhanCuoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThanhViens, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiNhan, opt => opt.Ignore())
                .ForMember(dest => dest.SoTinChuaDoc, opt => opt.Ignore())
                .ForMember(dest => dest.TatThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.GhimCuocTro, opt => opt.Ignore())
                .ForMember(dest => dest.BiDanhCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.VaiTroCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiNhanOnline, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianOnlineCuoi, opt => opt.Ignore());

            CreateMap<CuocTroChuyen, CuocTroChuyenTomTatResponse>()
                .ForMember(dest => dest.LoaiCuocTroChuyen, opt => opt.MapFrom(src => ParseEnum<LoaiCuocTroChuyenEnum>(src.LoaiCuocTroChuyen)))
                .ForMember(dest => dest.TenHienThi, opt => opt.MapFrom(src => src.TenNhomChat))
                .ForMember(dest => dest.AnhHienThi, opt => opt.MapFrom(src => src.AnhNhomChat))
                .ForMember(dest => dest.NoiDungTinCuoi, opt => opt.MapFrom(src => src.NoiDungTinCuoi))
                .ForMember(dest => dest.SoTinChuaDoc, opt => opt.Ignore())
                .ForMember(dest => dest.TatThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.GhimCuocTro, opt => opt.Ignore())
                .ForMember(dest => dest.Online, opt => opt.Ignore());
        }
        #endregion

        #region ThanhVienCuocTroChuyen Mappings
        private void ConfigureThanhVienCuocTroChuyenMappings()
        {
            CreateMap<ThanhVienCuocTroChuyen, ThanhVienCuocTroChuyenResponse>()
                .ForMember(dest => dest.VaiTro, opt => opt.MapFrom(src => ParseEnum<VaiTroThanhVienChatEnum>(src.VaiTro)))
                .ForMember(dest => dest.TatThongBao, opt => opt.MapFrom(src => src.TatThongBao ?? false))
                .ForMember(dest => dest.DaRoiNhom, opt => opt.MapFrom(src => src.DaRoiNhom ?? false))
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.DangOnline, opt => opt.Ignore());

            CreateMap<ThanhVienCuocTroChuyen, ThanhVienTomTatResponse>()
                .ForMember(dest => dest.VaiTro, opt => opt.MapFrom(src => ParseEnum<VaiTroThanhVienChatEnum>(src.VaiTro)))
                .ForMember(dest => dest.TenHienThi, opt => opt.Ignore())
                .ForMember(dest => dest.AnhDaiDien, opt => opt.Ignore())
                .ForMember(dest => dest.DangOnline, opt => opt.Ignore());
        }
        #endregion

        #region TinNhan Mappings
        private void ConfigureTinNhanMappings()
        {
            // Request -> Entity
            CreateMap<GuiTinNhanRequest, TinNhan>()
                .ForMember(dest => dest.MaTinNhan, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiGui, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiTinNhan, opt => opt.MapFrom(src => src.LoaiTinNhan.ToString()))
                .ForMember(dest => dest.DaThuHoi, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianThuHoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianGui, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaCuocTroChuyenNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ReplyTo, opt => opt.Ignore())
                .ForMember(dest => dest.InverseReplyTo, opt => opt.Ignore())
                .ForMember(dest => dest.DaXemTinNhans, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionTinNhans, opt => opt.Ignore());

            // Entity -> Response
            CreateMap<TinNhan, TinNhanResponse>()
                .ForMember(dest => dest.LoaiTinNhan, opt => opt.MapFrom(src => ParseEnum<LoaiTinNhanEnum>(src.LoaiTinNhan)))
                .ForMember(dest => dest.DaThuHoi, opt => opt.MapFrom(src => src.DaThuHoi ?? false))
                .ForMember(dest => dest.KichThuocFileHienThi, opt => opt.MapFrom(src => FormatFileSize(src.KichThuocFile)))
                .ForMember(dest => dest.SoNguoiDaXem, opt => opt.MapFrom(src => src.DaXemTinNhans.Count))
                .ForMember(dest => dest.TongReaction, opt => opt.MapFrom(src => src.ReactionTinNhans.Count))
                .ForMember(dest => dest.NguoiGui, opt => opt.Ignore())
                .ForMember(dest => dest.Sticker, opt => opt.Ignore())
                .ForMember(dest => dest.BoDeDinhKem, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauDinhKem, opt => opt.Ignore())
                .ForMember(dest => dest.ReplyTo, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiDaXem, opt => opt.Ignore())
                .ForMember(dest => dest.Reactions, opt => opt.Ignore())
                .ForMember(dest => dest.DaXem, opt => opt.Ignore())
                .ForMember(dest => dest.LaTinCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionCuaToi, opt => opt.Ignore());

            CreateMap<TinNhan, TinNhanTomTatResponse>()
                .ForMember(dest => dest.LoaiTinNhan, opt => opt.MapFrom(src => ParseEnum<LoaiTinNhanEnum>(src.LoaiTinNhan)))
                .ForMember(dest => dest.NoiDungRutGon, opt => opt.MapFrom(src => Truncate(src.NoiDung, 50)))
                .ForMember(dest => dest.DaThuHoi, opt => opt.MapFrom(src => src.DaThuHoi ?? false))
                .ForMember(dest => dest.TenNguoiGui, opt => opt.Ignore());

            CreateMap<TinNhan, TinNhanReplyResponse>()
                .ForMember(dest => dest.LoaiTinNhan, opt => opt.MapFrom(src => ParseEnum<LoaiTinNhanEnum>(src.LoaiTinNhan)))
                .ForMember(dest => dest.NoiDungRutGon, opt => opt.MapFrom(src => Truncate(src.NoiDung, 100)))
                .ForMember(dest => dest.DaThuHoi, opt => opt.MapFrom(src => src.DaThuHoi ?? false))
                .ForMember(dest => dest.NguoiGui, opt => opt.Ignore());
        }
        #endregion

        #region DaXemTinNhan Mappings
        private void ConfigureDaXemTinNhanMappings()
        {
            CreateMap<DaXemTinNhan, NguoiXemTinNhanResponse>()
                .ForMember(dest => dest.NguoiXem, opt => opt.Ignore());

            CreateMap<DaXemTinNhan, NguoiXemTomTatResponse>()
                .ForMember(dest => dest.TenNguoiXem, opt => opt.Ignore())
                .ForMember(dest => dest.AnhDaiDien, opt => opt.Ignore());
        }
        #endregion

        #region Reaction Mappings
        private void ConfigureReactionMappings()
        {
            // Reaction Bài đăng
            CreateMap<ReactionBaiDangRequest, ReactionBaiDang>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiReaction, opt => opt.MapFrom(src => src.LoaiReaction.ToString()))
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaBaiDangNavigation, opt => opt.Ignore());

            CreateMap<ReactionBaiDang, ReactionBaiDangChiTietResponse>()
                .ForMember(dest => dest.LoaiReaction, opt => opt.MapFrom(src => ParseEnum<LoaiReactionEnum>(src.LoaiReaction)))
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore());

            // Reaction Tin nhắn
            CreateMap<ReactionTinNhanRequest, ReactionTinNhan>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaTinNhanNavigation, opt => opt.Ignore());

            CreateMap<ReactionTinNhan, ReactionTinNhanChiTietResponse>()
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore());
        }
        #endregion

        #region Hashtag Mappings
        private void ConfigureHashtagMappings()
        {
            CreateMap<Hashtag, HashtagResponse>()
                .ForMember(dest => dest.SoLuotDung, opt => opt.MapFrom(src => src.SoLuotDung ?? 0))
                .ForMember(dest => dest.DangThinhHanh, opt => opt.MapFrom(src => src.DangThinhHanh ?? false));

            CreateMap<Hashtag, HashtagThinhHanhResponse>()
                .ForMember(dest => dest.SoLuotDung, opt => opt.MapFrom(src => src.SoLuotDung ?? 0))
                .ForMember(dest => dest.ThuHang, opt => opt.Ignore())
                .ForMember(dest => dest.PhanTramTangTruong, opt => opt.Ignore());
        }
        #endregion

        #region Mention Mappings
        private void ConfigureMentionMappings()
        {
            CreateMap<MentionBaiDang, MentionBaiDangResponse>()
                .ForMember(dest => dest.BaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiMention, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiMention, opt => opt.Ignore())
                .ForMember(dest => dest.DaDoc, opt => opt.Ignore());

            CreateMap<MentionBinhLuan, MentionBinhLuanResponse>()
                .ForMember(dest => dest.BinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiMention, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiMention, opt => opt.Ignore())
                .ForMember(dest => dest.DaDoc, opt => opt.Ignore());
        }
        #endregion

        #region ThichBinhLuan Mappings
        private void ConfigureThichBinhLuanMappings()
        {
            CreateMap<ThichBinhLuan, NguoiThichBinhLuanResponse>()
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore());
        }
        #endregion
    }
}