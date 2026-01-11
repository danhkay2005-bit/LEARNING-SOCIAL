using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TheFlashcardProfile : Profile
    {
        public TheFlashcardProfile()
        {
            // =================================================
            // CREATE (TaoTheFlashcardRequest -> Entity)
            // =================================================
            CreateMap<TaoTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())                  // DB tự sinh
                .ForMember(dest => dest.LoaiThe,
                           opt => opt.MapFrom(src => src.LoaiThe.ToString()))       // Enum -> string
                .ForMember(dest => dest.DoKho,
                           opt => opt.MapFrom(src =>
                               src.DoKho.HasValue ? (byte?)src.DoKho.Value : null))// Enum -> byte?
                .ForMember(dest => dest.SoLuongHoc, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLanDung, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLanSai, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ThoiGianTao,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.Ignore())
                .ForMember(dest => dest.CapGheps, opt => opt.Ignore())
                .ForMember(dest => dest.DapAnTracNghiems, opt => opt.Ignore())
                .ForMember(dest => dest.LogsGenerateAis, opt => opt.Ignore())
                .ForMember(dest => dest.PhanTuSapXeps, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHocTaps, opt => opt.Ignore())
                .ForMember(dest => dest.TuDienKhuyets, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatTheFlashcardRequest -> Entity)
            // =================================================
            CreateMap<CapNhatTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())                 // Không cho đổi bộ đề
                .ForMember(dest => dest.LoaiThe,
                           opt => opt.MapFrom(src => src.LoaiThe.ToString()))
                .ForMember(dest => dest.DoKho,
                           opt => opt.MapFrom(src =>
                               src.DoKho.HasValue ? (byte?)src.DoKho.Value : null))
                .ForMember(dest => dest.SoLuongHoc, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanSai, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianCapNhat,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.CapGheps, opt => opt.Ignore())
                .ForMember(dest => dest.DapAnTracNghiems, opt => opt.Ignore())
                .ForMember(dest => dest.LogsGenerateAis, opt => opt.Ignore())
                .ForMember(dest => dest.PhanTuSapXeps, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHocTaps, opt => opt.Ignore())
                .ForMember(dest => dest.TuDienKhuyets, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<TheFlashcard, TheFlashcardResponse>()
                .ForMember(dest => dest.LoaiThe,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<LoaiTheEnum>(src.LoaiThe ?? "CoBan")))
                .ForMember(dest => dest.DoKho,
                           opt => opt.MapFrom(src =>
                               src.DoKho.HasValue ? (MucDoKhoEnum?)src.DoKho.Value : null))
                .ForMember(dest => dest.SoLuongHoc,
                           opt => opt.MapFrom(src => src.SoLuongHoc ?? 0))
                .ForMember(dest => dest.SoLanDung,
                           opt => opt.MapFrom(src => src.SoLanDung ?? 0))
                .ForMember(dest => dest.SoLanSai,
                           opt => opt.MapFrom(src => src.SoLanSai ?? 0));

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<TheFlashcard, TheFlashcardSummaryResponse>()
                .ForMember(dest => dest.LoaiThe,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<LoaiTheEnum>(src.LoaiThe ?? "CoBan")))
                .ForMember(dest => dest.DoKho,
                           opt => opt.MapFrom(src =>
                               src.DoKho.HasValue ? (MucDoKhoEnum?)src.DoKho.Value : null));
        }
    }
}
