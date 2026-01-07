using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BaoCaoBoDeMapping : Profile
    {
        public BaoCaoBoDeMapping()
        {
            // ============================
            // REQUEST → ENTITY
            // ============================

            // Người dùng gửi báo cáo
            CreateMap<BaoCaoBoDeRequest, BaoCaoBoDe>()
                .ForMember(dest => dest.LyDo,
                    opt => opt.MapFrom(src => src.LyDo.ToString()))
                .ForMember(dest => dest.TrangThai,
                    opt => opt.MapFrom(_ => TrangThaiBaoCaoEnum.ChoDuyet.ToString()))
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.Now));

            // Admin xử lý báo cáo
            CreateMap<XuLyBaoCaoRequest, BaoCaoBoDe>()
                .ForMember(dest => dest.TrangThai,
                    opt => opt.MapFrom(src => src.TrangThai.ToString()))
                // PATCH: chỉ update field được gửi
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null)
                );

            // ============================
            // ENTITY → RESPONSE
            // ============================

            CreateMap<BaoCaoBoDe, BaoCaoBoDeResponse>()
                .ForMember(dest => dest.LyDo,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LyDoBaoCaoEnum>(src.LyDo)))
                .ForMember(dest => dest.TrangThai,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<TrangThaiBaoCaoEnum>(src.TrangThai ?? TrangThaiBaoCaoEnum.ChoDuyet.ToString())))
                .ForMember(dest => dest.BoDe,
                    opt => opt.MapFrom(src => src.MaBoDeNavigation));
        }
    }
}
