using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TienDoHocTapProfile : Profile
    {
        public TienDoHocTapProfile()
        {
            // =================================================
            // CREATE (TaoTienDoHocTapRequest -> Entity)
            // =================================================
            CreateMap<TaoTienDoHocTapRequest, TienDoHocTap>()
                .ForMember(dest => dest.MaTienDo, opt => opt.Ignore())              // DB tự sinh
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src => (byte)src.TrangThai))         // Enum -> byte
                .ForMember(dest => dest.HeSoDe, opt => opt.Ignore())
                .ForMember(dest => dest.KhoangCachNgay, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanLap, opt => opt.Ignore())
                .ForMember(dest => dest.NgayOnTapTiepTheo, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanDung, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLanSai, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.LanHocCuoi, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatTienDoHocTapRequest -> Entity)
            // =================================================
            CreateMap<CapNhatTienDoHocTapRequest, TienDoHocTap>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               src.TrangThai.HasValue ? (byte?)src.TrangThai.Value : null))
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<TienDoHocTap, TienDoHocTapResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               (TrangThaiHocEnum)(src.TrangThai ?? (byte)TrangThaiHocEnum.New)));

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<TienDoHocTap, TienDoHocTapSummaryResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               (TrangThaiHocEnum)(src.TrangThai ?? (byte)TrangThaiHocEnum.New)));
        }
    }
}
