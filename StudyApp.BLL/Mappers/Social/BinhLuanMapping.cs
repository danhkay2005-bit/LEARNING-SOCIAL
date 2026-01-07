using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;
using StudyApp.DTO.Requests.Social;
using System;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper cho Bình Luận Bài Đăng
    /// </summary>
    public class BinhLuanMapping : Profile
    {
        public BinhLuanMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== TẠO BÌNH LUẬN =====
            CreateMap<TaoBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(d => d.MaBaiDang, o => o.MapFrom(s => s.MaBaiDang))
                .ForMember(d => d.NoiDung, o => o.MapFrom(s => s.NoiDung))
                .ForMember(d => d.HinhAnh, o => o.MapFrom(s => s.HinhAnh))
                .ForMember(d => d.MaStickerDung, o => o.MapFrom(s => s.MaStickerDung))
                .ForMember(d => d.MaBinhLuanCha, o => o.MapFrom(s => s.MaBinhLuanCha))

                // system fields
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
                .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThoiGianSua, o => o.Ignore())

                // navigation → service xử lý
                .ForMember(d => d.MentionBinhLuans, o => o.Ignore())
                .ForMember(d => d.ThichBinhLuans, o => o.Ignore())
                .ForMember(d => d.InverseMaBinhLuanChaNavigation, o => o.Ignore())
                .ForMember(d => d.MaBaiDangNavigation, o => o.Ignore())
                .ForMember(d => d.MaBinhLuanChaNavigation, o => o.Ignore());

            // ===== CẬP NHẬT BÌNH LUẬN =====
            CreateMap<CapNhatBinhLuanRequest, BinhLuanBaiDang>()
                .ForMember(d => d.NoiDung, o => o.MapFrom(s => s.NoiDung))
                .ForMember(d => d.HinhAnh, o => o.MapFrom(s => s.HinhAnh))
                .ForMember(d => d.MaStickerDung, o => o.MapFrom(s => s.MaStickerDung))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
                .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))

                // không cho sửa
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaBaiDang, o => o.Ignore())
                .ForMember(d => d.MaBinhLuanCha, o => o.Ignore())
                .ForMember(d => d.SoLuotThich, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.DaXoa, o => o.Ignore())

                // navigation
                .ForMember(d => d.MentionBinhLuans, o => o.Ignore())
                .ForMember(d => d.ThichBinhLuans, o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== CHI TIẾT BÌNH LUẬN =====
            CreateMap<BinhLuanBaiDang, BinhLuanResponse>()
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
                .ForMember(d => d.SoTraLoi,
                    o => o.MapFrom(s =>
                        s.InverseMaBinhLuanChaNavigation.Count))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))

                // user-context
                .ForMember(d => d.DaThich, o => o.Ignore())

                // navigation response
                .ForMember(d => d.NguoiBinhLuan, o => o.Ignore())
                .ForMember(d => d.Sticker, o => o.Ignore())
                .ForMember(d => d.MentionNguoiDungs, o => o.Ignore())
                .ForMember(d => d.TraLois, o => o.Ignore());

            // ===== TÓM TẮT =====
            CreateMap<BinhLuanBaiDang, BinhLuanTomTatResponse>()
                .ForMember(d => d.NoiDungRutGon,
                    o => o.MapFrom(s =>
                        MappingHelpers.Truncate(s.NoiDung, 100)))
                .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
                .ForMember(d => d.NguoiBinhLuan, o => o.Ignore());
        }

        #endregion
    }
}
