using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper profile cho Bài Đăng (Request ↔ Entity ↔ Response)
    /// </summary>
    public class BaiDangMapping : Profile
    {
        public BaiDangMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== TẠO BÀI ĐĂNG =====
            CreateMap<TaoBaiDangRequest, BaiDang>()
                .ForMember(d => d.LoaiBaiDang,
                    o => o.MapFrom(s => s.LoaiBaiDang.ToString()))
                .ForMember(d => d.QuyenRiengTu,
                    o => o.MapFrom(s => (byte)s.QuyenRiengTu))
                .ForMember(d => d.GhimBaiDang, o => o.MapFrom(_ => false))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
                .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThoiGianSua, o => o.Ignore())

                // Navigation xử lý ở Service
                .ForMember(d => d.MentionBaiDangs, o => o.Ignore())
                .ForMember(d => d.MaHashtags, o => o.Ignore())
                .ForMember(d => d.ReactionBaiDangs, o => o.Ignore())
                .ForMember(d => d.BinhLuanBaiDangs, o => o.Ignore());

            // ===== CẬP NHẬT BÀI ĐĂNG =====
            CreateMap<CapNhatBaiDangRequest, BaiDang>()
                .ForMember(d => d.QuyenRiengTu,
                    o => o.MapFrom(s =>
                        s.QuyenRiengTu.HasValue
                            ? (byte?)s.QuyenRiengTu.Value
                            : null))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
                .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))

                // Không cho sửa
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.SoReaction, o => o.Ignore())
                .ForMember(d => d.SoBinhLuan, o => o.Ignore())
                .ForMember(d => d.SoLuotXem, o => o.Ignore())
                .ForMember(d => d.SoLuotChiaSe, o => o.Ignore())
                .ForMember(d => d.MentionBaiDangs, o => o.Ignore())
                .ForMember(d => d.MaHashtags, o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== CHI TIẾT BÀI ĐĂNG =====
            CreateMap<BaiDang, BaiDangResponse>()
                .ForMember(d => d.LoaiBaiDang,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiBaiDangEnum>(s.LoaiBaiDang)))
                .ForMember(d => d.QuyenRiengTu,
                    o => o.MapFrom(s =>
                        MappingHelpers.ByteToEnum<QuyenRiengTuEnum>(s.QuyenRiengTu) ?? QuyenRiengTuEnum.CongKhai))

                .ForMember(d => d.SoReaction, o => o.MapFrom(s => s.SoReaction ?? 0))
                .ForMember(d => d.SoBinhLuan, o => o.MapFrom(s => s.SoBinhLuan ?? 0))
                .ForMember(d => d.SoLuotXem, o => o.MapFrom(s => s.SoLuotXem ?? 0))
                .ForMember(d => d.SoLuotChiaSe, o => o.MapFrom(s => s.SoLuotChiaSe ?? 0))
                .ForMember(d => d.GhimBaiDang, o => o.MapFrom(s => s.GhimBaiDang ?? false))
                .ForMember(d => d.TatBinhLuan, o => o.MapFrom(s => s.TatBinhLuan ?? false))
                .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))

                // user-context → xử lý ở Service
                .ForMember(d => d.DaReaction, o => o.Ignore())
                .ForMember(d => d.LoaiReactionCuaToi, o => o.Ignore())
                .ForMember(d => d.DaLuu, o => o.Ignore())

                // navigation response
                .ForMember(d => d.Hashtags, o => o.Ignore())
                .ForMember(d => d.MentionNguoiDungs, o => o.Ignore())
                .ForMember(d => d.TopReactions, o => o.Ignore())
                .ForMember(d => d.BaiDangGoc, o => o.Ignore());

            // ===== TÓM TẮT (DANH SÁCH) =====
            CreateMap<BaiDang, BaiDangTomTatResponse>()
                .ForMember(d => d.NoiDungRutGon,
                    o => o.MapFrom(s =>
                        MappingHelpers.Truncate(s.NoiDung, 150)))
                .ForMember(d => d.HinhAnhDauTien,
                    o => o.MapFrom(s =>
                        MappingHelpers.GetFirstImage(s.HinhAnh)))
                .ForMember(d => d.LoaiBaiDang,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiBaiDangEnum>(s.LoaiBaiDang)))
                .ForMember(d => d.SoReaction, o => o.MapFrom(s => s.SoReaction ?? 0))
                .ForMember(d => d.SoBinhLuan, o => o.MapFrom(s => s.SoBinhLuan ?? 0));
        }

        #endregion
    }
}
