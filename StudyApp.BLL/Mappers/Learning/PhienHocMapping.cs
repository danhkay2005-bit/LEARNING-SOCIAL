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
    /// Mapping cho Phiên học (PhienHoc)
    /// </summary>
    public class PhienHocMapping : Profile
    {
        public PhienHocMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Bắt đầu phiên học
            // =====================================================
            CreateMap<BatDauPhienHocRequest, PhienHoc>()
                .ForMember(dest => dest.MaPhien, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // từ context
                .ForMember(dest => dest.LoaiPhien,
                    opt => opt.MapFrom(src => src.LoaiPhien.ToString()))
                .ForMember(dest => dest.ThoiGianBatDau,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianHocGiay, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoThe, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheMoi, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheOnTap, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheDung, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheSai, opt => opt.Ignore())
                .ForMember(dest => dest.SoTheBo, opt => opt.Ignore())
                .ForMember(dest => dest.DiemDat, opt => opt.Ignore())
                .ForMember(dest => dest.DiemToiDa, opt => opt.Ignore())
                .ForMember(dest => dest.TyLeDung, opt => opt.Ignore())
                .ForMember(dest => dest.Xpnhan, opt => opt.Ignore())
                .ForMember(dest => dest.VangNhan, opt => opt.Ignore())
                .ForMember(dest => dest.CamXuc, opt => opt.Ignore())
                .ForMember(dest => dest.GhiChu, opt => opt.Ignore())
                .ForMember(dest => dest.ChiTietTraLois, opt => opt.Ignore())
                .ForMember(dest => dest.LichSuHocBoDes, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // Kết thúc phiên học
            // =====================================================
            CreateMap<KetThucPhienHocRequest, PhienHoc>()
                .ForMember(dest => dest.CamXuc,
                    opt => opt.MapFrom(src =>
                        src.CamXuc.HasValue ? (byte?)src.CamXuc.Value : null))
                .ForMember(dest => dest.GhiChu, opt => opt.MapFrom(src => src.GhiChu))
                .ForMember(dest => dest.ThoiGianKetThuc,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // Phiên học chi tiết
            // =====================================================
            CreateMap<PhienHoc, PhienHocResponse>()
                .ForMember(dest => dest.BoDe,
                    opt => opt.MapFrom(src => src.MaBoDeNavigation))
                .ForMember(dest => dest.LoaiPhien,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiPhienHocEnum>(src.LoaiPhien)))
                .ForMember(dest => dest.ThoiGianHocGiay,
                    opt => opt.MapFrom(src => src.ThoiGianHocGiay ?? 0))
                .ForMember(dest => dest.ThoiGianHocHienThi,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.FormatDuration(src.ThoiGianHocGiay)))
                .ForMember(dest => dest.TongSoThe,
                    opt => opt.MapFrom(src => src.TongSoThe ?? 0))
                .ForMember(dest => dest.SoTheMoi,
                    opt => opt.MapFrom(src => src.SoTheMoi ?? 0))
                .ForMember(dest => dest.SoTheOnTap,
                    opt => opt.MapFrom(src => src.SoTheOnTap ?? 0))
                .ForMember(dest => dest.SoTheDung,
                    opt => opt.MapFrom(src => src.SoTheDung ?? 0))
                .ForMember(dest => dest.SoTheSai,
                    opt => opt.MapFrom(src => src.SoTheSai ?? 0))
                .ForMember(dest => dest.SoTheBo,
                    opt => opt.MapFrom(src => src.SoTheBo ?? 0))
                .ForMember(dest => dest.DiemDat,
                    opt => opt.MapFrom(src => src.DiemDat ?? 0))
                .ForMember(dest => dest.DiemToiDa,
                    opt => opt.MapFrom(src => src.DiemToiDa ?? 0))
                .ForMember(dest => dest.TyLeDung,
                    opt => opt.MapFrom(src => src.TyLeDung ?? 0))
                .ForMember(dest => dest.XpNhan,
                    opt => opt.MapFrom(src => src.Xpnhan ?? 0))
                .ForMember(dest => dest.VangNhan,
                    opt => opt.MapFrom(src => src.VangNhan ?? 0))
                .ForMember(dest => dest.CamXuc,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<CamXucHocEnum>(src.CamXuc)))
                .ForMember(dest => dest.ThongKe, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // Phiên học tóm tắt
            // =====================================================
            CreateMap<PhienHoc, PhienHocTomTatResponse>()
                .ForMember(dest => dest.BoDe,
                    opt => opt.MapFrom(src => src.MaBoDeNavigation))
                .ForMember(dest => dest.LoaiPhien,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiPhienHocEnum>(src.LoaiPhien)))
                .ForMember(dest => dest.ThoiGianHocGiay,
                    opt => opt.MapFrom(src => src.ThoiGianHocGiay ?? 0))
                .ForMember(dest => dest.TongSoThe,
                    opt => opt.MapFrom(src => src.TongSoThe ?? 0))
                .ForMember(dest => dest.SoTheDung,
                    opt => opt.MapFrom(src => src.SoTheDung ?? 0))
                .ForMember(dest => dest.TyLeDung,
                    opt => opt.MapFrom(src => src.TyLeDung ?? 0))
                .ForMember(dest => dest.XpNhan,
                    opt => opt.MapFrom(src => src.Xpnhan ?? 0));

            // =====================================================
            // ENTITY -> RESPONSE
            // Kết quả kết thúc phiên học
            // =====================================================
            CreateMap<PhienHoc, KetThucPhienHocResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.PhienHoc, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.PhanThuong, opt => opt.Ignore())
                .ForMember(dest => dest.ThanhTuuMoKhoa, opt => opt.Ignore())
                .ForMember(dest => dest.DatMucTieuNgay, opt => opt.Ignore())
                .ForMember(dest => dest.ChuoiNgayHienTai, opt => opt.Ignore());

            // =====================================================
            // LIST ENTITY -> DanhSachPhienHocResponse
            // =====================================================
            CreateMap<List<PhienHoc>, DanhSachPhienHocResponse>()
                .ForMember(dest => dest.PhienHocs,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore());
        }
    }
}
