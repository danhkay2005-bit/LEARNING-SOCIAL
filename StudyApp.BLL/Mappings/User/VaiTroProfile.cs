using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class VaiTroProfile : Profile
    {
        public VaiTroProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<VaiTro, VaiTroResponse>();

            // =========================
            // REQUEST → ENTITY
            // =========================

            // Tạo vai trò
            CreateMap<TaoVaiTroRequest, VaiTro>();

            // Cập nhật vai trò
            CreateMap<CapNhatVaiTroRequest, VaiTro>();
        }
    }
}
