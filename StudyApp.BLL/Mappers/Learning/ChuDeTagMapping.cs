using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Chủ đề (ChuDe) và Tag
    /// </summary>
    public class ChuDeTagMapping : Profile
    {
        public ChuDeTagMapping()
        {
            // =====================================================
            // CHỦ ĐỀ
            // ChuDe -> ChuDeResponse
            // =====================================================
            CreateMap<ChuDe, ChuDeResponse>()
                .ForMember(dest => dest.SoLuotDung,
                    opt => opt.MapFrom(src => src.SoLuotDung ?? 0));

            // =====================================================
            // TAG
            // Tag -> TagResponse
            // =====================================================
            CreateMap<Tag, TagResponse>()
                .ForMember(dest => dest.SoLuotDung,
                    opt => opt.MapFrom(src => src.SoLuotDung ?? 0));
            CreateMap<TagBoDe, TagResponse>()
                .ForMember(d => d.MaTag, o => o.MapFrom(s => s.MaTag))
                .ForMember(d => d.TenTag, o => o.MapFrom(s => s.MaTagNavigation.TenTag))
                .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.MaTagNavigation.SoLuotDung ?? 0))
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(s => s.MaTagNavigation.ThoiGianTao));
        }

    }
}
