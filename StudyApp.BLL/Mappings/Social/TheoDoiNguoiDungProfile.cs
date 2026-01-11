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
            // CREATE (Follow)
            CreateMap<TheoDoiNguoiDungRequest, TheoDoiNguoiDung>()
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.Now));

            // ENTITY -> RESPONSE
            CreateMap<TheoDoiNguoiDung, TheoDoiNguoiDungResponse>();
        }
    }
}
