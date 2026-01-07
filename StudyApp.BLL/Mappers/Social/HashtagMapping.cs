using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Mapping cho Hashtag
    /// </summary>
    public class HashtagMapping : Profile
    {
        public HashtagMapping()
        {
            MapEntityToResponse();
        }

        private void MapEntityToResponse()
        {
            // ===== HASHTAG CƠ BẢN =====
            CreateMap<Hashtag, HashtagResponse>()
                .ForMember(d => d.MaHashtag,
                    o => o.MapFrom(s => s.MaHashtag))
                .ForMember(d => d.TenHashtag,
                    o => o.MapFrom(s => s.TenHashtag))
                .ForMember(d => d.SoLuotDung,
                    o => o.MapFrom(s => s.SoLuotDung ?? 0))
                .ForMember(d => d.DangThinhHanh,
                    o => o.MapFrom(s => s.DangThinhHanh ?? false))
                .ForMember(d => d.ThoiGianTao,
                    o => o.MapFrom(s => s.ThoiGianTao));

            // ===== HASHTAG THỊNH HÀNH =====
            CreateMap<Hashtag, HashtagThinhHanhResponse>()
                .ForMember(d => d.MaHashtag,
                    o => o.MapFrom(s => s.MaHashtag))
                .ForMember(d => d.TenHashtag,
                    o => o.MapFrom(s => s.TenHashtag))
                .ForMember(d => d.SoLuotDung,
                    o => o.MapFrom(s => s.SoLuotDung ?? 0))

                // tính toán ở Service
                .ForMember(d => d.ThuHang,
                    o => o.Ignore())
                .ForMember(d => d.PhanTramTangTruong,
                    o => o.Ignore());
        }
    }
}
