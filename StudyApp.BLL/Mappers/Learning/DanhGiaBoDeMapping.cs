using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class DanhGiaBoDeMapping : Profile
    {
        public DanhGiaBoDeMapping()
        {
            CreateMap<DanhGiaBoDeRequest, DanhGiaBoDe>()
                .ForMember(d => d.MaDanhGia, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Thường gán từ User Claims trong Service
                .ForMember(d => d.DoKhoThucTe, o => o.MapFrom(s => s.DoKhoThucTe.HasValue ? (byte)s.DoKhoThucTe.Value : (byte?)null))
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

            CreateMap<CapNhatDanhGiaRequest, DanhGiaBoDe>()
                .ForMember(d => d.MaDanhGia, o => o.Ignore())
                .ForMember(d => d.MaBoDe, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.ThoiGian, o => o.Ignore())
                .ForMember(d => d.DoKhoThucTe, o => o.MapFrom((s, d) => s.DoKhoThucTe.HasValue ? (byte)s.DoKhoThucTe.Value : d.DoKhoThucTe))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<DanhGiaBoDe, DanhGiaBoDeResponse>()
                .ForMember(d => d.DoKhoThucTe, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoThucTe)))
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
                .ForMember(d => d.NguoiDanhGia, o => o.Ignore())
                .ForMember(d => d.DaThich, o => o.Ignore())
                .ForMember(d => d.LaCuaToi, o => o.Ignore());
        }
    }
}
