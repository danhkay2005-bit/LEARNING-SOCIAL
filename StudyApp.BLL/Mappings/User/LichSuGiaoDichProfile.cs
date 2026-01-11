using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class LichSuGiaoDichProfile : Profile
    {
        public LichSuGiaoDichProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<LichSuGiaoDich, LichSuGiaoDichResponse>()
                .ForMember(d => d.LoaiGiaoDich,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiGiaoDichEnum>(s.LoaiGiaoDich)))
                .ForMember(d => d.LoaiTien,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiTienGiaoDichEnum>(s.LoaiTien)));
        }
    }
}
