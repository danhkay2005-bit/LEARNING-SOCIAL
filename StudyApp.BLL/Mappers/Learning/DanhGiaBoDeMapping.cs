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
    /// Mapping cho Đánh giá bộ đề (DanhGiaBoDe)
    /// </summary>
    public class DanhGiaBoDeMapping : Profile
    {
        public DanhGiaBoDeMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // DanhGiaBoDeRequest -> DanhGiaBoDe
            // =====================================================
            CreateMap<DanhGiaBoDeRequest, DanhGiaBoDe>()
                .ForMember(dest => dest.MaDanhGia, opt => opt.Ignore())       // identity
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())     // lấy từ context
                .ForMember(dest => dest.SoLuotThich, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.DoKhoThucTe,
                    opt => opt.MapFrom(src =>
                        src.DoKhoThucTe.HasValue
                            ? (byte?)src.DoKhoThucTe.Value
                            : null))
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // CapNhatDanhGiaRequest -> DanhGiaBoDe
            // =====================================================
            CreateMap<CapNhatDanhGiaRequest, DanhGiaBoDe>()
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
                .ForMember(dest => dest.ThoiGian, opt => opt.Ignore()) // không đổi thời gian tạo
                .ForMember(dest => dest.DoKhoThucTe,
                    opt => opt.MapFrom(src =>
                        src.DoKhoThucTe.HasValue
                            ? (byte?)src.DoKhoThucTe.Value
                            : null))
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // DanhGiaBoDe -> DanhGiaBoDeResponse
            // =====================================================
            CreateMap<DanhGiaBoDe, DanhGiaBoDeResponse>()
                .ForMember(dest => dest.DoKhoThucTe,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKhoThucTe)))
                .ForMember(dest => dest.SoLuotThich,
                    opt => opt.MapFrom(src => src.SoLuotThich ?? 0))
                .ForMember(dest => dest.NguoiDanhGia, opt => opt.Ignore()) // service set
                .ForMember(dest => dest.DaThich, opt => opt.Ignore())      // service set
                .ForMember(dest => dest.LaCuaToi, opt => opt.Ignore());    // service set

            // =====================================================
            // ENTITY -> RESPONSE
            // DanhGiaBoDe -> KetQuaDanhGiaResponse
            // =====================================================
            CreateMap<DanhGiaBoDe, KetQuaDanhGiaResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.DanhGia, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DanhSachDanhGiaBoDeResponse
            // =====================================================
            CreateMap<List<DanhGiaBoDe>, DanhSachDanhGiaBoDeResponse>()
                .ForMember(dest => dest.DanhGias,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore())
                .ForMember(dest => dest.ThongKe, opt => opt.Ignore()); // service set

            // =====================================================
            // LIST ENTITY -> ThongKeDanhGiaResponse
            // (dùng khi cần map riêng)
            // =====================================================
            CreateMap<List<DanhGiaBoDe>, ThongKeDanhGiaResponse>()
                .ForMember(dest => dest.TongSoDanhGia,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.DiemTrungBinh,
                    opt => opt.MapFrom(src =>
                        src.Any() ? Math.Round(src.Average(x => x.SoSao), 2) : 0))
                .ForMember(dest => dest.So5Sao, opt => opt.MapFrom(src => src.Count(x => x.SoSao == 5)))
                .ForMember(dest => dest.So4Sao, opt => opt.MapFrom(src => src.Count(x => x.SoSao == 4)))
                .ForMember(dest => dest.So3Sao, opt => opt.MapFrom(src => src.Count(x => x.SoSao == 3)))
                .ForMember(dest => dest.So2Sao, opt => opt.MapFrom(src => src.Count(x => x.SoSao == 2)))
                .ForMember(dest => dest.So1Sao, opt => opt.MapFrom(src => src.Count(x => x.SoSao == 1)))
                .ForMember(dest => dest.PhanTram5Sao,
                    opt => opt.MapFrom(src => MappingHelpers.CalculatePercentage(src.Count(x => x.SoSao == 5), src.Count)))
                .ForMember(dest => dest.PhanTram4Sao,
                    opt => opt.MapFrom(src => MappingHelpers.CalculatePercentage(src.Count(x => x.SoSao == 4), src.Count)))
                .ForMember(dest => dest.PhanTram3Sao,
                    opt => opt.MapFrom(src => MappingHelpers.CalculatePercentage(src.Count(x => x.SoSao == 3), src.Count)))
                .ForMember(dest => dest.PhanTram2Sao,
                    opt => opt.MapFrom(src => MappingHelpers.CalculatePercentage(src.Count(x => x.SoSao == 2), src.Count)))
                .ForMember(dest => dest.PhanTram1Sao,
                    opt => opt.MapFrom(src => MappingHelpers.CalculatePercentage(src.Count(x => x.SoSao == 1), src.Count)));
        }
    }
}
