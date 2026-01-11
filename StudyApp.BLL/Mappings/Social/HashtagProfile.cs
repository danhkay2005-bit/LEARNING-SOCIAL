using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using StudyApp.DTO.Enums;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class HashtagProfile : Profile
    {
        public HashtagProfile()
        {
            // =================================================
            // UPDATE TRENDING (CapNhatTrangThaiHashtagRequest -> Entity)
            // =================================================
            CreateMap<CapNhatTrangThaiHashtagRequest, Hashtag>()
                .ForMember(dest => dest.DangThinhHanh,
                    opt => opt.MapFrom(src => src.DangThinhHanh))
                .ForMember(dest => dest.MaHashtag, opt => opt.Ignore())
                .ForMember(dest => dest.TenHashtag, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuotDung, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.MaBaiDangs, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE
            // =================================================
            CreateMap<Hashtag, HashtagResponse>()
                .ForMember(dest => dest.SoLuotDung,
                    opt => opt.MapFrom(src => src.SoLuotDung ?? 0))
                .ForMember(dest => dest.TrangThai,
                    opt => opt.MapFrom(src =>
                        (src.DangThinhHanh ?? false)
                            ? TrangThaiHashtagEnum.ThinhHanh
                            : TrangThaiHashtagEnum.BinhThuong));
        }
    }
}
