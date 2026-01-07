using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Đáp án trắc nghiệm
    /// </summary>
    public class DapAnTracNghiemMapping : Profile
    {
        public DapAnTracNghiemMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Tạo đáp án trắc nghiệm
            // =====================================================
            CreateMap<TaoDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(dest => dest.MaDapAn, opt => opt.Ignore())       // identity
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())         // lấy từ route
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // Cập nhật đáp án trắc nghiệm
            // =====================================================
            CreateMap<CapNhatDapAnTracNghiemRequest, DapAnTracNghiem>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // DapAnTracNghiem -> DapAnTracNghiemResponse
            // =====================================================
            CreateMap<DapAnTracNghiem, DapAnTracNghiemResponse>()
                .ForMember(dest => dest.LaDapAnDung,
                    opt => opt.MapFrom(src => src.LaDapAnDung ?? false))
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0));

            // =====================================================
            // ENTITY -> RESPONSE (KHI HỌC)
            // Ẩn đáp án đúng
            // =====================================================
            CreateMap<DapAnTracNghiem, DapAnTracNghiemHocResponse>()
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForSourceMember(src => src.LaDapAnDung, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.GiaiThich, opt => opt.DoNotValidate());
        }
    }
}
