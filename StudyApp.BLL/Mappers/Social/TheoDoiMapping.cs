using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Mapping cho Theo Dõi (Follow)
    /// </summary>
    public class TheoDoiMapping : Profile
    {
        public TheoDoiMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== THEO DÕI =====
            CreateMap<TheoDoiRequest, TheoDoi>()
                .ForMember(d => d.MaNguoiDuocTheoDoi,
                    o => o.MapFrom(s => s.MaNguoiDuocTheoDoi))
                .ForMember(d => d.ThongBaoBaiMoi,
                    o => o.MapFrom(s => s.ThongBaoBaiMoi))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // lấy từ context
                .ForMember(d => d.MaNguoiTheoDoi,
                    o => o.Ignore());

            // ===== CẬP NHẬT CÀI ĐẶT =====
            CreateMap<CapNhatCaiDatTheoDoiRequest, TheoDoi>()
                .ForMember(d => d.ThongBaoBaiMoi,
                    o => o.MapFrom(s => s.ThongBaoBaiMoi))

                // không cho sửa
                .ForMember(d => d.MaNguoiTheoDoi,
                    o => o.Ignore())
                .ForMember(d => d.MaNguoiDuocTheoDoi,
                    o => o.Ignore())
                .ForMember(d => d.ThoiGian,
                    o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<TheoDoi, TheoDoiResponse>()
                // xác định ở Service (tùy đang xem danh sách gì)
                .ForMember(d => d.MaNguoiDung,
                    o => o.Ignore())
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))
                .ForMember(d => d.ThongBaoBaiMoi,
                    o => o.MapFrom(s => s.ThongBaoBaiMoi ?? false))

                // user-context
                .ForMember(d => d.DangTheoDoi,
                    o => o.Ignore())
                .ForMember(d => d.DuocTheoDoi,
                    o => o.Ignore())

                // navigation
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());
        }

        #endregion
    }
}
