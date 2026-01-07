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
    /// Mapping cho Thư mục (ThuMuc)
    /// </summary>
    public class ThuMucMapping : Profile
    {
        public ThuMucMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // TẠO THƯ MỤC
            // =====================================================
            CreateMap<TaoThuMucRequest, ThuMuc>()
                .ForMember(dest => dest.MaThuMuc, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // từ context
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.Ignore())
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaThuMucChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThuMucChaNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // CẬP NHẬT THƯ MỤC
            // =====================================================
            CreateMap<CapNhatThuMucRequest, ThuMuc>()
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.BoDeHocs, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaThuMucChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaThuMucChaNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // THƯ MỤC CHI TIẾT
            // =====================================================
            CreateMap<ThuMuc, ThuMucResponse>()
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForMember(dest => dest.SoBoDeTrongThuMuc,
                    opt => opt.MapFrom(src => src.BoDeHocs.Count))
                .ForMember(dest => dest.SoThuMucCon,
                    opt => opt.MapFrom(src => src.InverseMaThuMucChaNavigation.Count))
                // danh sách con → service quyết định có load hay không
                .ForMember(dest => dest.ThuMucCons, opt => opt.Ignore())
                .ForMember(dest => dest.BoDes, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // THƯ MỤC TÓM TẮT
            // =====================================================
            CreateMap<ThuMuc, ThuMucTomTatResponse>()
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForMember(dest => dest.SoBoDe,
                    opt => opt.MapFrom(src => src.BoDeHocs.Count));

            // =====================================================
            // ENTITY -> RESPONSE
            // NODE CÂY THƯ MỤC
            // (service build tree, mapper map node)
            // =====================================================
            CreateMap<ThuMuc, ThuMucNodeResponse>()
                .ForMember(dest => dest.ThuTu,
                    opt => opt.MapFrom(src => src.ThuTu ?? 0))
                .ForMember(dest => dest.SoBoDe,
                    opt => opt.MapFrom(src => src.BoDeHocs.Count))
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // KẾT QUẢ THƯ MỤC
            // =====================================================
            CreateMap<ThuMuc, KetQuaThuMucResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.ThuMuc, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> CÂY THƯ MỤC RESPONSE (wrapper)
            // =====================================================
            CreateMap<List<ThuMuc>, CayThuMucResponse>()
                .ForMember(dest => dest.Nodes, opt => opt.Ignore()) // service build tree
                .ForMember(dest => dest.TongSoThuMuc,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.TongSoBoDe,
                    opt => opt.MapFrom(src => src.Sum(x => x.BoDeHocs.Count)));
        }
    }
}
