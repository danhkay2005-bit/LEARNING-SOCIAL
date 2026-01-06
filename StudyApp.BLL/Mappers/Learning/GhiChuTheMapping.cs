using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class GhiChuTheMapping : Profile
    {
        public GhiChuTheMapping()
        {
            CreateMap<TaoGhiChuTheRequest, GhiChuThe>()
                .ForMember(d => d.MaGhiChu, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Gán từ User ID trong Service
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<CapNhatGhiChuTheRequest, GhiChuThe>()
                .ForMember(d => d.MaGhiChu, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<GhiChuThe, GhiChuTheResponse>();
        }
    }
}
