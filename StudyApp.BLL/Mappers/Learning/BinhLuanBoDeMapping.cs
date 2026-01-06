using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BinhLuanBoDeMapping : Profile
    {
        public BinhLuanBoDeMapping()
        {
            CreateMap<TaoBinhLuanBoDeRequest, BinhLuanBoDe>()
                .ForMember(d => d.MaBinhLuan, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Gán từ UserContext trong Service
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
                .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThoiGianSua, o => o.Ignore())
                .ForMember(d => d.MaBoDeNavigation, o => o.Ignore())
                .ForMember(d => d.MaBinhLuanChaNavigation, o => o.Ignore())
                .ForMember(d => d.InverseMaBinhLuanChaNavigation, o => o.Ignore())
                .ForMember(d => d.ThichBinhLuanBoDes, o => o.Ignore());

            CreateMap<CapNhatBinhLuanBoDeRequest, BinhLuanBoDe>()
                .ForMember(d => d.MaBinhLuan, o => o.Ignore())
                .ForMember(d => d.MaBoDe, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaBinhLuanCha, o => o.Ignore())
                .ForMember(d => d.ThoiGian, o => o.Ignore())
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
                .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<BinhLuanBoDe, BinhLuanBoDeResponse>()
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
                .ForMember(d => d.SoTraLoi, o => o.MapFrom(s => s.InverseMaBinhLuanChaNavigation.Count))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))
                .ForMember(d => d.NguoiBinhLuan, o => o.Ignore())
                .ForMember(d => d.DaThich, o => o.Ignore())
                .ForMember(d => d.LaCuaToi, o => o.Ignore())
                .ForMember(d => d.TraLois, o => o.Ignore());


        }
    }
}
