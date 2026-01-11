using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class NhiemVuProfile : Profile
    {
        public NhiemVuProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<NhiemVu, NhiemVuResponse>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s => Enum.Parse<LoaiNhiemVuEnum>(s.LoaiNhiemVu!)))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s => Enum.Parse<LoaiDieuKienEnum>(s.LoaiDieuKien!)))
                .ForMember(d => d.ConHieuLuc,
                    o => o.MapFrom(s => s.ConHieuLuc ?? true));

            // =========================
            // REQUEST → ENTITY (CREATE)
            // =========================
            CreateMap<TaoNhiemVuRequest, NhiemVu>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s => s.LoaiNhiemVu.ToString()))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s => s.LoaiDieuKien.ToString()))
                .ForMember(d => d.ConHieuLuc,
                    o => o.MapFrom(_ => true))
                .ForMember(d => d.ThoiGianTao,
                    o => o.MapFrom(_ => DateTime.Now));

            // =========================
            // REQUEST → ENTITY (UPDATE)
            // =========================
            CreateMap<CapNhatNhiemVuRequest, NhiemVu>()
                .ForMember(d => d.LoaiNhiemVu,
                    o => o.MapFrom(s => s.LoaiNhiemVu.ToString()))
                .ForMember(d => d.LoaiDieuKien,
                    o => o.MapFrom(s => s.LoaiDieuKien.ToString()));
        }
    }
}
