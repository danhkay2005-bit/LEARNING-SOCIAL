using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers.Social
{
    /// <summary>
    /// AutoMapper cho Chặn Người Dùng
    /// </summary>
    public class ChanNguoiDungMapping : Profile
    {
        public ChanNguoiDungMapping()
        {
            MapEntityToResponse();
        }

        private void MapEntityToResponse()
        {
            CreateMap<ChanNguoiDung, NguoiBiChanResponse>()
                .ForMember(d => d.MaNguoiBiChan,
                    o => o.MapFrom(s => s.MaNguoiBiChan))
                .ForMember(d => d.LyDo,
                    o => o.MapFrom(s => s.LyDo))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // navigation
                .ForMember(d => d.NguoiBiChan,
                    o => o.Ignore());
        }
    }
}
