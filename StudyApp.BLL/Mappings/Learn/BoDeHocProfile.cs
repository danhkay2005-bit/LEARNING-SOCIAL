using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Mappings.Learn
{
    public class BoDeHocProfile : Profile
    {
        public BoDeHocProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================

            CreateMap<BoDeHoc, BoDeHocResponse>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src =>
                        (MucDoKhoEnum)(src.MucDoKho ?? (byte)MucDoKhoEnum.TrungBinh)))
                .ForMember(dest => dest.LaCongKhai,
                    opt => opt.MapFrom(src => src.LaCongKhai ?? true))
                .ForMember(dest => dest.SoLuongThe,
                    opt => opt.MapFrom(src => src.SoLuongThe ?? 0))
                .ForMember(dest => dest.SoLuotHoc,
                    opt => opt.MapFrom(src => src.SoLuotHoc ?? 0))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(src => src.ThoiGianTao ?? DateTime.MinValue));

            // =========================
            // CREATE REQUEST -> ENTITY
            // =========================

            CreateMap<TaoBoDeHocRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (byte)src.MucDoKho))
                .ForMember(dest => dest.SoLuongThe, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotHoc, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.Ignore())
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.MaBoDeGoc, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.Now));

            // =========================
            // UPDATE REQUEST -> ENTITY
            // =========================

            CreateMap<CapNhatBoDeHocRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (byte)src.MucDoKho))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.Now))

                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeGoc, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuongThe, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotHoc, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.Ignore())
                .ForMember(dest => dest.DaXoa, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore());
        }
    }
}
