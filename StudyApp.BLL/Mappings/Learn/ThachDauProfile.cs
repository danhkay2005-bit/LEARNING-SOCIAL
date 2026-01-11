using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class ThachDauProfile : Profile
    {
        public ThachDauProfile()
        {
            // =================================================
            // CREATE (TaoThachDauRequest -> Entity)
            // =================================================
            CreateMap<TaoThachDauRequest, ThachDau>()
                .ForMember(dest => dest.MaThachDau, opt => opt.Ignore())               // DB tự sinh
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(_ => TrangThaiThachDauEnum.ChoNguoiChoi.ToString()))
                .ForMember(dest => dest.ThoiGianTao,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianBatDau, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.PhienHocs, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauNguoiChois, opt => opt.Ignore());

            // =================================================
            // START (BatDauThachDauRequest -> Entity)
            // =================================================
            CreateMap<BatDauThachDauRequest, ThachDau>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(_ => TrangThaiThachDauEnum.DangDau.ToString()))
                .ForMember(dest => dest.ThoiGianBatDau,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.PhienHocs, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauNguoiChois, opt => opt.Ignore());

            // =================================================
            // END (KetThucThachDauRequest -> Entity)
            // =================================================
            CreateMap<KetThucThachDauRequest, ThachDau>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(_ => TrangThaiThachDauEnum.DaKetThuc.ToString()))
                .ForMember(dest => dest.ThoiGianKetThuc,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianBatDau, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.PhienHocs, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauNguoiChois, opt => opt.Ignore());

            // =================================================
            // CANCEL (HuyThachDauRequest -> Entity)
            // =================================================
            CreateMap<HuyThachDauRequest, ThachDau>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(_ => TrangThaiThachDauEnum.Huy.ToString()))
                .ForMember(dest => dest.ThoiGianKetThuc,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianBatDau, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.PhienHocs, opt => opt.Ignore())
                .ForMember(dest => dest.ThachDauNguoiChois, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<ThachDau, ThachDauResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<TrangThaiThachDauEnum>(src.TrangThai ?? "ChoNguoiChoi")));

            // =================================================
            // ENTITY -> RESPONSE (SUMMARY)
            // =================================================
            CreateMap<ThachDau, ThachDauSummaryResponse>()
                .ForMember(dest => dest.TrangThai,
                           opt => opt.MapFrom(src =>
                               Enum.Parse<TrangThaiThachDauEnum>(src.TrangThai ?? "ChoNguoiChoi")));
        }
    }
}
