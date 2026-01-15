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
            // =========================================================
            // REQUEST -> ENTITY (Khi tạo mới điểm danh từ API nếu cần)
            // =========================================================
            CreateMap<DiemDanhHangNgayRequest, DiemDanhHangNgay>()
                .ForMember(dest => dest.MaNguoiDungNavigation, opt => opt.Ignore()) // EF tự lo
                .ForMember(dest => dest.NgayThuMay, opt => opt.Ignore())            // Tính toán ở Service
                .ForMember(dest => dest.ThuongVang, opt => opt.Ignore())            // Tính toán ở Service
                .ForMember(dest => dest.ThuongXp, opt => opt.Ignore())              // Tính toán ở Service
                .ForMember(dest => dest.ThuongDacBiet, opt => opt.Ignore());

            // =========================================================
            // ENTITY -> RESPONSE (Trả về cho Client hiển thị)
            // =========================================================
            CreateMap<DiemDanhHangNgay, DiemDanhHangNgayResponse>()
                // Xử lý null: Nếu null thì trả về 0 để UI không bị lỗi
                .ForMember(dest => dest.ThuongVang, opt => opt.MapFrom(src => src.ThuongVang ?? 0))
                .ForMember(dest => dest.ThuongXp, opt => opt.MapFrom(src => src.ThuongXp ?? 0));
        }
    }
}