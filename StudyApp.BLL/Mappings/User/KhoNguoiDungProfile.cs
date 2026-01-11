using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class KhoNguoiDungProfile : Profile
    {
        public KhoNguoiDungProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<KhoNguoiDung, KhoNguoiDungResponse>()
                .ForMember(d => d.SoLuong,
                    o => o.MapFrom(s => s.SoLuong ?? 0))
                .ForMember(d => d.DaTrangBi,
                    o => o.MapFrom(s => s.DaTrangBi ?? false));

            // =========================
            // REQUEST → ENTITY
            // =========================

            // Mua vật phẩm
            CreateMap<MuaVatPhamRequest, KhoNguoiDung>()
                .ForMember(d => d.MaKho, o => o.Ignore())
                .ForMember(d => d.DaTrangBi, o => o.MapFrom(_ => false))
                .ForMember(d => d.ThoiGianMua,
                    o => o.MapFrom(_ => DateTime.Now))
                .ForMember(d => d.ThoiGianHetHan, o => o.Ignore());

            // Trang bị / gỡ trang bị
            CreateMap<TrangBiVatPhamRequest, KhoNguoiDung>()
                .ForMember(d => d.MaKho, o => o.Ignore())
                .ForMember(d => d.SoLuong, o => o.Ignore())
                .ForMember(d => d.ThoiGianMua, o => o.Ignore())
                .ForMember(d => d.ThoiGianHetHan, o => o.Ignore());

            // SuDungVatPhamRequest
            // ❌ Không map trực tiếp (chỉ dùng để xử lý nghiệp vụ)
        }
    }
}
