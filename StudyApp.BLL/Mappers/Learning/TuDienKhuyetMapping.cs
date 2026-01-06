using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class TuDienKhuyetMapping : Profile
    {
        public TuDienKhuyetMapping()
        {
            CreateMap<TaoTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(d => d.MaTuDienKhuyet, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());
            CreateMap<CapNhatTuDienKhuyetRequest, TuDienKhuyet>()
                .ForMember(d => d.MaTuDienKhuyet, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<TuDienKhuyet, TuDienKhuyetResponse>();

            CreateMap<TuDienKhuyet, TuDienKhuyetHocResponse>()
            .ForMember(d => d.DoRongTu, o => o.MapFrom(s => Math.Max(s.TuCanDien.Length + 2, 5)));
        }
    }
}
