using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Mapping cho Mention (Bài đăng & Bình luận)
    /// </summary>
    public class MentionMapping : Profile
    {
        public MentionMapping()
        {
            MapMentionBaiDang();
            MapMentionBinhLuan();
        }

        private void MapMentionBaiDang()
        {
            CreateMap<MentionBaiDang, MentionBaiDangResponse>()
                .ForMember(d => d.MaBaiDang,
                    o => o.MapFrom(s => s.MaBaiDang))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // xử lý ở Service
                .ForMember(d => d.MaNguoiMention, o => o.Ignore())
                .ForMember(d => d.DaDoc, o => o.Ignore())
                .ForMember(d => d.BaiDang, o => o.Ignore())
                .ForMember(d => d.NguoiMention, o => o.Ignore());
        }

        private void MapMentionBinhLuan()
        {
            CreateMap<MentionBinhLuan, MentionBinhLuanResponse>()
                .ForMember(d => d.MaBinhLuan,
                    o => o.MapFrom(s => s.MaBinhLuan))
                .ForMember(d => d.ThoiGian,
                    o => o.MapFrom(s => s.ThoiGian))

                // lấy từ navigation / context
                .ForMember(d => d.MaBaiDang,
                    o => o.Ignore())
                .ForMember(d => d.MaNguoiMention,
                    o => o.Ignore())
                .ForMember(d => d.DaDoc,
                    o => o.Ignore())

                // navigation response
                .ForMember(d => d.BinhLuan,
                    o => o.Ignore())
                .ForMember(d => d.NguoiMention,
                    o => o.Ignore());
        }
    }
}
