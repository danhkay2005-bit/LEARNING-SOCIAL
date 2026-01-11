using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class ThachDauNguoiChoiProfile : Profile
    {
        public ThachDauNguoiChoiProfile()
        {
            // =================================================
            // JOIN (ThamGiaThachDauRequest -> Entity)
            // =================================================
            CreateMap<ThamGiaThachDauRequest, ThachDauNguoiChoi>()
                .ForMember(dest => dest.Diem, opt => opt.Ignore())                 // Chưa có điểm
                .ForMember(dest => dest.SoTheDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheSai, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianLamBaiGiay, opt => opt.Ignore())
                .ForMember(dest => dest.LaNguoiThang, opt => opt.Ignore())
                .ForMember(dest => dest.MaThachDauNavigation, opt => opt.Ignore());

            // =================================================
            // UPDATE RESULT (CapNhatKetQuaThachDauRequest -> Entity)
            // =================================================
            CreateMap<CapNhatKetQuaThachDauRequest, ThachDauNguoiChoi>()
                .ForMember(dest => dest.MaThachDauNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<ThachDauNguoiChoi, ThachDauNguoiChoiResponse>();

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<ThachDauNguoiChoi, ThachDauNguoiChoiSummaryResponse>();
        }
    }
}
