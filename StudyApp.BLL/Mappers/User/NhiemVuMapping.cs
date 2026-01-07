using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using StudyApp.BLL.Mappers;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Nhiệm vụ & Tiến độ nhiệm vụ
    /// </summary>
    public class NhiemVuMapping : Profile
    {
        public NhiemVuMapping()
        {
            MapEntityToResponse();
            MapRequestToEntity();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== NHIỆM VỤ CƠ BẢN =====
            CreateMap<NhiemVu, NhiemVuResponse>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnumNullable<LoaiNhiemVuEnum>(s.LoaiNhiemVu)))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnumNullable<LoaiDieuKienEnum>(s.LoaiDieuKien)));

            // ===== TIẾN ĐỘ + NHIỆM VỤ =====
            CreateMap<TienDoNhiemVu, NhiemVuVoiTienDoResponse>()
                .ForMember(d => d.MaNhiemVu,
                    o => o.MapFrom(s => s.MaNhiemVu))
                .ForMember(d => d.TenNhiemVu,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.TenNhiemVu))
                .ForMember(d => d.BieuTuong,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.BieuTuong))
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnumNullable<LoaiNhiemVuEnum>(
                            s.MaNhiemVuNavigation.LoaiNhiemVu)))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnumNullable<LoaiDieuKienEnum>(
                            s.MaNhiemVuNavigation.LoaiDieuKien)))
                .ForMember(d => d.DieuKienDatDuoc,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.DieuKienDatDuoc))
                .ForMember(d => d.ThuongVang,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongVang))
                .ForMember(d => d.ThuongKimCuong,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongKimCuong))
                .ForMember(d => d.ThuongXp,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongXp))
                .ForMember(d => d.NgayBatDauThamGia,
                    o => o.MapFrom(s => s.NgayBatDau));

            // ===== TIẾN ĐỘ RIÊNG =====
            CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
                .ForMember(d => d.TenNhiemVu,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.TenNhiemVu))
                .ForMember(d => d.DieuKienDatDuoc,
                    o => o.MapFrom(s => s.MaNhiemVuNavigation.DieuKienDatDuoc));
        }

        #endregion

        #region Request → Entity (Admin)

        private void MapRequestToEntity()
        {
            // ===== TẠO NHIỆM VỤ =====
            CreateMap<TaoNhiemVuRequest, NhiemVu>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s => s.LoaiNhiemVu))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s => s.LoaiDieuKien));

            // ===== CẬP NHẬT NHIỆM VỤ (PATCH) =====
            CreateMap<CapNhatNhiemVuRequest, NhiemVu>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s => s.LoaiNhiemVu.ToString()))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s => s.LoaiDieuKien.ToString()))
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion
    }
}
