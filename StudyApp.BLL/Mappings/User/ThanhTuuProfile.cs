using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class ThanhTuuProfile : Profile
    {
        public ThanhTuuProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<ThanhTuu, ThanhTuuResponse>()
                .ForMember(d => d.LoaiThanhTuu,
                    o => o.MapFrom(s => ParseLoaiThanhTuu(s.LoaiThanhTuu)))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => ParseLoaiDieuKien(s.DieuKienLoai)))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => ParseDoHiem(s.DoHiem)))
                .ForMember(d => d.BiAn,
                    o => o.MapFrom(s => s.BiAn ?? false))
                .ForMember(d => d.ThuongVang,
                    o => o.MapFrom(s => s.ThuongVang ?? 0))
                .ForMember(d => d.ThuongKimCuong,
                    o => o.MapFrom(s => s.ThuongKimCuong ?? 0))

                .ForMember(d => d.ThuongXp,
                    o => o.MapFrom(s => s.ThuongXp ?? 0))
            .ForMember(d => d.HinhHuy, o => o.MapFrom(s => s.HinhHuy));



            // =========================
            // REQUEST → ENTITY (CREATE)
            // =========================
            CreateMap<TaoThanhTuuRequest, ThanhTuu>()
                .ForMember(d => d.LoaiThanhTuu,
                    o => o.MapFrom(s => s.LoaiThanhTuu.ToString()))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => s.DieuKienLoai.ToString()))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => (byte)s.DoHiem))
                .ForMember(d => d.BiAn,
                    o => o.MapFrom(s => s.BiAn))
                .ForMember(d => d.ThoiGianTao,
                    o => o.MapFrom(_ => DateTime.Now));

            // =========================
            // REQUEST → ENTITY (UPDATE)
            // =========================
            CreateMap<CapNhatThanhTuuRequest, ThanhTuu>()
                .ForMember(d => d.LoaiThanhTuu,
                    o => o.MapFrom(s => s.LoaiThanhTuu.ToString()))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => s.DieuKienLoai.ToString()))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => (byte)s.DoHiem))
                .ForMember(d => d.BiAn,
                    o => o.MapFrom(s => s.BiAn));
        }

        private static LoaiThanhTuuEnum ParseLoaiThanhTuu(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return LoaiThanhTuuEnum.HocTap;
            }

            value = value.Trim();

            LoaiThanhTuuEnum parsed;
            if (Enum.TryParse(value, ignoreCase: true, out parsed))
            {
                return parsed;
            }

            return LoaiThanhTuuEnum.HocTap;
        }

        private static LoaiDieuKienEnum ParseLoaiDieuKien(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return LoaiDieuKienEnum.SoLanDangNhap;
            }

            value = value.Trim();

            if (string.Equals(value, "Login", StringComparison.OrdinalIgnoreCase))
            {
                return LoaiDieuKienEnum.SoLanDangNhap;
            }

            LoaiDieuKienEnum parsed;
            if (Enum.TryParse(value, ignoreCase: true, out parsed))
            {
                return parsed;
            }

            return LoaiDieuKienEnum.SoLanDangNhap;
        }

        private static DoHiemEnum ParseDoHiem(byte? value)
        {
            if (value == null)
            {
                return DoHiemEnum.PhoBien;
            }

            var asInt = (int)value.Value;
            if (asInt < (int)DoHiemEnum.PhoBien)
            {
                return DoHiemEnum.PhoBien;
            }

            if (asInt > (int)DoHiemEnum.HuyenThoai)
            {
                return DoHiemEnum.HuyenThoai;
            }

            return (DoHiemEnum)value.Value;
        }
    }
}
