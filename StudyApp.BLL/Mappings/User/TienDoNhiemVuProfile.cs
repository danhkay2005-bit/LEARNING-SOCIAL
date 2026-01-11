using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class TienDoNhiemVuProfile : Profile
    {
        public TienDoNhiemVuProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
                .ForMember(d => d.TienDoHienTai,
                    o => o.MapFrom(s => s.TienDoHienTai ?? 0))
                .ForMember(d => d.DaHoanThanh,
                    o => o.MapFrom(s => s.DaHoanThanh ?? false))
                .ForMember(d => d.DaNhanThuong,
                    o => o.MapFrom(s => s.DaNhanThuong ?? false));
        }
    }
}
