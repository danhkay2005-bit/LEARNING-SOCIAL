using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Tùy chỉnh Profile
    /// </summary>
    public class TuyChinhProfileMapping : Profile
    {
        public TuyChinhProfileMapping()
        {
            MapEntityToResponse();
            MapRequestToEntity();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<TuyChinhProfile, TuyChinhProfileResponse>()
                .ForMember(d => d.AvatarDangDung,
                    o => o.MapFrom(s => s.MaAvatarDangDungNavigation))
                .ForMember(d => d.KhungDangDung,
                    o => o.MapFrom(s => s.MaKhungDangDungNavigation))
                .ForMember(d => d.HinhNenDangDung,
                    o => o.MapFrom(s => s.MaHinhNenDangDungNavigation))
                .ForMember(d => d.HieuUngDangDung,
                    o => o.MapFrom(s => s.MaHieuUngDangDungNavigation))
                .ForMember(d => d.ThemeDangDung,
                    o => o.MapFrom(s => s.MaThemeDangDungNavigation))
                .ForMember(d => d.NhacNenDangDung,
                    o => o.MapFrom(s => s.MaNhacNenDangDungNavigation))
                .ForMember(d => d.BadgeDangDung,
                    o => o.MapFrom(s => s.MaBadgeDangDungNavigation))
                .ForMember(d => d.HuyHieuHienThi,
                    o => o.MapFrom(s => s.MaHuyHieuHienThiNavigation));
        }

        #endregion

        #region Request → Entity (PATCH)

        private void MapRequestToEntity()
        {
            CreateMap<CapNhatTuyChinhProfileRequest, TuyChinhProfile>()
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion
    }
}
