using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class TagMapping : Profile
    {
        public TagMapping() 
        {
            CreateMap<Tag, TagResponse>()
                .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.SoLuotDung ?? 0));
        }
    }
}
