using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class BaiDangProfile : Profile
    {
        public BaiDangProfile()
        {
            // =================================================
            // CREATE (TaoBaiDangRequest -> Entity)
            // =================================================
            CreateMap<TaoBaiDangRequest, BaiDang>()
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore()) // DB tự sinh
                .ForMember(dest => dest.LoaiBaiDang,
                    opt => opt.MapFrom(src => src.LoaiBaiDang.ToString()))
                .ForMember(dest => dest.QuyenRiengTu,
                    opt => opt.MapFrom(src => (byte)src.QuyenRiengTu))
                .ForMember(dest => dest.SoReaction, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoBinhLuan, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.GhimBaiDang, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.TatBinhLuan, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())

                // Ignore navigation
                .ForMember(dest => dest.BinhLuanBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangGocNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangMoiNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.MaHashtags, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatBaiDangRequest -> Entity)
            // =================================================
            CreateMap<CapNhatBaiDangRequest, BaiDang>()
                .ForMember(dest => dest.QuyenRiengTu,
                    opt => opt.MapFrom(src => (byte)src.QuyenRiengTu))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianSua,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.SoReaction, opt => opt.Ignore())
                .ForMember(dest => dest.SoBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.DaXoa, opt => opt.Ignore())

                // Ignore navigation
                .ForMember(dest => dest.BinhLuanBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangGocNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangMoiNavigations, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBaiDangs, opt => opt.Ignore())
                .ForMember(dest => dest.MaHashtags, opt => opt.Ignore());

            // =================================================
            // PIN / UNPIN (GhimBaiDangRequest -> Entity)
            // =================================================
            CreateMap<GhimBaiDangRequest, BaiDang>()
                .ForMember(dest => dest.GhimBaiDang,
                    opt => opt.MapFrom(src => src.GhimBaiDang))
                 .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.NoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.HinhAnh, opt => opt.Ignore())
                .ForMember(dest => dest.Video, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeLienKet, opt => opt.Ignore())
                .ForMember(dest => dest.MaThanhTuuLienKet, opt => opt.Ignore())
                .ForMember(dest => dest.MaThachDauLienKet, opt => opt.Ignore())
                .ForMember(dest => dest.SoChuoiNgay, opt => opt.Ignore())
                .ForMember(dest => dest.QuyenRiengTu, opt => opt.Ignore())
                .ForMember(dest => dest.SoReaction, opt => opt.Ignore())
                .ForMember(dest => dest.SoBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.TatBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.DaChinhSua, opt => opt.Ignore())
                .ForMember(dest => dest.DaXoa, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())

                 .ForMember(dest => dest.BinhLuanBaiDangs, opt => opt.Ignore())
                    .ForMember(dest => dest.ReactionBaiDangs, opt => opt.Ignore())
                    .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangGocNavigations, opt => opt.Ignore())
                    .ForMember(dest => dest.ChiaSeBaiDangMaBaiDangMoiNavigations, opt => opt.Ignore())
                    .ForMember(dest => dest.MentionBaiDangs, opt => opt.Ignore())
                    .ForMember(dest => dest.MaHashtags, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<BaiDang, BaiDangResponse>()
                .ForMember(dest => dest.LoaiBaiDang,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiBaiDangEnum>(src.LoaiBaiDang ?? "VanBan")))
                .ForMember(dest => dest.QuyenRiengTu,
                    opt => opt.MapFrom(src =>
                        (QuyenRiengTuEnum)(src.QuyenRiengTu ?? (byte)QuyenRiengTuEnum.CongKhai)))
                .ForMember(dest => dest.SoReaction,
                    opt => opt.MapFrom(src => src.SoReaction ?? 0))
                .ForMember(dest => dest.SoBinhLuan,
                    opt => opt.MapFrom(src => src.SoBinhLuan ?? 0));
        }
    }
}
