using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using StudyApp.BLL.Mappers;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Boost Đang Hoạt Động
    /// </summary>
    public class BoostDangHoatDongMapping : Profile
    {
        public BoostDangHoatDongMapping()
        {
            CreateMap<BoostDangHoatDong, BoostDangHoatDongResponse>()
                .ForMember(d => d.MaBoost,
                    o => o.MapFrom(s => s.MaBoost))
                .ForMember(d => d.MaVatPham,
                    o => o.MapFrom(s => s.MaVatPham))
                .ForMember(d => d.LoaiBoost,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnumNullable<LoaiBoostEnum>(s.LoaiBoost)))
                .ForMember(d => d.HeSoNhan,
                    o => o.MapFrom(s => s.HeSoNhan))
                .ForMember(d => d.ThoiGianBatDau,
                    o => o.MapFrom(s => s.ThoiGianBatDau))
                .ForMember(d => d.ThoiGianKetThuc,
                    o => o.MapFrom(s => s.ThoiGianKetThuc))
                .ForMember(d => d.ConHieuLuc,
                    o => o.MapFrom(s => s.ConHieuLuc))

                // lấy từ navigation
                .ForMember(d => d.TenVatPham,
                    o => o.Ignore())
                .ForMember(d => d.DuongDanHinh,
                    o => o.Ignore());
        }
    }
}
