using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class PhanTuSapXepMapping : Profile
    {
        public PhanTuSapXepMapping()
        {
            CreateMap<TaoPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(d => d.MaPhanTu, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<CapNhatPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(d => d.MaPhanTu, o => o.Ignore())
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<PhanTuSapXep, PhanTuSapXepResponse>();

            CreateMap<PhanTuSapXep, PhanTuSapXepHocResponse>()
                .ForMember(d => d.ThuTuHienThi, o => o.Ignore());
        }
    }
}
