using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class ChiTietTraLoiMapping : Profile
    {
        public ChiTietTraLoiMapping()
        {
            CreateMap<GuiCauTraLoiRequest, ChiTietTraLoi>()
            .ForMember(d => d.MaTraLoi, o => o.Ignore())
            .ForMember(d => d.DapAnDung, o => o.Ignore())
            .ForMember(d => d.DoKhoUserDanhGia, o => o.MapFrom(s => s.DoKhoUserDanhGia.HasValue ? (byte)s.DoKhoUserDanhGia.Value : (byte?)null))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaPhienNavigation, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<ChiTietTraLoi, ChiTietTraLoiResponse>()
                .ForMember(d => d.ThoiGianTraLoiGiay, o => o.MapFrom(s => s.ThoiGianTraLoiGiay ?? 0))
                .ForMember(d => d.DoKhoUserDanhGia, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoUserDanhGia)))
                .ForMember(d => d.The, o => o.Ignore());
        }
    }
}
