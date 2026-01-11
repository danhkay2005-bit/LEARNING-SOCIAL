using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class ChiTietTraLoiProfile : Profile
    {
        public ChiTietTraLoiProfile()
        {
            // =========================
            // Request -> Entity
            // =========================
            CreateMap<ChiTietTraLoiRequest, ChiTietTraLoi>()
                .ForMember(dest => dest.MaTraLoi, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.TraLoiDung, opt => opt.Ignore())        // Server chấm
                .ForMember(dest => dest.DapAnDung, opt => opt.Ignore())         // Server gán
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaPhienNavigation, opt => opt.Ignore());

            // =========================
            // Entity -> Response
            // =========================
            CreateMap<ChiTietTraLoi, ChiTietTraLoiResponse>();
        }
    }
}
