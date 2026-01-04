using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class CapGhepMapping : Profile
    {
        public CapGhepMapping()
        {
            CreateMap<TaoCapGhepRequest, CapGhep>()
                .ForMember(d => d.MaCap, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<CapNhatCapGhepRequest, CapGhep>()
                .ForMember(d => d.MaCap, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<CapGhep, CapGhepResponse>()
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

            CreateMap<CapGhep, CapGhepHocResponse>()
                .ForMember(d => d.ThuTuVeTrai, o => o.MapFrom(s => s.ThuTu ?? 0));
        }
    }
}
