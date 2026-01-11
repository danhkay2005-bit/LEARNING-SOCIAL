using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class PhanTuSapXepProfile : Profile
    {
        public PhanTuSapXepProfile()
        {
            // =========================
            // Create (Request -> Entity)
            // =========================
            CreateMap<TaoPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(dest => dest.MaPhanTu, opt => opt.Ignore())      // DB tự sinh
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =========================
            // Update (Request -> Entity)
            // =========================
            CreateMap<CapNhatPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())        // Không cho đổi thẻ
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =========================
            // Entity -> Response
            // =========================
            CreateMap<PhanTuSapXep, PhanTuSapXepResponse>();

            // =========================
            // Entity -> ViewResponse
            // =========================
            CreateMap<PhanTuSapXep, PhanTuSapXepViewResponse>();
        }
    }
}
