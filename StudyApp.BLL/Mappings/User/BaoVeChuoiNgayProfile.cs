using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class BaoVeChuoiNgayProfile : Profile
    {
        public BaoVeChuoiNgayProfile()
        {
            // =====================================================
            // REQUEST -> ENTITY (Sử dụng bảo vệ chuỗi ngày)
            // =====================================================
            CreateMap<SuDungBaoVeChuoiNgayRequest, BaoVeChuoiNgay>()
                .ForMember(dest => dest.LoaiBaoVe,
                    opt => opt.MapFrom(src => src.LoaiBaoVe.ToString()))
                .ForMember(dest => dest.MaNguoiDungNavigation,
                    opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // =====================================================
            CreateMap<BaoVeChuoiNgay, BaoVeChuoiNgayResponse>()
                .ForMember(dest => dest.LoaiBaoVe,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<LoaiBaoVeChuoiEnum>(src.LoaiBaoVe ?? "Freeze")));
        }
    }
}
