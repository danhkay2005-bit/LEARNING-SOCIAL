using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class TagBoDeMapping : Profile
    {
        public TagBoDeMapping()
        {
            CreateMap<TagBoDe, TagResponse>()
                .ForMember(d => d.MaTag, o => o.MapFrom(s => s.MaTag))
                .ForMember(d => d.TenTag, o => o.MapFrom(s => s.MaTagNavigation.TenTag))
                .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.MaTagNavigation.SoLuotDung ?? 0))
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(s => s.MaTagNavigation.ThoiGianTao));
        }
    }
}
