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
    /// Mapping cho Mục tiêu cá nhân
    /// </summary>
    public class MucTieuCaNhanMapping : Profile
    {
        public MucTieuCaNhanMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Tạo mục tiêu
            // =====================================================
            CreateMap<TaoMucTieuRequest, MucTieuCaNhan>()
                .ForMember(dest => dest.MaMucTieu, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // từ context
                .ForMember(dest => dest.LoaiMucTieu,
                    opt => opt.MapFrom(src => src.LoaiMucTieu.ToString()))
                .ForMember(dest => dest.GiaTriHienTai, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DaHoanThanh, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.NgayHoanThanh, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.UtcNow));

            // =====================================================
            // REQUEST -> ENTITY
            // Cập nhật mục tiêu
            // =====================================================
            CreateMap<CapNhatMucTieuRequest, MucTieuCaNhan>()
                .ForMember(dest => dest.LoaiMucTieu, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.GiaTriHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.DaHoanThanh, opt => opt.Ignore())
                .ForMember(dest => dest.NgayHoanThanh, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // MucTieuCaNhan -> MucTieuCaNhanResponse
            // =====================================================
            CreateMap<MucTieuCaNhan, MucTieuCaNhanResponse>()
                .ForMember(dest => dest.LoaiMucTieu,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiMucTieuEnum>(src.LoaiMucTieu)))
                .ForMember(dest => dest.GiaTriHienTai,
                    opt => opt.MapFrom(src => src.GiaTriHienTai ?? 0))
                .ForMember(dest => dest.TienDoPhanTram,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.CalculatePercentage(
                            src.GiaTriHienTai ?? 0,
                            src.GiaTriMucTieu)))
                .ForMember(dest => dest.SoNgayConLai,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.CalculateDaysRemaining(src.NgayKetThuc)))
                .ForMember(dest => dest.DaHoanThanh,
                    opt => opt.MapFrom(src => src.DaHoanThanh ?? false));

            // =====================================================
            // ENTITY -> RESPONSE
            // MucTieuCaNhan -> KetQuaMucTieuResponse
            // =====================================================
            CreateMap<MucTieuCaNhan, KetQuaMucTieuResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.MucTieu, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DanhSachMucTieuResponse
            // =====================================================
            CreateMap<List<MucTieuCaNhan>, DanhSachMucTieuResponse>()
                .ForMember(dest => dest.MucTieus,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SoDaHoanThanh,
                    opt => opt.MapFrom(src => src.Count(x => x.DaHoanThanh == true)))
                .ForMember(dest => dest.SoDangThucHien,
                    opt => opt.MapFrom(src => src.Count(x => x.DaHoanThanh != true)))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore());
        }
    }
}
