using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper cho Chia Sẻ Bài Đăng
    /// </summary>
    public class ChiaSeBaiDangMapping : Profile
    {
        public ChiaSeBaiDangMapping()
        {
            MapEntityToResponse();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<ChiaSeBaiDang, ChiaSeBaiDangResponse>()
                .ForMember(d => d.MaChiaSe,
                    o => o.MapFrom(s => s.MaChiaSe))
                .ForMember(d => d.MaBaiDangGoc,
                    o => o.MapFrom(s => s.MaBaiDangGoc))
                .ForMember(d => d.MaNguoiChiaSe,
                    o => o.MapFrom(s => s.MaNguoiChiaSe))
                .ForMember(d => d.NoiDungThem,
                    o => o.MapFrom(s => s.NoiDungThem))
                .ForMember(d => d.MaBaiDangMoi,
                    o => o.MapFrom(s => s.MaBaiDangMoi))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // navigation
                .ForMember(d => d.BaiDangGoc,
                    o => o.Ignore())
                .ForMember(d => d.NguoiChiaSe,
                    o => o.Ignore());
        }

        #endregion
    }
}
