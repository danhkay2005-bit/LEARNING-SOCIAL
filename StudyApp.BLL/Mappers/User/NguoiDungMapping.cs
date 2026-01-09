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
            CreateMap<NguoiDung, NguoiDungResponse>()
                .ForMember(d => d.GioiTinh,
                    o => o.MapFrom(s =>
                        MappingHelpers.ByteToEnum<GioiTinhEnum>(s.GioiTinh)))
                .ForMember(d => d.VaiTro,
                    o => o.MapFrom(s => s.MaVaiTroNavigation))
                .ForMember(d => d.CapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation));

            CreateMap<NguoiDung, NguoiDungChiTietResponse>()
                .IncludeBase<NguoiDung, NguoiDungResponse>();

            CreateMap<NguoiDung, NguoiDungTomTatResponse>()
                .ForMember(d => d.TenCapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation!.TenCapDo));

            CreateMap<NguoiDung, BangXepHangNguoiDungResponse>()
                .ForMember(d => d.TenCapDo,
                    o => o.MapFrom(s => s.MaCapDoNavigation!.TenCapDo));
        }

        #endregion

        #region Request → Entity (có kiểm soát)

        private void MapRequestToEntity()
        {
            // ===== ĐĂNG KÝ =====
            CreateMap<DangKyNguoiDungRequest, NguoiDung>()
                .ForMember(d => d.TenDangNhap, o => o.MapFrom(s => s.TenDangNhap))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.SoDienThoai, o => o.MapFrom(s => s.SoDienThoai))
                .ForMember(d => d.HoVaTen, o => o.MapFrom(s => s.HoVaTen))
                // MatKhauMaHoa sẽ set ở Service (SHA256)
                .ForMember(d => d.MatKhauMaHoa, o => o.Ignore())
                // để DB default xử lý
                .ForMember(d => d.MaVaiTro, o => o.Ignore())
                .ForMember(d => d.MaCapDo, o => o.Ignore())
                .ForMember(d => d.Vang, o => o.Ignore())
                .ForMember(d => d.KimCuong, o => o.Ignore())
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

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
