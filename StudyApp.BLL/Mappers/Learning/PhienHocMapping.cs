using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning;

public class PhienHocMapping : Profile
{
    public PhienHocMapping()
    {
        CreateMap<PhienHoc, PhienHocResponse>()
            .ForMember(d => d.LoaiPhien, o => o.MapFrom(s => ParseEnum<LoaiPhienHocEnum>(s.LoaiPhien)))
            .ForMember(d => d.ThoiGianHocGiay, o => o.MapFrom(s => s.ThoiGianHocGiay ?? 0))
            .ForMember(d => d.ThoiGianHocHienThi, o => o.MapFrom(s => FormatDuration(s.ThoiGianHocGiay)))
            .ForMember(d => d.TongSoThe, o => o.MapFrom(s => s.TongSoThe ?? 0))
            .ForMember(d => d.SoTheMoi, o => o.MapFrom(s => s.SoTheMoi ?? 0))
            .ForMember(d => d.SoTheOnTap, o => o.MapFrom(s => s.SoTheOnTap ?? 0))
            .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
            .ForMember(d => d.SoTheSai, o => o.MapFrom(s => s.SoTheSai ?? 0))
            .ForMember(d => d.SoTheBo, o => o.MapFrom(s => s.SoTheBo ?? 0))
            .ForMember(d => d.DiemDat, o => o.MapFrom(s => s.DiemDat ?? 0))
            .ForMember(d => d.DiemToiDa, o => o.MapFrom(s => s.DiemToiDa ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
            .ForMember(d => d.VangNhan, o => o.MapFrom(s => s.VangNhan ?? 0))
            .ForMember(d => d.CamXuc, o => o.MapFrom(s => ByteToEnum<CamXucHocEnum>(s.CamXuc)))
            .ForMember(d => d.BoDe, o => o.Ignore())
            .ForMember(d => d.ThongKe, o => o.Ignore());

        CreateMap<PhienHoc, PhienHocTomTatResponse>()
            .ForMember(d => d.LoaiPhien, o => o.MapFrom(s => ParseEnum<LoaiPhienHocEnum>(s.LoaiPhien)))
            .ForMember(d => d.ThoiGianHocGiay, o => o.MapFrom(s => s.ThoiGianHocGiay ?? 0))
            .ForMember(d => d.TongSoThe, o => o.MapFrom(s => s.TongSoThe ?? 0))
            .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
            .ForMember(d => d.BoDe, o => o.Ignore());
    }

}
