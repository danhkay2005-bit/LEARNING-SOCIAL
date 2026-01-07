using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using StudyApp.BLL.Mappers;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Người Dùng
    /// </summary>
    public class NguoiDungMapping : Profile
    {
        public NguoiDungMapping()
        {
            MapEntityToResponse();
            MapRequestToEntity();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== CƠ BẢN =====
            CreateMap<NguoiDung, NguoiDungResponse>()
                .ForMember(d => d.GioiTinh,
                    o => o.MapFrom(s =>
                        MappingHelpers.ByteToEnum<GioiTinhEnum>(s.GioiTinh)))
                .ForMember(d => d.VaiTro,
                    o => o.MapFrom(s => s.MaVaiTroNavigation))
                .ForMember(d => d.CapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation));

            // ===== CHI TIẾT =====
            CreateMap<NguoiDung, NguoiDungChiTietResponse>()
                .IncludeBase<NguoiDung, NguoiDungResponse>();

            // ===== TÓM TẮT =====
            CreateMap<NguoiDung, NguoiDungTomTatResponse>()
                .ForMember(d => d.TenCapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation!.TenCapDo));

            // ===== BẢNG XẾP HẠNG =====
            CreateMap<NguoiDung, BangXepHangNguoiDungResponse>()
                .ForMember(d => d.TenCapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation!.TenCapDo));
        }

        #endregion

        #region Request → Entity (có kiểm soát)

        private void MapRequestToEntity()
        {
            // ===== CẬP NHẬT PROFILE =====
            CreateMap<CapNhatThongTinCaNhanRequest, NguoiDung>()
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            // ===== CẬP NHẬT HÌNH ẢNH =====
            CreateMap<CapNhatHinhAnhProfileRequest, NguoiDung>()
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion
    }
}
