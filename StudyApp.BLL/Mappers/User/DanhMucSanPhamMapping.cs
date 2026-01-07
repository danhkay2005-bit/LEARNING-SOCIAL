using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Linq;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Danh Mục Sản Phẩm
    /// </summary>
    public class DanhMucSanPhamMapping : Profile
    {
        public DanhMucSanPhamMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== TẠO DANH MỤC =====
            CreateMap<TaoDanhMucSanPhamRequest, DanhMucSanPham>()
                .ForMember(d => d.TenDanhMuc, o => o.MapFrom(s => s.TenDanhMuc))
                .ForMember(d => d.MoTa, o => o.MapFrom(s => s.MoTa))
                .ForMember(d => d.ThuTuHienThi, o => o.MapFrom(s => s.ThuTuHienThi))
                .ForMember(d => d.ThoiGianTao,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // không mapper
                .ForMember(d => d.MaDanhMuc, o => o.Ignore())
                .ForMember(d => d.VatPhams, o => o.Ignore());

            // ===== CẬP NHẬT DANH MỤC =====
            CreateMap<CapNhatDanhMucSanPhamRequest, DanhMucSanPham>()
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<DanhMucSanPham, DanhMucSanPhamResponse>()
                .ForMember(d => d.SoVatPham,
                    o => o.MapFrom(s => s.VatPhams.Count));
        }

        #endregion
    }
}
