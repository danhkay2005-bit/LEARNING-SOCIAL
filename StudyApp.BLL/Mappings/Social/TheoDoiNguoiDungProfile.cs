using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class TheoDoiNguoiDungProfile : Profile
    {
        public TheoDoiNguoiDungProfile()
        {
            // Entity -> Response
            CreateMap<TheoDoiNguoiDung, TheoDoiNguoiDungResponse>();

            // Request -> Entity
            CreateMap<TheoDoiNguoiDungRequest, TheoDoiNguoiDung>()
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
