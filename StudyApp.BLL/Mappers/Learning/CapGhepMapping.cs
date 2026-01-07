using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    public class CapGhepMapping : Profile
    {
        public CapGhepMapping()
        {
            // ============================
            // REQUEST → ENTITY
            // ============================

            // Tạo cặp ghép mới
            CreateMap<TaoCapGhepRequest, CapGhep>()
                .ForMember(dest => dest.MaCap, opt => opt.Ignore())
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // Cập nhật cặp ghép
            CreateMap<CapNhatCapGhepRequest, CapGhep>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // ============================
            // ENTITY → RESPONSE
            // ============================

            // Response chi tiết
            CreateMap<CapGhep, CapGhepResponse>()
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0));

            // Response dùng khi học (vế trái)
            CreateMap<CapGhep, CapGhepHocResponse>()
                .ForMember(dest => dest.ThuTuVeTrai,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0));

            // Response danh sách vế phải (đã trộn)
            CreateMap<CapGhep, VePhaiItem>()
                .ForMember(dest => dest.ThuTuHienThi,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0));
        }
    }
}
