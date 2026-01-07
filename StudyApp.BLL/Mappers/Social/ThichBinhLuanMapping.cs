using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Mapping cho Thích Bình Luận
    /// </summary>
    public class ThichBinhLuanMapping : Profile
    {
        public ThichBinhLuanMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== THÍCH BÌNH LUẬN =====
            CreateMap<ThichBinhLuanRequest, ThichBinhLuan>()
                .ForMember(d => d.MaBinhLuan,
                    o => o.MapFrom(s => s.MaBinhLuan))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // lấy từ context
                .ForMember(d => d.MaNguoiDung,
                    o => o.Ignore())
                .ForMember(d => d.MaBinhLuanNavigation,
                    o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<ThichBinhLuan, NguoiThichBinhLuanResponse>()
                .ForMember(d => d.MaBinhLuan,
                    o => o.MapFrom(s => s.MaBinhLuan))
                .ForMember(d => d.MaNguoiDung,
                    o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // navigation
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());
        }

        #endregion
    }
}
