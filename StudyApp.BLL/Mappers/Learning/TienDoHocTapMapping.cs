using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class TienDoHocTapMapping : Profile
    {
        public TienDoHocTapMapping()
        {
           
            CreateMap<TienDoHocTap, TienDoTheResponse>()
                .ForMember(d => d.TrangThai, o => o.MapFrom(s => (TrangThaiSRSEnum)(s.TrangThai ?? 0)))
                .ForMember(d => d.HeSoDe, o => o.MapFrom(s => s.HeSoDe ?? 2.5))
                .ForMember(d => d.KhoangCachNgay, o => o.MapFrom(s => s.KhoangCachNgay ?? 0))
                .ForMember(d => d.SoLanLap, o => o.MapFrom(s => s.SoLanLap ?? 0))
                .ForMember(d => d.SoLanDung, o => o.MapFrom(s => s.SoLanDung ?? 0))
                .ForMember(d => d.SoLanSai, o => o.MapFrom(s => s.SoLanSai ?? 0))
                .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
                .ForMember(d => d.ThoiGianTraLoiTbGiay, o => o.MapFrom(s => s.ThoiGianTraLoiTbgiay ?? 0))
                .ForMember(d => d.DoKhoCanNhan, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoCanNhan)));
            
        }
    }
}
