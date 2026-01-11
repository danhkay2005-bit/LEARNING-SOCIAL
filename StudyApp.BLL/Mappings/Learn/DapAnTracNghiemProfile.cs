using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class DapAnTracNghiemProfile : Profile
    {
        public DapAnTracNghiemProfile()
        {
            // =========================
            // Create (Request -> Entity)
            // =========================
            CreateMap<TaoDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(dest => dest.MaDapAn, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.LaDapAnDung, opt => opt.MapFrom(src => src.LaDapAnDung))
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =========================
            // Update (Request -> Entity)
            // =========================
            CreateMap<CapNhatDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())            // Không đổi thẻ
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =========================
            // Entity -> Response
            // =========================
            CreateMap<DapAnTracNghiem, DapAnTracNghiemResponse>()
                .ForMember(dest => dest.LaDapAnDung,
                           opt => opt.MapFrom(src => src.LaDapAnDung ?? false));

            // =========================
            // Entity -> ViewResponse
            // =========================
            CreateMap<DapAnTracNghiem, DapAnTracNghiemViewResponse>();
        }
    }
}
