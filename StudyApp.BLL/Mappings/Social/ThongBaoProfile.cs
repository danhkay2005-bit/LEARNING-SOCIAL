using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Enums.StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    public class ThongBaoProfile : Profile
    {
        public ThongBaoProfile()
        {
            // =====================================================
            // ENTITY -> RESPONSE (đầy đủ)
            // =====================================================
            CreateMap<ThongBao, ThongBaoResponse>()
                .ForMember(dest => dest.LoaiThongBao,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiThongBaoEnum>(src.LoaiThongBao.ToString())));

            // =====================================================
            // ENTITY -> RESPONSE (rút gọn)
            // =====================================================
            CreateMap<ThongBao, ThongBaoSummaryResponse>()
                .ForMember(dest => dest.LoaiThongBao,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiThongBaoEnum>(src.LoaiThongBao.ToString())));
        }
    }
}
