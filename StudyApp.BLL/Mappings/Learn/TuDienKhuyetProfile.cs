using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TuDienKhuyetProfile : Profile
    {
        public TuDienKhuyetProfile()
        {
            // =================================================
            // CREATE (TaoTuDienKhuyetRequest -> Entity)
            // =================================================
            CreateMap<TaoTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(dest => dest.MaTuDienKhuyet, opt => opt.Ignore()) // DB tự sinh
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatTuDienKhuyetRequest -> Entity)
            // =================================================
            CreateMap<CapNhatTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())          // Không cho đổi thẻ
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<TuDienKhuyet, TuDienKhuyetResponse>();

            // =================================================
            // ENTITY -> RESPONSE (VIEW – không lộ đáp án)
            // =================================================
            CreateMap<TuDienKhuyet, TuDienKhuyetViewResponse>();
        }
    }
}
    