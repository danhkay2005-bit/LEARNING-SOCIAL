using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class BinhLuanBaiDangProfile : Profile
    {
        public BinhLuanBaiDangProfile()
        {
            // =================================================
            // CREATE: TaoBinhLuanRequest -> Entity
            // =================================================
            CreateMap<TaoBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(dest => dest.MaBinhLuan, opt => opt.Ignore()) // Identity
                .ForMember(dest => dest.SoLuotReaction, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())

                // Navigation – luôn ignore
                .ForMember(dest => dest.MaBaiDangNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBinhLuans, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBinhLuans, opt => opt.Ignore());

            // =================================================
            // UPDATE: CapNhatBinhLuanRequest -> Entity
            // =================================================
            CreateMap<CapNhatBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianSua,
                    opt => opt.MapFrom(_ => DateTime.Now))

                // Không cho sửa
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanCha, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotReaction, opt => opt.Ignore())
                .ForMember(dest => dest.DaXoa, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())

                // Navigation
                .ForMember(dest => dest.MaBaiDangNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBinhLuans, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBinhLuans, opt => opt.Ignore());

            // =================================================
            // SOFT DELETE: XoaBinhLuanRequest -> Entity
            // =================================================
            CreateMap<XoaBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(src => src.Xoa))
                .ForMember(dest => dest.ThoiGianSua,
                    opt => opt.MapFrom(_ => DateTime.Now))

                // Ignore tất cả field còn lại
                .ForMember(dest => dest.MaBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDang, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.NoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.HinhAnh, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanCha, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotReaction, opt => opt.Ignore())
                .ForMember(dest => dest.DaChinhSua, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())

                // Navigation
                .ForMember(dest => dest.MaBaiDangNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MentionBinhLuans, opt => opt.Ignore())
                .ForMember(dest => dest.ReactionBinhLuans, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<BinhLuanBaiDang, BinhLuanBaiDangResponse>()
                .ForMember(dest => dest.SoLuotReactions,
                    opt => opt.MapFrom(src => src.SoLuotReaction ?? 0))
                .ForMember(dest => dest.DaChinhSua,
                    opt => opt.MapFrom(src => src.DaChinhSua ?? false))
                .ForMember(dest => dest.DaXoa,
                    opt => opt.MapFrom(src => src.DaXoa ?? false));
        }
    }
}
