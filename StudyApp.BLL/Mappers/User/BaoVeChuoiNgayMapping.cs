using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Bảo Vệ Chuỗi Ngày (Streak)
    /// </summary>
    public class BaoVeChuoiNgayMapping : Profile
    {
        public BaoVeChuoiNgayMapping()
        {
            CreateMap<BaoVeChuoiNgay, BaoVeChuoiNgayResponse>()
                .ForMember(d => d.MaBaoVe,
                    o => o.MapFrom(s => s.MaBaoVe))
                .ForMember(d => d.NgaySuDung,
                    o => o.MapFrom(s => s.NgaySuDung))
                .ForMember(d => d.LoaiBaoVe,
                    o => o.MapFrom(s =>
                        string.IsNullOrEmpty(s.LoaiBaoVe)
                            ? (LoaiBaoVeStreakEnum?)null
                            : Enum.Parse<LoaiBaoVeStreakEnum>(s.LoaiBaoVe)))
                .ForMember(d => d.ChuoiNgayTruocKhi,
                    o => o.MapFrom(s => s.ChuoiNgayTruocKhi))
                .ForMember(d => d.ChuoiNgaySauKhi,
                    o => o.MapFrom(s => s.ChuoiNgaySauKhi));
        }
    }
}
