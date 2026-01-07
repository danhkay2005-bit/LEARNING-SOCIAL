using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Đánh dấu thẻ (DanhDauThe)
    /// </summary>
    public class DanhDauTheMapping : Profile
    {
        public DanhDauTheMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // DanhDauTheRequest -> DanhDauThe
            // =====================================================
            CreateMap<DanhDauTheRequest, DanhDauThe>()
                .ForMember(dest => dest.MaDanhDau, opt => opt.Ignore())     // identity
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())   // lấy từ context
                .ForMember(dest => dest.LoaiDanhDau,
                    opt => opt.MapFrom(src => src.LoaiDanhDau.ToString()))
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // DanhDauThe -> DanhDauTheResponse
            // =====================================================
            CreateMap<DanhDauThe, DanhDauTheResponse>()
                .ForMember(dest => dest.The,
                    opt => opt.MapFrom(src => src.MaTheNavigation))
                .ForMember(dest => dest.LoaiDanhDau,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ParseEnum<LoaiDanhDauTheEnum>(src.LoaiDanhDau)));

            // =====================================================
            // ENTITY -> RESPONSE
            // DanhDauThe -> KetQuaDanhDauTheResponse
            // =====================================================
            CreateMap<DanhDauThe, KetQuaDanhDauTheResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.DanhDau, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DanhSachTheDanhDauResponse
            // =====================================================
            CreateMap<List<DanhDauThe>, DanhSachTheDanhDauResponse>()
                .ForMember(dest => dest.DanhDaus,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore())
                .ForMember(dest => dest.ThongKeTheoLoai,
                    opt => opt.MapFrom(src =>
                        src.GroupBy(x =>
                                MappingHelpers.ParseEnum<LoaiDanhDauTheEnum>(x.LoaiDanhDau))
                           .Where(g => g.Key != default)
                           .ToDictionary(g => g.Key, g => g.Count())));
        }
    }
}
