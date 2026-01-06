using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class VaiTroMapping: Profile
    {
        public VaiTroMapping()
        {
            CreateMap<VaiTro, VaiTroResponse>();

            CreateMap<VaiTro, VaiTroChiTietResponse>()
                .ForMember(dest => dest.SoNguoiDung, opt => opt.Ignore());

            // TaoVaiTroRequest -> VaiTro
            CreateMap<TaoVaiTroRequest, VaiTro>()
                .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.NguoiDungs, opt => opt.Ignore());

            // CapNhatVaiTroRequest -> VaiTro
            CreateMap<CapNhatVaiTroRequest, VaiTro>()
                .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiDungs, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
