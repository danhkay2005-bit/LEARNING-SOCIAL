using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class CapDoProfile : Profile
    {
        public CapDoProfile()
        {
            // =====================================================
            // CREATE
            // =====================================================
            CreateMap<TaoCapDoRequest, CapDo>()
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.NguoiDungs,
                    opt => opt.Ignore());

            // =====================================================
            // UPDATE
            // =====================================================
            CreateMap<CapNhatCapDoRequest, CapDo>()
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.Ignore())
                .ForMember(dest => dest.NguoiDungs,
                    opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // =====================================================
            CreateMap<CapDo, CapDoResponse>();
        }
    }
}
