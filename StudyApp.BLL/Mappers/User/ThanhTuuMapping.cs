using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class ThanhTuuMapping: Profile
    {
        ThanhTuuMapping()
        {
            // ThanhTuu -> ThanhTuuResponse
            CreateMap<ThanhTuu, ThanhTuuResponse>()
                .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => ParseEnum<LoaiThanhTuuEnum>(src.LoaiThanhTuu)))
                .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.DieuKienLoai)))
                .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
                .ForMember(dest => dest.BiAn, opt => opt.MapFrom(src => src.BiAn ?? false));

            // ThanhTuu -> ThanhTuuVoiTrangThaiResponse
            CreateMap<ThanhTuu, ThanhTuuVoiTrangThaiResponse>()
                .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => ParseEnum<LoaiThanhTuuEnum>(src.LoaiThanhTuu)))
                .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.DieuKienLoai)))
                .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
                .ForMember(dest => dest.BiAn, opt => opt.MapFrom(src => src.BiAn ?? false))
                .ForMember(dest => dest.DaDat, opt => opt.Ignore())
                .ForMember(dest => dest.NgayDat, opt => opt.Ignore())
                .ForMember(dest => dest.DaXem, opt => opt.Ignore())
                .ForMember(dest => dest.DaChiaSe, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHienTai, opt => opt.Ignore());

            // TaoThanhTuuRequest -> ThanhTuu (Admin)
            CreateMap<TaoThanhTuuRequest, ThanhTuu>()
                .ForMember(dest => dest.MaThanhTuu, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => src.LoaiThanhTuu.ToString()))
                .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => src.DieuKienLoai.HasValue ? src.DieuKienLoai.Value.ToString() : null))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore());

            // CapNhatThanhTuuRequest -> ThanhTuu (Admin)
            CreateMap<CapNhatThanhTuuRequest, ThanhTuu>()
                .ForMember(dest => dest.MaThanhTuu, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => src.LoaiThanhTuu.HasValue ? src.LoaiThanhTuu.Value.ToString() : null))
                .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => src.DieuKienLoai.HasValue ? src.DieuKienLoai.Value.ToString() : null))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            // ThanhTuuDatDuoc -> ThanhTuuDatDuocResponse
            CreateMap<ThanhTuuDatDuoc, ThanhTuuDatDuocResponse>()
                .ForMember(dest => dest.MaThanhTuu, opt => opt.MapFrom(src => src.MaThanhTuu))
                .ForMember(dest => dest.TenThanhTuu, opt => opt.Ignore()) // Map từ Navigation
                .ForMember(dest => dest.MoTa, opt => opt.Ignore())
                .ForMember(dest => dest.BieuTuong, opt => opt.Ignore())
                .ForMember(dest => dest.HinhHuy, opt => opt.Ignore())
                .ForMember(dest => dest.DoHiem, opt => opt.Ignore())
                .ForMember(dest => dest.DaXem, opt => opt.MapFrom(src => src.DaXem ?? false))
                .ForMember(dest => dest.DaChiaSe, opt => opt.MapFrom(src => src.DaChiaSe ?? false));
        }
    }
}
