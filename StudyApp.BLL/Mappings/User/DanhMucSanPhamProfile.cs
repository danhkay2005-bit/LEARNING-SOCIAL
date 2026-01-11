using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class DanhMucSanPhamProfile : Profile
    {
        public DanhMucSanPhamProfile()
        {
            // =====================================================
            // CREATE
            // =====================================================
            CreateMap<TaoDanhMucSanPhamRequest, DanhMucSanPham>()
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.VatPhams,
                    opt => opt.Ignore());

            // =====================================================
            // UPDATE
            // =====================================================
            CreateMap<CapNhatDanhMucSanPhamRequest, DanhMucSanPham>()
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.Ignore())
                .ForMember(dest => dest.VatPhams,
                    opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // =====================================================
            CreateMap<DanhMucSanPham, DanhMucSanPhamResponse>();
        }
    }
}
