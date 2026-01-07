using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Mappers.User
{
    public class VatPhamMapping : Profile
    {
        public VatPhamMapping()
        {
            // ============================
            // REQUEST → ENTITY
            // ============================

            CreateMap<TaoVatPhamRequest, VatPham>()
                .ForMember(dest => dest.LoaiTienTe,
                    opt => opt.MapFrom(src => (int)src.LoaiTienTe))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<CapNhatVatPhamRequest, VatPham>()
                .ForMember(dest => dest.LoaiTienTe,
                    opt => opt.MapFrom(src =>
                        src.LoaiTienTe.HasValue
                            ? (int?)src.LoaiTienTe.Value
                            : null
                    ))
                // PATCH: không overwrite khi null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null)
                );

            // ============================
            // ENTITY → RESPONSE
            // ============================

            CreateMap<VatPham, VatPhamResponse>()
                .ForMember(dest => dest.LoaiTienTe,
                    opt => opt.MapFrom(src => (LoaiTienTeEnum)src.LoaiTienTe))
                .ForMember(dest => dest.DanhMuc,
                    opt => opt.MapFrom(src => src.MaDanhMucNavigation));

            CreateMap<VatPham, VatPhamTomTatResponse>();

            CreateMap<DanhMucSanPham, DanhMucSanPhamTomTatResponse>();
        }
    }
}
