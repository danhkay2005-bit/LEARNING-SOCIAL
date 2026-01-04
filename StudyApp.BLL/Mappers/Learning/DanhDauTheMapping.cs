using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class DanhDauTheMapping : Profile
    {
        public DanhDauTheMapping()
        {
            CreateMap<DanhDauTheRequest, DanhDauThe>()
                .ForMember(d => d.MaDanhDau, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Gán từ UserContext trong Service
                .ForMember(d => d.LoaiDanhDau, o => o.MapFrom(s => s.LoaiDanhDau.ToString()))
                .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MaTheNavigation, o => o.Ignore());

            CreateMap<DanhDauThe, DanhDauTheResponse>()
                .ForMember(d => d.LoaiDanhDau, o => o.MapFrom(s => ParseEnum<LoaiDanhDauTheEnum>(s.LoaiDanhDau)))
                .ForMember(d => d.The, o => o.Ignore());
        }
    }
}
