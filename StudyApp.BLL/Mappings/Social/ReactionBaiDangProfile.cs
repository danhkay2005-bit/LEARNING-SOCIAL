using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class ReactionBaiDangProfile : Profile
    {
        public ReactionBaiDangProfile()
        {
            // =====================================================
            // REQUEST -> ENTITY (Create / Update - Upsert)
            // =====================================================
            CreateMap<TaoHoacCapNhatReactionBaiDangRequest, ReactionBaiDang>()
                .ForMember(dest => dest.LoaiReaction,
                    opt => opt.MapFrom(src => src.LoaiReaction.ToString()))
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBaiDangNavigation,
                    opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // =====================================================
            CreateMap<ReactionBaiDang, ReactionBaiDangResponse>()
                .ForMember(dest => dest.LoaiReaction,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiReactionEnum>(src.LoaiReaction ?? "Thich")));
        }
    }
}
