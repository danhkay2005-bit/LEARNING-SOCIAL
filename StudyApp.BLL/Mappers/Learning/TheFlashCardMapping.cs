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
    /// Mapping cho Thẻ Flashcard
    /// </summary>
    public class TheFlashcardMapping : Profile
    {
        public TheFlashcardMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // TẠO THẺ
            // =====================================================
            CreateMap<TaoTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiThe,
                    opt => opt.MapFrom(src => src.LoaiThe.ToString()))
                .ForMember(dest => dest.DoKho,
                    opt => opt.MapFrom(src =>
                        src.DoKho.HasValue ? (byte?)src.DoKho.Value : null))
                .ForMember(dest => dest.SoLuotHoc, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLanDung, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLanSai, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.TyLeDungTb, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ChiTietTraLois, opt => opt.Ignore())
                .ForMember(dest => dest.DanhDauThes, opt => opt.Ignore())
                .ForMember(dest => dest.GhiChuThes, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHocTaps, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // CẬP NHẬT THẺ
            // =====================================================
            CreateMap<CapNhatTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.LoaiThe,
                    opt => opt.MapFrom(src =>
                        src.LoaiThe.HasValue ? src.LoaiThe.Value.ToString() : null))
                .ForMember(dest => dest.DoKho,
                    opt => opt.MapFrom(src =>
                        src.DoKho.HasValue ? (byte?)src.DoKho.Value : null))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // THẺ CHI TIẾT
            // =====================================================
            CreateMap<TheFlashcard, TheFlashcardResponse>()
                .ForMember(dest => dest.LoaiThe,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiTheEnum>(src.LoaiThe)))
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForMember(dest => dest.DoKho,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKho)))
                .ForMember(dest => dest.SoLuotHoc,
                    opt => opt.MapFrom(src => src.SoLuotHoc ?? 0))
                .ForMember(dest => dest.SoLanDung,
                    opt => opt.MapFrom(src => src.SoLanDung ?? 0))
                .ForMember(dest => dest.SoLanSai,
                    opt => opt.MapFrom(src => src.SoLanSai ?? 0))
                .ForMember(dest => dest.TyLeDungTb,
                    opt => opt.MapFrom(src => src.TyLeDungTb ?? 0))
                // dữ liệu người dùng hiện tại → service set
                .ForMember(dest => dest.TienDoCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.DanhDauCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.GhiChuCuaToi, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // THẺ TÓM TẮT
            // =====================================================
            CreateMap<TheFlashcard, TheFlashcardTomTatResponse>()
                .ForMember(dest => dest.LoaiThe,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiTheEnum>(src.LoaiThe)))
                .ForMember(dest => dest.MatTruocRutGon,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.Truncate(src.MatTruoc, 50)))
                .ForMember(dest => dest.MatSauRutGon,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.Truncate(src.MatSau, 50)))
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForMember(dest => dest.DoKho,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKho)))
                .ForMember(dest => dest.TrangThaiSRS, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // THẺ ĐỂ HỌC
            // =====================================================
            CreateMap<TheFlashcard, TheHocResponse>()
                .ForMember(dest => dest.LoaiThe,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiTheEnum>(src.LoaiThe)))
                .ForMember(dest => dest.DoKho,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKho)))
                // Dữ liệu học → service set
                .ForMember(dest => dest.LaTheMoi, opt => opt.Ignore())
                .ForMember(dest => dest.SoLanDaHoc, opt => opt.Ignore())
                .ForMember(dest => dest.LanHocCuoi, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // KẾT QUẢ TRẢ LỜI
            // =====================================================
            CreateMap<TheFlashcard, TheKetQuaResponse>()
                .ForMember(dest => dest.DapAnDungDayDu,
                    opt => opt.MapFrom(src => src.MatSau))
                .ForMember(dest => dest.GiaiThich,
                    opt => opt.MapFrom(src => src.GiaiThich))
                .ForMember(dest => dest.TrangThaiMoi, opt => opt.Ignore())
                .ForMember(dest => dest.NgayOnTapTiepTheo, opt => opt.Ignore())
                .ForMember(dest => dest.KhoangCachNgayMoi, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // SAU TẠO / CẬP NHẬT
            // =====================================================
            CreateMap<TheFlashcard, TaoTheResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.The, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DANH SÁCH
            // =====================================================
            CreateMap<List<TheFlashcard>, DanhSachTheResponse>()
                .ForMember(dest => dest.Thes,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore());
        }
    }
}
