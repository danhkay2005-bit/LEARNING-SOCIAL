using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class DiemDanhHangNgayProfile : Profile
    {
        public DiemDanhHangNgayProfile()
        {
            // =====================================================
            // REQUEST -> ENTITY (Điểm danh)
            // =====================================================
            CreateMap<DiemDanhHangNgayRequest, DiemDanhHangNgay>()
                .ForMember(dest => dest.MaNguoiDungNavigation,
                    opt => opt.Ignore())
                .ForMember(dest => dest.NgayThuMay,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ThuongVang,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ThuongXp,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ThuongDacBiet,
                    opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // =====================================================
            CreateMap<DiemDanhHangNgay, DiemDanhHangNgayResponse>();
        }
    }
}
