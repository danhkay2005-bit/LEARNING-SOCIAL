using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class CapGhepProfile : Profile
    {
        public CapGhepProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<CapGhep, CapGhepResponse>();

            // =========================
            // REQUEST -> ENTITY
            // =========================
            CreateMap<CapGhepRequest, CapGhep>()
                .ForMember(dest => dest.MaCap, opt => opt.Ignore())   // PK
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())  // set trong service
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());
        }
    }
}
