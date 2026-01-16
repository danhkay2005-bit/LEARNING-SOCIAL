using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class ThanhTuuDatDuocProfile : Profile
    {
        public ThanhTuuDatDuocProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<ThanhTuuDatDuoc, ThanhTuuDatDuocResponse>()
                .ForMember(d => d.NgayDat, o => o.MapFrom(s => s.NgayDat))
                .ForMember(d => d.DaXem, o => o.MapFrom(s => s.DaXem ?? false));
        }
    }
}
