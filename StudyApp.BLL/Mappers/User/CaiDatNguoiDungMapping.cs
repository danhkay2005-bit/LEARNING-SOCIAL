using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class CaiDatNguoiDungMapping: Profile
    {
        public CaiDatNguoiDungMapping()
        {
            // CaiDatNguoiDung -> CaiDatNguoiDungResponse
            CreateMap<CaiDatNguoiDung, CaiDatNguoiDungResponse>();

            // CapNhatCaiDatNguoiDungRequest -> CaiDatNguoiDung
            CreateMap<CapNhatCaiDatNguoiDungRequest, CaiDatNguoiDung>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaNguoiDungNavigation, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }       
    }
}
