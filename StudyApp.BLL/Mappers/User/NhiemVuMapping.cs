using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class NhiemVuMapping: Profile
    {
        NhiemVuMapping()
        {
            // NhiemVu -> NhiemVuResponse
            CreateMap<NhiemVu, NhiemVuResponse>()
                .ForMember(dest => dest.LoaiNhiemVu, opt => opt.MapFrom(src => ParseEnum<LoaiNhiemVuEnum>(src.LoaiNhiemVu)))
                .ForMember(dest => dest.LoaiDieuKien, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.LoaiDieuKien)))
                .ForMember(dest => dest.ConHieuLuc, opt => opt.MapFrom(src => src.ConHieuLuc ?? true))
                .ForMember(dest => dest.ThuongVang, opt => opt.MapFrom(src => src.ThuongVang ?? 0))
                .ForMember(dest => dest.ThuongKimCuong, opt => opt.MapFrom(src => src.ThuongKimCuong ?? 0))
                .ForMember(dest => dest.ThuongXp, opt => opt.MapFrom(src => src.ThuongXp ?? 0));

            // NhiemVu -> NhiemVuVoiTienDoResponse
            CreateMap<NhiemVu, NhiemVuVoiTienDoResponse>()
                .ForMember(dest => dest.LoaiNhiemVu, opt => opt.MapFrom(src => ParseEnum<LoaiNhiemVuEnum>(src.LoaiNhiemVu)))
                .ForMember(dest => dest.LoaiDieuKien, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.LoaiDieuKien)))
                .ForMember(dest => dest.ConHieuLuc, opt => opt.MapFrom(src => src.ConHieuLuc ?? true))
                .ForMember(dest => dest.ThuongVang, opt => opt.MapFrom(src => src.ThuongVang ?? 0))
                .ForMember(dest => dest.ThuongKimCuong, opt => opt.MapFrom(src => src.ThuongKimCuong ?? 0))
                .ForMember(dest => dest.ThuongXp, opt => opt.MapFrom(src => src.ThuongXp ?? 0))
                .ForMember(dest => dest.TienDoHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.DaHoanThanh, opt => opt.Ignore())
                .ForMember(dest => dest.DaNhanThuong, opt => opt.Ignore())
                .ForMember(dest => dest.NgayBatDauThamGia, opt => opt.Ignore())
                .ForMember(dest => dest.NgayHoanThanh, opt => opt.Ignore());
            // TienDoNhiemVu -> TienDoNhiemVuResponse
            CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
                .ForMember(dest => dest.TienDoHienTai, opt => opt.MapFrom(src => src.TienDoHienTai ?? 0))
                .ForMember(dest => dest.DaHoanThanh, opt => opt.MapFrom(src => src.DaHoanThanh ?? false))
                .ForMember(dest => dest.DaNhanThuong, opt => opt.MapFrom(src => src.DaNhanThuong ?? false))
                .ForMember(dest => dest.TenNhiemVu, opt => opt.Ignore()) // Map từ Navigation
                .ForMember(dest => dest.DieuKienDatDuoc, opt => opt.Ignore());
        }
    }
}
