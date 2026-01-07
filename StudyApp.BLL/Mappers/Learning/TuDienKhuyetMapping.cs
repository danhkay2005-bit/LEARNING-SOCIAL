using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Từ điền khuyết (Fill-in-the-blank)
    /// </summary>
    public class TuDienKhuyetMapping : Profile
    {
        public TuDienKhuyetMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // TẠO TỪ ĐIỀN KHUYẾT
            // =====================================================
            CreateMap<TaoTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(dest => dest.MaTuDienKhuyet, opt => opt.Ignore())
                .ForMember(dest => dest.MaThe, opt => opt.Ignore()) // gán ở service
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // CẬP NHẬT TỪ ĐIỀN KHUYẾT
            // =====================================================
            CreateMap<CapNhatTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // TỪ ĐIỀN KHUYẾT CHI TIẾT
            // =====================================================
            CreateMap<TuDienKhuyet, TuDienKhuyetResponse>();

            // =====================================================
            // ENTITY -> RESPONSE
            // TỪ ĐIỀN KHUYẾT KHI HỌC (ẨN ĐÁP ÁN)
            // =====================================================
            CreateMap<TuDienKhuyet, TuDienKhuyetHocResponse>()
                .ForMember(dest => dest.DoRongTu,
                    opt => opt.MapFrom(src => src.TuCanDien.Length));
        }
    }
}
