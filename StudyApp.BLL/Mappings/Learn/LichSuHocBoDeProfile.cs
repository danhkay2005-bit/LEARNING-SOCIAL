using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using StudyApp.DTO.Responses.Learn.StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class LichSuHocBoDeProfile : Profile
    {
        public LichSuHocBoDeProfile()
        {
            // =========================
            // Create (Request -> Entity)
            // =========================
            CreateMap<TaoLichSuHocBoDeRequest, LichSuHocBoDe>()
                .ForMember(dest => dest.MaLichSu, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaPhienNavigation, opt => opt.Ignore());

            // =========================
            // Entity -> Response
            // =========================
            CreateMap<LichSuHocBoDe, LichSuHocBoDeResponse>();

            // =========================
            // Entity -> SummaryResponse
            // =========================
            CreateMap<LichSuHocBoDe, LichSuHocBoDeSummaryResponse>();
        }
    }
}
