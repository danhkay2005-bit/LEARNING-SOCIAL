using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            // =================================================
            // CREATE (TaoTagRequest -> Entity)
            // =================================================
            CreateMap<TaoTagRequest, Tag>()
                .ForMember(dest => dest.MaTag, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.SoLuotDung, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.TagBoDes, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatTagRequest -> Entity)
            // =================================================
            CreateMap<CapNhatTagRequest, Tag>()
                .ForMember(dest => dest.SoLuotDung, opt => opt.Ignore())     // Không update thống kê
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())    // Không đổi ngày tạo
                .ForMember(dest => dest.TagBoDes, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<Tag, TagResponse>()
                .ForMember(dest => dest.SoLuotDung,
                           opt => opt.MapFrom(src => src.SoLuotDung ?? 0));

            // =================================================
            // ENTITY -> RESPONSE (SELECT)
            // =================================================
            CreateMap<Tag, TagSelectResponse>();
        }
    }
}
