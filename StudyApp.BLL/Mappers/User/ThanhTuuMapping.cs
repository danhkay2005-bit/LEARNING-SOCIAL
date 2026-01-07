using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using StudyApp.BLL.Mappers;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Thành tựu & Thành tựu đạt được
    /// </summary>
    public class ThanhTuuMapping : Profile
    {
        public ThanhTuuMapping()
        {
            MapEntityToResponse();
            MapRequestToEntity();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== THÀNH TỰU TÓM TẮT =====
            CreateMap<ThanhTuu, ThanhTuuTomTatResponse>();

            // ===== THÀNH TỰU ĐẠT ĐƯỢC (dùng khi gắn profile / danh sách) =====
            CreateMap<ThanhTuuDatDuoc, ThanhTuuTomTatResponse>()
                .ForMember(d => d.MaThanhTuu,
                    o => o.MapFrom(s => s.MaThanhTuu))
                .ForMember(d => d.TenThanhTuu,
                    o => o.MapFrom(s => s.MaThanhTuuNavigation.TenThanhTuu))
                .ForMember(d => d.BieuTuong,
                    o => o.MapFrom(s => s.MaThanhTuuNavigation.BieuTuong))
                .ForMember(d => d.HinhHuy,
                    o => o.MapFrom(s => s.MaThanhTuuNavigation.HinhHuy))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => s.MaThanhTuuNavigation.DoHiem));
        }

        #endregion

        #region Request → Entity (Admin)

        private void MapRequestToEntity()
        {
            // ===== TẠO THÀNH TỰU =====
            CreateMap<TaoThanhTuuRequest, ThanhTuu>()
                .ForMember(d => d.LoaiThanhTuu,
                    o => o.MapFrom(s => s.LoaiThanhTuu.ToString()))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => s.DieuKienLoai != null
                        ? s.DieuKienLoai.ToString()
                        : null));

            // ===== CẬP NHẬT THÀNH TỰU (PATCH) =====
            CreateMap<CapNhatThanhTuuRequest, ThanhTuu>()
                .ForMember(d => d.LoaiThanhTuu,
                    o => o.MapFrom(s => s.LoaiThanhTuu != null
                        ? s.LoaiThanhTuu.ToString()
                        : null))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => s.DieuKienLoai != null
                        ? s.DieuKienLoai.ToString()
                        : null))
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion
    }
}
