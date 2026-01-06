using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class ThuMucMapping : Profile
    {
        public ThuMucMapping() 
        {

            CreateMap<TaoThuMucRequest, ThuMuc>()
                .ForMember(d => d.MaThuMuc, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Gán từ UserContext trong Service
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.BoDeHocs, o => o.Ignore())
                .ForMember(d => d.InverseMaThuMucChaNavigation, o => o.Ignore())
                .ForMember(d => d.MaThuMucChaNavigation, o => o.Ignore());

            CreateMap<CapNhatThuMucRequest, ThuMuc>()
                .ForMember(d => d.MaThuMuc, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));


            CreateMap<ThuMuc, ThuMucResponse>()
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
                .ForMember(d => d.SoBoDeTrongThuMuc, o => o.MapFrom(s => s.BoDeHocs.Count))
                .ForMember(d => d.SoThuMucCon, o => o.MapFrom(s => s.InverseMaThuMucChaNavigation.Count))
                .ForMember(d => d.ThuMucCons, o => o.Ignore())
                .ForMember(d => d.BoDes, o => o.Ignore());

            CreateMap<ThuMuc, ThuMucTomTatResponse>()
                .ForMember(d => d.SoBoDe, o => o.MapFrom(s => s.BoDeHocs.Count))
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

            CreateMap<ThuMuc, ThuMucNodeResponse>()
                .ForMember(d => d.SoBoDe, o => o.MapFrom(s => s.BoDeHocs.Count))
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
                .ForMember(d => d.Children, o => o.Ignore());


        }
    }
}
