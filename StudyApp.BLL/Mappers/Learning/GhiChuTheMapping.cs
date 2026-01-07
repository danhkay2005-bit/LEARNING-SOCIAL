using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Ghi chú thẻ (GhiChuThe)
    /// </summary>
    public class GhiChuTheMapping : Profile
    {
        public GhiChuTheMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Tạo ghi chú thẻ
            // =====================================================
            CreateMap<TaoGhiChuTheRequest, GhiChuThe>()
                .ForMember(dest => dest.MaGhiChu, opt => opt.Ignore())        // identity
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())    // lấy từ context
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // Cập nhật ghi chú thẻ
            // =====================================================
            CreateMap<CapNhatGhiChuTheRequest, GhiChuThe>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianSua,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // GhiChuThe -> GhiChuTheResponse
            // =====================================================
            CreateMap<GhiChuThe, GhiChuTheResponse>();

            // =====================================================
            // ENTITY -> RESPONSE
            // GhiChuThe -> KetQuaGhiChuTheResponse
            // =====================================================
            CreateMap<GhiChuThe, KetQuaGhiChuTheResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.GhiChu, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DanhSachGhiChuTheResponse
            // =====================================================
            CreateMap<List<GhiChuThe>, DanhSachGhiChuTheResponse>()
                .ForMember(dest => dest.GhiChus,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count));
        }
    }
}
