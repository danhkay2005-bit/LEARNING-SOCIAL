using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class VatPhamProfile : Profile
    {
        public VatPhamProfile()
        {
            // =========================
            // ENTITY → RESPONSE
            // =========================
            CreateMap<VatPham, VatPhamResponse>()
                .ForMember(d => d.LoaiTienTe,
                    o => o.MapFrom(s => (LoaiTienTeEnum)s.LoaiTienTe))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s =>
                        (DoHiemEnum)(s.DoHiem ?? (byte)DoHiemEnum.PhoBien)))
                .ForMember(d => d.ConHang,
                    o => o.MapFrom(s => s.ConHang ?? true));

            // =========================
            // REQUEST → ENTITY
            // =========================

            // Tạo vật phẩm
            CreateMap<TaoVatPhamRequest, VatPham>()
                .ForMember(d => d.LoaiTienTe,
                    o => o.MapFrom(s => (int)s.LoaiTienTe))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => (byte)s.DoHiem))
                .ForMember(d => d.ConHang,
                    o => o.MapFrom(_ => true));

            // Cập nhật vật phẩm
            CreateMap<CapNhatVatPhamRequest, VatPham>()
                .ForMember(d => d.LoaiTienTe,
                    o => o.MapFrom(s => (int)s.LoaiTienTe))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => (byte)s.DoHiem));
        }
    }
}
