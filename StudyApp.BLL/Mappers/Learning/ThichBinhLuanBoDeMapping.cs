using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class ThichBinhLuanBoDeMapping : Profile
    {
        public ThichBinhLuanBoDeMapping()
        {

            CreateMap<ThichBinhLuanBoDe, NguoiThichBinhLuanBoDeResponse>()
                .ForMember(d => d.MaNguoiDung, o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.MaBinhLuan, o => o.MapFrom(s => s.MaBinhLuan))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(s => s.ThoiGian))
                .ForMember(d => d.NguoiDung, o => o.Ignore());

        }
    }
}
