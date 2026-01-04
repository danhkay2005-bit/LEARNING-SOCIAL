using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class LichSuCloneMapping : Profile
    {
        public LichSuCloneMapping()
        {
            CreateMap<LichSuClone, LichSuCloneResponse>()
                .ForMember(d => d.MaClone, o => o.MapFrom(s => s.MaClone))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(s => s.ThoiGian))
                .ForMember(d => d.BoDeGoc, o => o.Ignore())
                .ForMember(d => d.BoDeClone, o => o.Ignore())
                .ForMember(d => d.NguoiClone, o => o.Ignore());
        }
    }
}
