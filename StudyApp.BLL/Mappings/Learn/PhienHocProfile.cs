using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class PhienHocProfile : Profile
    {
        public PhienHocProfile()
        {
            // =================================================
            // START SESSION (BatDauPhienHocRequest -> Entity)
            // =================================================
            CreateMap<BatDauPhienHocRequest, PhienHoc>()
                .ForMember(dest => dest.MaPhien, opt => opt.Ignore())                // DB tự sinh
                .ForMember(dest => dest.LoaiPhien,
                           opt => opt.MapFrom(src => src.LoaiPhien.ToString()))     // Enum -> string
                .ForMember(dest => dest.ThoiGianBatDau,
                           opt => opt.MapFrom(_ => DateTime.Now))                   // Server set
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianHocGiay, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoThe, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheMoi, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheOnTap, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheSai, opt => opt.Ignore())
                .ForMember(dest => dest.TyLeDung, opt => opt.Ignore())
                .ForMember(dest => dest.ChiTietTraLois, opt => opt.Ignore())
                .ForMember(dest => dest.LichSuHocBoDes, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThachDauNavigation, opt => opt.Ignore());

            // =================================================
            // END SESSION (KetThucPhienHocRequest -> Entity)
            // =================================================
            CreateMap<KetThucPhienHocRequest, PhienHoc>()
                .ForMember(dest => dest.ThoiGianKetThuc,
                           opt => opt.MapFrom(_ => DateTime.Now))                   // Server set
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.MaThachDau, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiPhien, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianBatDau, opt => opt.Ignore())
                .ForMember(dest => dest.ChiTietTraLois, opt => opt.Ignore())
                .ForMember(dest => dest.LichSuHocBoDes, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThachDauNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<PhienHoc, PhienHocResponse>()
                .ForMember(dest => dest.LoaiPhien,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<LoaiPhienHocEnum>(src.LoaiPhien ?? "HocMoi")));

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<PhienHoc, PhienHocSummaryResponse>()
                .ForMember(dest => dest.LoaiPhien,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<LoaiPhienHocEnum>(src.LoaiPhien ?? "HocMoi")));
        }
    }
}
