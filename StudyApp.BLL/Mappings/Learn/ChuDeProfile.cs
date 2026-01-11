using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class ChuDeProfile : Profile
    {
        public ChuDeProfile()
        {
            // =========================
            // Create (Request -> Entity)
            // =========================
            CreateMap<TaoChuDeRequest, ChuDe>()
                .ForMember(dest => dest.MaChuDe, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.SoLuotDung, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore());

            // =========================
            // Update (Request -> Entity)
            // =========================
            CreateMap<CapNhatChuDeRequest, ChuDe>()
                .ForMember(dest => dest.SoLuotDung, opt => opt.Ignore())       // Không sửa bằng request
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())      // Không đổi thời gian tạo
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore());

            // =========================
            // Entity -> Response
            // =========================
            CreateMap<ChuDe, ChuDeResponse>()
                .ForMember(dest => dest.SoLuotDung,
                           opt => opt.MapFrom(src => src.SoLuotDung ?? 0));

            CreateMap<ChuDe, ChuDeSelectResponse>();
        }
    }
}
