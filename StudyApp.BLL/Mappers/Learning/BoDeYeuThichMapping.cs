using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BoDeYeuThichMapping : Profile
    {
        public BoDeYeuThichMapping()
        {
            CreateMap<ThemBoDeYeuThichRequest, BoDeYeuThich>()
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.ThoiGianLuu, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

            CreateMap<BoDeYeuThich, BoDeYeuThichResponse>()
                .ForMember(d => d.BoDe, o => o.Ignore());
        }
    }
}
