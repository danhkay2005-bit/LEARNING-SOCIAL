using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

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
                    o => o.MapFrom(s => Enum.Parse<LoaiThanhTuuEnum>(s.LoaiThanhTuu!)))
                .ForMember(d => d.DieuKienLoai,
                    o => o.MapFrom(s => Enum.Parse<LoaiDieuKienEnum>(s.DieuKienLoai!)))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => (DoHiemEnum)(s.DoHiem ?? (byte)DoHiemEnum.PhoBien)))
                .ForMember(d => d.BiAn,
                    o => o.MapFrom(s => s.BiAn ?? false));

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
    }
}
