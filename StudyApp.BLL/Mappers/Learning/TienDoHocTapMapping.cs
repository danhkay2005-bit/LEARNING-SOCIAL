using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Tiến độ học tập (SRS)
    /// </summary>
    public class TienDoHocTapMapping : Profile
    {
        public TienDoHocTapMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Cập nhật tiến độ (SM-2)
            // =====================================================
            CreateMap<CapNhatTienDoRequest, TienDoHocTap>()
                .ForMember(dest => dest.MaTienDo, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // từ context
                .ForMember(dest => dest.TrangThai, opt => opt.Ignore())   // service set
                .ForMember(dest => dest.HeSoDe, opt => opt.Ignore())
                .ForMember(dest => dest.KhoangCachNgay, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanLap, opt => opt.Ignore())
                .ForMember(dest => dest.NgayOnTapTiepTheo, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanSai, opt => opt.Ignore())
                .ForMember(dest => dest.TyLeDung, opt => opt.Ignore())
                .ForMember(dest => dest.DoKhoCanNhan, opt => opt.Ignore())
                .ForMember(dest => dest.LanHocCuoi,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianTraLoiTbgiay, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // Tiến độ thẻ chi tiết
            // =====================================================
            CreateMap<TienDoHocTap, TienDoTheResponse>()
                .ForMember(dest => dest.TrangThai,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<TrangThaiSRSEnum>(src.TrangThai)
                        ?? TrangThaiSRSEnum.New))
                .ForMember(dest => dest.HeSoDe,
                    opt => opt.MapFrom(src => src.HeSoDe ?? 2.5))
                .ForMember(dest => dest.KhoangCachNgay,
                    opt => opt.MapFrom(src => src.KhoangCachNgay ?? 0))
                .ForMember(dest => dest.SoLanLap,
                    opt => opt.MapFrom(src => src.SoLanLap ?? 0))
                .ForMember(dest => dest.SoLanDung,
                    opt => opt.MapFrom(src => src.SoLanDung ?? 0))
                .ForMember(dest => dest.SoLanSai,
                    opt => opt.MapFrom(src => src.SoLanSai ?? 0))
                .ForMember(dest => dest.TyLeDung,
                    opt => opt.MapFrom(src => src.TyLeDung ?? 0))
                .ForMember(dest => dest.ThoiGianTraLoiTbGiay,
                    opt => opt.MapFrom(src => src.ThoiGianTraLoiTbgiay ?? 0))
                .ForMember(dest => dest.DoKhoCanNhan,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKhoCanNhan)));

            // =====================================================
            // ENTITY -> RESPONSE
            // Kết quả cập nhật tiến độ
            // =====================================================
            CreateMap<TienDoHocTap, CapNhatTienDoResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.TrangThaiMoi,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<TrangThaiSRSEnum>(src.TrangThai)
                        ?? TrangThaiSRSEnum.New))
                .ForMember(dest => dest.KhoangCachNgayMoi,
                    opt => opt.MapFrom(src => src.KhoangCachNgay ?? 0))
                .ForMember(dest => dest.HeSoDeMoi,
                    opt => opt.MapFrom(src => src.HeSoDe ?? 2.5));

            // =====================================================
            // LIST ENTITY -> TỔNG QUAN TIẾN ĐỘ
            // =====================================================
            CreateMap<List<TienDoHocTap>, TongQuanTienDoResponse>()
                .ForMember(dest => dest.TongSoThe,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SoTheMoi,
                    opt => opt.MapFrom(src =>
                        src.Count(x => x.TrangThai == (byte)TrangThaiSRSEnum.New)))
                .ForMember(dest => dest.SoTheDangHoc,
                    opt => opt.MapFrom(src =>
                        src.Count(x => x.TrangThai == (byte)TrangThaiSRSEnum.Learning)))
                .ForMember(dest => dest.SoTheCanOnTap,
                    opt => opt.MapFrom(src =>
                        src.Count(x =>
                            x.NgayOnTapTiepTheo.HasValue &&
                            x.NgayOnTapTiepTheo.Value <= DateTime.UtcNow)))
                .ForMember(dest => dest.SoTheThanhThao,
                    opt => opt.MapFrom(src =>
                        src.Count(x => x.TrangThai == (byte)TrangThaiSRSEnum.Mastered)))
                .ForMember(dest => dest.TyLeDungTrungBinh,
                    opt => opt.MapFrom(src =>
                        src.Any()
                            ? Math.Round(src.Average(x => x.TyLeDung ?? 0), 2)
                            : 0))
                .ForMember(dest => dest.ChuoiNgayHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.ChuoiNgayCaoNhat, opt => opt.Ignore())
                .ForMember(dest => dest.ThongKeTheoTrangThai,
                    opt => opt.MapFrom(src =>
                        src.GroupBy(x =>
                                MappingHelpers.ByteToEnum<TrangThaiSRSEnum>(x.TrangThai)
                                ?? TrangThaiSRSEnum.New)
                           .ToDictionary(g => g.Key, g => g.Count())));
        }
    }
}
