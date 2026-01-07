using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.NguoiDung;
using StudyApp.BLL.Mappers;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Lịch Sử Người Dùng (Giao dịch & Hoạt động)
    /// </summary>
    public class LichSuNguoiDungMapping : Profile
    {
        public LichSuNguoiDungMapping()
        {
            MapLichSuGiaoDich();
            MapLichSuHoatDong();
        }

        #region Lịch sử giao dịch

        private void MapLichSuGiaoDich()
        {
            CreateMap<LichSuGiaoDich, LichSuGiaoDichResponse>()
                .ForMember(d => d.MaGiaoDich,
                    o => o.MapFrom(s => s.MaGiaoDich))
                .ForMember(d => d.LoaiGiaoDich,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiGiaoDichEnum>(s.LoaiGiaoDich)))
                .ForMember(d => d.LoaiTien,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiTienTeGiaoDichEnum>(s.LoaiTien)))
                .ForMember(d => d.SoLuong,
                    o => o.MapFrom(s => s.SoLuong))
                .ForMember(d => d.SoDuTruoc,
                    o => o.MapFrom(s => s.SoDuTruoc))
                .ForMember(d => d.SoDuSau,
                    o => o.MapFrom(s => s.SoDuSau))
                .ForMember(d => d.MoTa,
                    o => o.MapFrom(s => s.MoTa))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // VatPham là optional
                .ForMember(d => d.VatPham,
                    o => o.MapFrom(s => s.MaVatPhamNavigation));
        }

        #endregion

        #region Lịch sử hoạt động

        private void MapLichSuHoatDong()
        {
            CreateMap<LichSuHoatDong, LichSuHoatDongResponse>()
                .ForMember(d => d.MaHoatDong,
                    o => o.MapFrom(s => s.MaHoatDong))
                .ForMember(d => d.LoaiHoatDong,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiHoatDongEnum>(s.LoaiHoatDong)))
                .ForMember(d => d.MoTa,
                    o => o.MapFrom(s => s.MoTa))
                .ForMember(d => d.ChiTiet,
                    o => o.MapFrom(s => s.DuLieuThem))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian));
        }

        #endregion
    }
}
