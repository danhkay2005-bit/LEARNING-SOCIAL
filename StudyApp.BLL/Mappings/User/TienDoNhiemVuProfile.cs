using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class TienDoNhiemVuProfile : Profile
    {
        public TienDoNhiemVuProfile()
        {
            CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
                .ForMember(d => d.TienDoHienTai, o => o.MapFrom(s => s.TienDoHienTai ?? 0))
                .ForMember(d => d.DaHoanThanh, o => o.MapFrom(s => s.DaHoanThanh ?? false))
                .ForMember(d => d.DaNhanThuong, o => o.MapFrom(s => s.DaNhanThuong ?? false))
                
                .ForMember(d => d.MaNhiemVu, o => o.MapFrom(s => s.MaNhiemVuNavigation.MaNhiemVu))
                .ForMember(d => d.TenNhiemVu, o => o.MapFrom(s => s.MaNhiemVuNavigation.TenNhiemVu))
                
                .ForMember(d => d.MoTa, o => o.MapFrom(s => s.MaNhiemVuNavigation.MoTa))
                .ForMember(d => d.BieuTuong, o => o.MapFrom(s => s.MaNhiemVuNavigation.BieuTuong))
                .ForMember(d => d.DieuKienDatDuoc, o => o.MapFrom(s => s.MaNhiemVuNavigation.DieuKienDatDuoc))
                .ForMember(d => d.ThuongVang, o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongVang ?? 0))
                .ForMember(d => d.ThuongKimCuong, o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongKimCuong ?? 0))
                .ForMember(d => d.ThuongXP, o => o.MapFrom(s => s.MaNhiemVuNavigation.ThuongXp ?? 0))

                .ForMember(d => d.LoaiNhiemVu, o => o.MapFrom(s => ParseLoaiNhiemVu(s.MaNhiemVuNavigation.LoaiNhiemVu)));
        }

        private static LoaiNhiemVuEnum ParseLoaiNhiemVu(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return LoaiNhiemVuEnum.HangNgay;
            }

            LoaiNhiemVuEnum parsed;
            if (Enum.TryParse(value, ignoreCase: true, out parsed))
            {
                return parsed;
            }

            return LoaiNhiemVuEnum.HangNgay;
        }
    }
}
