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
    /// Mapping cho Reaction (Bài đăng & Tin nhắn)
    /// </summary>
    public class ReactionMapping : Profile
    {
        public ReactionMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            // ===== REACTION BÀI ĐĂNG =====
            CreateMap<ReactionBaiDangRequest, ReactionBaiDang>()
                .ForMember(d => d.MaBaiDang, o => o.MapFrom(s => s.MaBaiDang))
                .ForMember(d => d.LoaiReaction,
                    o => o.MapFrom(s => s.LoaiReaction.ToString()))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // lấy từ context
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaBaiDangNavigation, o => o.Ignore());

            // ===== REACTION TIN NHẮN =====
            CreateMap<ReactionTinNhanRequest, ReactionTinNhan>()
                .ForMember(d => d.MaTinNhan, o => o.MapFrom(s => s.MaTinNhan))
                .ForMember(d => d.Emoji, o => o.MapFrom(s => s.Emoji))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // lấy từ context
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaTinNhanNavigation, o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== REACTION BÀI ĐĂNG =====
            CreateMap<ReactionBaiDang, ReactionBaiDangChiTietResponse>()
                .ForMember(d => d.MaBaiDang,
                    o => o.MapFrom(s => s.MaBaiDang))
                .ForMember(d => d.MaNguoiDung,
                    o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.LoaiReaction,
                    o => o.MapFrom(s =>
                        Enum.Parse<LoaiReactionEnum>(s.LoaiReaction!)))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // navigation
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());

            // ===== REACTION TIN NHẮN =====
            CreateMap<ReactionTinNhan, ReactionTinNhanChiTietResponse>()
                .ForMember(d => d.MaTinNhan,
                    o => o.MapFrom(s => s.MaTinNhan))
                .ForMember(d => d.MaNguoiDung,
                    o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.Emoji,
                    o => o.MapFrom(s => s.Emoji))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // navigation
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());
        }

        #endregion
    }
}
