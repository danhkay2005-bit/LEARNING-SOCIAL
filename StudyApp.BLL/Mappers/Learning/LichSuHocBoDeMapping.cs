using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class LichSuHocBoDeMapping : Profile
    {
        public LichSuHocBoDeMapping()
        {
            CreateMap<LichSuHocBoDe, LichSuHocBoDeResponse>()
                .ForMember(d => d.SoTheHoc, o => o.MapFrom(s => s.SoTheHoc ?? 0))
                .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
                .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
                .ForMember(d => d.BoDe, o => o.Ignore());


        }
    }
}
