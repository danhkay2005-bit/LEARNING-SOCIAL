using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class ChiaSeBaiDangProfile : Profile
    {
        public ChiaSeBaiDangProfile()
        {
            // =================================================
            // CREATE (ChiaSeBaiDangRequest -> Entity)
            // =================================================
            CreateMap<ChiaSeBaiDangRequest, ChiaSeBaiDang>()
                .ForMember(dest => dest.MaChiaSe, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.MaBaiDangMoi, opt => opt.Ignore())     // Bài mới do Service tạo
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.Now))

                // Navigation
                .ForMember(dest => dest.MaBaiDangGocNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangMoiNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<ChiaSeBaiDang, ChiaSeBaiDangResponse>();
        }
    }
}
