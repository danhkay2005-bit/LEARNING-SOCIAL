using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BaoCaoBoDeMapping : Profile
    {
        public BaoCaoBoDeMapping()
        {
            CreateMap<BaoCaoBoDeRequest, BaoCaoBoDe>()
                .ForMember(d => d.MaBaoCao, o => o.Ignore())
                .ForMember(d => d.MaNguoiBaoCao, o => o.Ignore()) // Gán từ User ID trong Service
                .ForMember(d => d.LyDo, o => o.MapFrom(s => s.LyDo.ToString()))
                .ForMember(d => d.TrangThai, o => o.MapFrom(_ => TrangThaiBaoCaoEnum.ChoDuyet.ToString()))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

            CreateMap<BaoCaoBoDe, BaoCaoBoDeResponse>()
                .ForMember(d => d.LyDo, o => o.MapFrom(s => ParseEnum<LyDoBaoCaoEnum>(s.LyDo)))
                .ForMember(d => d.TrangThai, o => o.MapFrom(s => ParseEnum<TrangThaiBaoCaoEnum>(s.TrangThai)))
                .ForMember(d => d.BoDe, o => o.Ignore())
                .ForMember(d => d.NguoiBaoCao, o => o.Ignore());
        }
    }
}
