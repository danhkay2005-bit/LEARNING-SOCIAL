using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Kho Người Dùng
    /// </summary>
    public class KhoNguoiDungMapping : Profile
    {
        public KhoNguoiDungMapping()
        {
            CreateMap<KhoNguoiDung, KhoNguoiDungResponse>()
                .ForMember(d => d.MaKho,
                    o => o.MapFrom(s => s.MaKho))
                .ForMember(d => d.MaVatPham,
                    o => o.MapFrom(s => s.MaVatPham))
                .ForMember(d => d.SoLuong,
                    o => o.MapFrom(s => s.SoLuong))
                .ForMember(d => d.DaTrangBi,
                    o => o.MapFrom(s => s.DaTrangBi))
                .ForMember(d => d.ThoiGianMua,
                    o => o.MapFrom(s => s.ThoiGianMua))
                .ForMember(d => d.ThoiGianHetHan,
                    o => o.MapFrom(s => s.ThoiGianHetHan))

                // lấy từ navigation VatPham
                .ForMember(d => d.TenVatPham,
                    o => o.MapFrom(s => s.MaVatPhamNavigation.TenVatPham))
                .ForMember(d => d.DuongDanHinh,
                    o => o.MapFrom(s => s.MaVatPhamNavigation.DuongDanHinh))
                .ForMember(d => d.DoHiem,
                    o => o.MapFrom(s => s.MaVatPhamNavigation.DoHiem))
                .ForMember(d => d.TenDanhMuc,
                    o => o.MapFrom(s => s.MaVatPhamNavigation.MaDanhMucNavigation.TenDanhMuc));
        }
    }
}
