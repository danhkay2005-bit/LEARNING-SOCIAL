using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class ThongKeNgayMapping : Profile
    {
        public ThongKeNgayMapping()
        {
            CreateMap<ThongKeNgay, ThongKeNgayResponse>()
                .ForMember(d => d.TongTheHoc, o => o.MapFrom(s => s.TongTheHoc ?? 0))
                .ForMember(d => d.SoTheMoi, o => o.MapFrom(s => s.SoTheMoi ?? 0))
                .ForMember(d => d.SoTheOnTap, o => o.MapFrom(s => s.SoTheOnTap ?? 0))
                .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
                .ForMember(d => d.SoTheSai, o => o.MapFrom(s => s.SoTheSai ?? 0))
                .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
                .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
                .ForMember(d => d.SoPhienHoc, o => o.MapFrom(s => s.SoPhienHoc ?? 0))
                .ForMember(d => d.SoBoDeHoc, o => o.MapFrom(s => s.SoBoDeHoc ?? 0))
                .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
                .ForMember(d => d.VangNhan, o => o.MapFrom(s => s.VangNhan ?? 0))
                .ForMember(d => d.DaHoanThanhMucTieu, o => o.MapFrom(s => s.DaHoanThanhMucTieu ?? false));

        }
    }
}
