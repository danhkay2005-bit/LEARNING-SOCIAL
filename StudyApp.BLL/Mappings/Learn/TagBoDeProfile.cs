using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TagBoDeProfile : Profile
    {
        public TagBoDeProfile()
        {
            // =================================================
            // CREATE (GanTagBoDeRequest -> Entity)
            // =================================================
            CreateMap<GanTagBoDeRequest, TagBoDe>()
                .ForMember(dest => dest.ThoiGianThem,
                           opt => opt.MapFrom(_ => DateTime.Now))   // Server set
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaTagNavigation, opt => opt.Ignore());

            // =================================================
            // REMOVE (GoTagBoDeRequest -> Entity)
            // =================================================
            // Dùng để map key khi cần tìm/xóa
            CreateMap<GoTagBoDeRequest, TagBoDe>()
                .ForMember(dest => dest.ThoiGianThem, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaTagNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<TagBoDe, TagBoDeResponse>();

            // =================================================
            // ENTITY -> VIEW RESPONSE
            // =================================================
            CreateMap<TagBoDe, TagBoDeViewResponse>()
                .ForMember(dest => dest.MaTag,
                           opt => opt.MapFrom(src => src.MaTag))
                .ForMember(dest => dest.TenTag,
                           opt => opt.MapFrom(src => src.MaTagNavigation.TenTag));
        }
    }
}
