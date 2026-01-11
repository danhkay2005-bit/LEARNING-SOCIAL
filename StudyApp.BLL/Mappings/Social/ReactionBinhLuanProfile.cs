using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class ReactionBinhLuanProfile : Profile
    {
        public ReactionBinhLuanProfile()
        {
            // =================================================
            // CREATE / UPDATE (Upsert)
            // =================================================
            CreateMap<TaoHoacCapNhatReactionBinhLuanRequest, ReactionBinhLuan>()
                .ForMember(dest => dest.LoaiReaction,
                    opt => opt.MapFrom(src => src.LoaiReaction.ToString()))
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.Now))

                // Navigation
                .ForMember(dest => dest.MaBinhLuanNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<ReactionBinhLuan, ReactionBinhLuanResponse>()
                .ForMember(dest => dest.LoaiReaction,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiReactionEnum>(src.LoaiReaction ?? "Thich")));
        }
    }
}
