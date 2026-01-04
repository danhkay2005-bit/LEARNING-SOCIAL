using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class DapAnTracNghiemMapping : Profile
    {
        public DapAnTracNghiemMapping()
        {
            CreateMap<TaoDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(d => d.MaDapAn, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<CapNhatDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(d => d.MaDapAn, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<DapAnTracNghiem, DapAnTracNghiemResponse>()
                .ForMember(d => d.LaDapAnDung, o => o.MapFrom(s => s.LaDapAnDung ?? false))
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

            CreateMap<DapAnTracNghiem, DapAnTracNghiemHocResponse>()
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));
        }
    }
}
