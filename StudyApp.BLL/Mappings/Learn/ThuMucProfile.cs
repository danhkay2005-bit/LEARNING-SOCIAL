using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class ThuMucProfile : Profile
    {
        public ThuMucProfile()
        {
            // =================================================
            // CREATE (TaoThuMucRequest -> Entity)
            // =================================================
            CreateMap<TaoThuMucRequest, ThuMuc>()
                .ForMember(dest => dest.MaThuMuc, opt => opt.Ignore())          // DB tự sinh
                .ForMember(dest => dest.ThoiGianTao,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.Ignore())
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaThuMucChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThuMucChaNavigation, opt => opt.Ignore());

            // =================================================
            // UPDATE (CapNhatThuMucRequest -> Entity)
            // =================================================
            CreateMap<CapNhatThuMucRequest, ThuMuc>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())       // Không đổi chủ sở hữu
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianCapNhat,
                           opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaThuMucChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThuMucChaNavigation, opt => opt.Ignore());

            // =================================================
            // ENTITY -> RESPONSE (FULL)
            // =================================================
            CreateMap<ThuMuc, ThuMucResponse>();

            // =================================================
            // ENTITY -> RESPONSE (SELECT)
            // =================================================
            CreateMap<ThuMuc, ThuMucSelectResponse>();

            // =================================================
            // ENTITY -> TREE RESPONSE
            // (map đệ quy – children)
            // =================================================
            CreateMap<ThuMuc, ThuMucTreeResponse>()
                .ForMember(dest => dest.ThuMucCon,
                           opt => opt.MapFrom(src => src.InverseMaThuMucChaNavigation));
        }
    }
}
