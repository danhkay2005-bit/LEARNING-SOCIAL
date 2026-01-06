using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class LichSuGiaoDichMapping: Profile
    {
        public LichSuGiaoDichMapping()
        {
            CreateMap<LichSuGiaoDich, LichSuGiaoDichResponse>()
            .ForMember(dest => dest.LoaiGiaoDich, opt => opt.MapFrom(src => ParseEnum<LoaiGiaoDichEnum>(src.LoaiGiaoDich)))
            .ForMember(dest => dest.LoaiTien, opt => opt.MapFrom(src => ParseEnum<LoaiTienTeGiaoDichEnum>(src.LoaiTien)))
            .ForMember(dest => dest.TenLoaiGiaoDich, opt => opt.Ignore())
            .ForMember(dest => dest.TenLoaiTien, opt => opt.Ignore())
            .ForMember(dest => dest.LaChi, opt => opt.Ignore())
            .ForMember(dest => dest.ThayDoi, opt => opt.Ignore());
        }
    }
}
