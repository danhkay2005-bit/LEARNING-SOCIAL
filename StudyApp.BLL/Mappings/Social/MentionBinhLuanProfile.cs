using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappings.Social
{
    public class MentionBinhLuanProfile : Profile
    {
        public MentionBinhLuanProfile()
        {
            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<MentionBinhLuan, MentionBinhLuanResponse>();
        }
    }
}
