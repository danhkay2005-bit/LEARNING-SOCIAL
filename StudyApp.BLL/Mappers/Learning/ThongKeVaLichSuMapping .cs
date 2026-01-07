using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Responses.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho THỐNG KÊ + LỊCH SỬ HỌC TẬP
    /// </summary>
    public class ThongKeVaLichSuMapping : Profile
    {
        public ThongKeVaLichSuMapping()
        {
            // =====================================================
            // I. THỐNG KÊ NGÀY
            // =====================================================
            CreateMap<ThongKeNgay, ThongKeNgayResponse>()
                .ForMember(d => d.TongTheHoc, o => o.MapFrom(s => s.TongTheHoc ?? 0))
                .ForMember(d => d.SoTheMoi, o => o.MapFrom(s => s.SoTheMoi ?? 0))
                .ForMember(d => d.SoTheOnTap, o => o.MapFrom(s => s.SoTheOnTap ?? 0))
                .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
                .ForMember(d => d.SoTheSai, o => o.MapFrom(s => s.SoTheSai ?? 0))
                .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
                .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
                .ForMember(d => d.SoPhienHoc, o => o.MapFrom(s => s.SoPhienHoc ?? 0))
                .ForMember(d => d.SoBoDeHoc, o => o.MapFrom(s => s.SoBoDeHoc ?? 0))
                .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
                .ForMember(d => d.VangNhan, o => o.MapFrom(s => s.VangNhan ?? 0))
                .ForMember(d => d.DaHoanThanhMucTieu, o => o.MapFrom(s => s.DaHoanThanhMucTieu ?? false));

            CreateMap<List<ThongKeNgay>, DanhSachThongKeNgayResponse>()
                .ForMember(d => d.ThongKes, o => o.MapFrom(s => s))
                .ForMember(d => d.TongHop, o => o.Ignore());

            CreateMap<List<ThongKeNgay>, ThongKeTongHopResponse>()
                .ForMember(d => d.TongSoTheHoc, o => o.MapFrom(s => s.Sum(x => x.TongTheHoc ?? 0)))
                .ForMember(d => d.TongThoiGianHocPhut, o => o.MapFrom(s => s.Sum(x => x.ThoiGianHocPhut ?? 0)))
                .ForMember(d => d.TongSoPhien, o => o.MapFrom(s => s.Sum(x => x.SoPhienHoc ?? 0)))
                .ForMember(d => d.TongSoBoDeHoc, o => o.MapFrom(s => s.Sum(x => x.SoBoDeHoc ?? 0)))
                .ForMember(d => d.TyLeDungTrungBinh,
                    o => o.MapFrom(s => s.Any() ? Math.Round(s.Average(x => x.TyLeDung ?? 0), 2) : 0))
                .ForMember(d => d.TongXpNhan, o => o.MapFrom(s => s.Sum(x => x.Xpnhan ?? 0)))
                .ForMember(d => d.TongVangNhan, o => o.MapFrom(s => s.Sum(x => x.VangNhan ?? 0)))
                .ForMember(d => d.SoNgayHoc, o => o.MapFrom(s => s.Count))
                .ForMember(d => d.TrungBinhTheMoiNgay,
                    o => o.MapFrom(s => s.Any() ? Math.Round(s.Average(x => x.TongTheHoc ?? 0), 2) : 0))
                .ForMember(d => d.TrungBinhPhutMoiNgay,
                    o => o.MapFrom(s => s.Any() ? Math.Round(s.Average(x => x.ThoiGianHocPhut ?? 0), 2) : 0));

            // =====================================================
            // II. LỊCH SỬ HỌC BỘ ĐỀ
            // =====================================================
            CreateMap<LichSuHocBoDe, LichSuHocBoDeResponse>()
                .ForMember(d => d.SoTheHoc, o => o.MapFrom(s => s.SoTheHoc ?? 0))
                .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
                .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
                .ForMember(d => d.BoDe, o => o.Ignore()); // service map

            CreateMap<List<LichSuHocBoDe>, DanhSachLichSuHocResponse>()
                .ForMember(d => d.LichSus, o => o.MapFrom(s => s))
                .ForMember(d => d.TongSo, o => o.MapFrom(s => s.Count))
                .ForMember(d => d.TrangHienTai, o => o.Ignore())
                .ForMember(d => d.TongSoTrang, o => o.Ignore());

            // =====================================================
            // III. LỊCH SỬ CLONE BỘ ĐỀ
            // =====================================================
            CreateMap<LichSuClone, LichSuCloneResponse>()
                .ForMember(d => d.BoDeGoc, o => o.Ignore())
                .ForMember(d => d.BoDeClone, o => o.Ignore())
                .ForMember(d => d.NguoiClone, o => o.Ignore());
        }
    }
}
    