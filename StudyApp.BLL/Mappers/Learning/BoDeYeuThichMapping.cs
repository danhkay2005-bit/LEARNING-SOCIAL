using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BoDeYeuThichMapping : Profile
    {
        public BoDeYeuThichMapping()
        {
            // ============================
            // ENTITY → RESPONSE
            // ============================

            CreateMap<BoDeYeuThich, BoDeYeuThichResponse>()
                .ForMember(dest => dest.BoDe,
                    opt => opt.MapFrom(src => src.MaBoDeNavigation))
                // BoDeHocTomTatResponse có mapping riêng
                .ForMember(dest => dest.MaNguoiDung,
                    opt => opt.MapFrom(src => src.MaNguoiDung))
                .ForMember(dest => dest.MaBoDe,
                    opt => opt.MapFrom(src => src.MaBoDe));

            // ============================
            // KHÔNG map REQUEST → ENTITY
            // ============================
            // Vì:
            // - Yêu thích / bỏ yêu thích = toggle theo context user
            // - MaNguoiDung lấy từ token
            // - Entity tạo trong Service là đúng nhất
        }
    }
}
