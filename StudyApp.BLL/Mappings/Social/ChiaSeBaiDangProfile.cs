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
            // REQUEST -> ENTITY
            // =================================================
            CreateMap<ChiaSeBaiDangRequest, ChiaSeBaiDang>()
                .ForMember(dest => dest.MaChiaSe, opt => opt.Ignore()) // DB tự sinh
                .ForMember(dest => dest.ThoiGian, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaBaiDangMoi, opt => opt.Ignore()) // Sẽ set sau khi tạo bài đăng mới
                .ForMember(dest => dest.MaBaiDangGocNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangMoiNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<ChiaSeBaiDang, ChiaSeBaiDangResponse>()
                .ForMember(dest => dest.BaiDangGoc, 
                    opt => opt.MapFrom(src => src.MaBaiDangGocNavigation))
                .ForMember(dest => dest.BaiDangMoi, 
                    opt => opt.MapFrom(src => src.MaBaiDangMoiNavigation));
        }
    }
}
