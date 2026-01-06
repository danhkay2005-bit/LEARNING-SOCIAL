using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class VatPhamMapping: Profile
    {
        VatPhamMapping()
        {
            // VatPham -> VatPhamResponse
            CreateMap<VatPham, VatPhamResponse>()
                .ForMember(dest => dest.LoaiTienTe, opt => opt.MapFrom(src => (LoaiTienTeEnum)src.LoaiTienTe))
                .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
                .ForMember(dest => dest.ConHang, opt => opt.MapFrom(src => src.ConHang ?? true))
                .ForMember(dest => dest.DanhMuc, opt => opt.Ignore());

            // VatPham -> VatPhamTomTatResponse
            CreateMap<VatPham, VatPhamTomTatResponse>()
                .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem));

            // KhoNguoiDung -> KhoNguoiDungResponse
            CreateMap<KhoNguoiDung, KhoNguoiDungResponse>()
                .ForMember(dest => dest.MaKho, opt => opt.MapFrom(src => src.MaKho))
                .ForMember(dest => dest.SoLuong, opt => opt.MapFrom(src => src.SoLuong ?? 0))
                .ForMember(dest => dest.DaTrangBi, opt => opt.MapFrom(src => src.DaTrangBi ?? false))
                .ForMember(dest => dest.TenVatPham, opt => opt.Ignore()) // Map từ Navigation
                .ForMember(dest => dest.DuongDanHinh, opt => opt.Ignore())
                .ForMember(dest => dest.TenDanhMuc, opt => opt.Ignore())
                .ForMember(dest => dest.DoHiem, opt => opt.Ignore());
            // DanhMucSanPham -> DanhMucSanPhamTomTatResponse
            CreateMap<DanhMucSanPham, DanhMucSanPhamTomTatResponse>();

            // DanhMucSanPham -> DanhMucVoiVatPhamResponse
            CreateMap<DanhMucSanPham, DanhMucVoiVatPhamResponse>()
                .ForMember(dest => dest.VatPhams, opt => opt.Ignore());
        }
    }
}
