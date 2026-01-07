using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using System;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Cấp Độ (Level)
    /// </summary>
    public class CapDoMapping : Profile
    {
        public CapDoMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== TẠO CẤP ĐỘ =====
            CreateMap<TaoCapDoRequest, CapDo>()
                .ForMember(d => d.TenCapDo, o => o.MapFrom(s => s.TenCapDo))
                .ForMember(d => d.BieuTuong, o => o.MapFrom(s => s.BieuTuong))
                .ForMember(d => d.MucXptoiThieu, o => o.MapFrom(s => s.MucXptoiThieu))
                .ForMember(d => d.MucXptoiDa, o => o.MapFrom(s => s.MucXptoiDa))
                .ForMember(d => d.ThoiGianTao,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // không mapper
                .ForMember(d => d.MaCapDo, o => o.Ignore())
                .ForMember(d => d.NguoiDungs, o => o.Ignore());

            // ===== CẬP NHẬT CẤP ĐỘ =====
            CreateMap<CapNhatCapDoRequest, CapDo>()
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<CapDo, CapDoResponse>();
            // mapping 1–1 → AutoMapper tự xử lý
        }

        #endregion
    }
}
