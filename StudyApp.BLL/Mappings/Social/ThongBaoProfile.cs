using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;  // ✅ Chỉ cần 1 using này
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                        (LoaiThongBaoEnum)src.LoaiThongBao));  // ✅ Cast trực tiếp từ int

            // =====================================================
            // ENTITY -> RESPONSE (rút gọn)
            // =====================================================
            CreateMap<ThongBao, ThongBaoSummaryResponse>()
                .ForMember(dest => dest.LoaiThongBao,
                    opt => opt.MapFrom(src =>
                        (LoaiThongBaoEnum)src.LoaiThongBao));  // ✅ Cast trực tiếp từ int
        }
    }
}