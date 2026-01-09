using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappers.Social
{
    /// <summary>
    /// Mapping cho Tin Nhắn
    /// </summary>
    public class TinNhanMapping : Profile
    {
        public TinNhanMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== GỬI TIN NHẮN =====
            CreateMap<GuiTinNhanRequest, TinNhan>()
                .ForMember(d => d.MaCuocTroChuyen,
                    o => o.MapFrom(s => s.MaCuocTroChuyen))
                .ForMember(d => d.NoiDung,
                    o => o.MapFrom(s => s.NoiDung))
                .ForMember(d => d.LoaiTinNhan,
                    o => o.MapFrom(s => s.LoaiTinNhan.ToString()))
                .ForMember(d => d.DuongDanFile,
                    o => o.MapFrom(s => s.DuongDanFile))
                .ForMember(d => d.TenFile,
                    o => o.MapFrom(s => s.TenFile))
                .ForMember(d => d.KichThuocFile,
                    o => o.MapFrom(s => s.KichThuocFile))
                .ForMember(d => d.MaStickerDung,
                    o => o.MapFrom(s => s.MaStickerDung))
                .ForMember(d => d.MaBoDeDinhKem,
                    o => o.MapFrom(s => s.MaBoDeDinhKem))
                .ForMember(d => d.MaThachDauDinhKem,
                    o => o.MapFrom(s => s.MaThachDauDinhKem))
                .ForMember(d => d.ReplyToId,
                    o => o.MapFrom(s => s.ReplyToId))
                .ForMember(d => d.ThoiGianGui,
                    o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.DaThuHoi,
                    o => o.MapFrom(_ => false))

                // lấy từ context
                .ForMember(d => d.MaNguoiGui, o => o.Ignore())

                // navigation
                .ForMember(d => d.MaCuocTroChuyenNavigation, o => o.Ignore())
                .ForMember(d => d.ReplyTo, o => o.Ignore())
                .ForMember(d => d.InverseReplyTo, o => o.Ignore())
                .ForMember(d => d.DaXemTinNhans, o => o.Ignore())
                .ForMember(d => d.ReactionTinNhans, o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== TIN NHẮN CHI TIẾT =====
            CreateMap<TinNhan, TinNhanResponse>()
                .ForMember(d => d.LoaiTinNhan,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiTinNhanEnum>(s.LoaiTinNhan!)))
                .ForMember(d => d.DaThuHoi,
                    o => o.MapFrom(s => s.DaThuHoi ?? false))
                .ForMember(d => d.KichThuocFileHienThi,
                    o => o.MapFrom(s =>
                        MappingHelpers.FormatFileSize(s.KichThuocFile)))

                // user-context
                .ForMember(d => d.LaTinCuaToi, o => o.Ignore())
                .ForMember(d => d.ReactionCuaToi, o => o.Ignore())

                // đã xem
                .ForMember(d => d.DaXem, o => o.Ignore())
                .ForMember(d => d.SoNguoiDaXem, o => o.Ignore())
                .ForMember(d => d.NguoiDaXem, o => o.Ignore())

                // reaction
                .ForMember(d => d.Reactions, o => o.Ignore())
                .ForMember(d => d.TongReaction, o => o.Ignore())

                // navigation
                .ForMember(d => d.NguoiGui, o => o.Ignore())
                .ForMember(d => d.Sticker, o => o.Ignore())
                .ForMember(d => d.BoDeDinhKem, o => o.Ignore())
                .ForMember(d => d.ThachDauDinhKem, o => o.Ignore())
                .ForMember(d => d.ReplyTo, o => o.Ignore());

            // ===== TIN NHẮN REPLY =====
            CreateMap<TinNhan, TinNhanReplyResponse>()
                .ForMember(d => d.NoiDungRutGon,
                    o => o.MapFrom(s =>
                        MappingHelpers.Truncate(s.NoiDung, 100)))
                .ForMember(d => d.LoaiTinNhan,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiTinNhanEnum>(s.LoaiTinNhan!)))
                .ForMember(d => d.DaThuHoi,
                    o => o.MapFrom(s => s.DaThuHoi ?? false))
                .ForMember(d => d.NguoiGui, o => o.Ignore());

            // ===== TIN NHẮN TÓM TẮT =====
            CreateMap<TinNhan, TinNhanTomTatResponse>()
                .ForMember(d => d.NoiDungRutGon,
                    o => o.MapFrom(s =>
                        MappingHelpers.Truncate(s.NoiDung, 80)))
                .ForMember(d => d.LoaiTinNhan,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiTinNhanEnum>(s.LoaiTinNhan!)))
                .ForMember(d => d.DaThuHoi,
                    o => o.MapFrom(s => s.DaThuHoi ?? false))

                // navigation
                .ForMember(d => d.TenNguoiGui, o => o.Ignore());
        }

        #endregion
    }
}
