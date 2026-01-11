using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class LogsGenerateAiProfile : Profile
    {
        public LogsGenerateAiProfile()
        {
            // =================================================
            // CREATE (TaoLogsGenerateAiRequest -> Entity)
            // =================================================
            CreateMap<TaoLogsGenerateAiRequest, LogsGenerateAi>()
                .ForMember(dest => dest.MaLog, opt => opt.Ignore())                 // DB tự sinh
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src => src.TrangThai.ToString()))   // Enum -> string
                .ForMember(dest => dest.ThoiGian,
                           opt => opt.MapFrom(_ => DateTime.Now))                 // Server set
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<LogsGenerateAi, LogsGenerateAiResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<TrangThaiAIEnum>(src.TrangThai ?? "DangXuLy")));

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<LogsGenerateAi, LogsGenerateAiSummaryResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<TrangThaiAIEnum>(src.TrangThai ?? "DangXuLy")));
        }
    }
}
