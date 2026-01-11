using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappings.Social
{
    public class MentionBaiDangProfile : Profile
    {
        public MentionBaiDangProfile()
        {
            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<MentionBaiDang, MentionBaiDangResponse>();
        }
    }
}
