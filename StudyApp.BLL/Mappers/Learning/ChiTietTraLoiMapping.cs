using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using StudyApp.DTO.Enums;

namespace StudyApp.BLL.Mappers.Learning
{
    public class ChiTietTraLoiMapping : Profile
    {
        public ChiTietTraLoiMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // GuiCauTraLoiRequest -> ChiTietTraLoi
            // =====================================================
            CreateMap<GuiCauTraLoiRequest, ChiTietTraLoi>()
                .ForMember(dest => dest.MaTraLoi, opt => opt.Ignore())
                .ForMember(dest => dest.TraLoiDung, opt => opt.Ignore())   // server chấm
                .ForMember(dest => dest.DapAnDung, opt => opt.Ignore())    // server set
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.DoKhoUserDanhGia,
                    opt => opt.MapFrom(src =>
                        src.DoKhoUserDanhGia.HasValue
                            ? (byte?)src.DoKhoUserDanhGia.Value
                            : null))
                .ForMember(dest => dest.MaPhienNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // ChiTietTraLoi -> ChiTietTraLoiResponse
            // =====================================================
            CreateMap<ChiTietTraLoi, ChiTietTraLoiResponse>()
                .ForMember(dest => dest.The,
                    opt => opt.MapFrom(src => src.MaTheNavigation))
                .ForMember(dest => dest.DoKhoUserDanhGia,
                    opt => opt.MapFrom(src =>
                        MappingHelpers.ByteToEnum<MucDoKhoEnum>(src.DoKhoUserDanhGia)));

            // =====================================================
            // ENTITY -> RESPONSE
            // ChiTietTraLoi -> GuiCauTraLoiResponse
            // =====================================================
            CreateMap<ChiTietTraLoi, GuiCauTraLoiResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.TraLoiDung, opt => opt.MapFrom(src => src.TraLoiDung))
                .ForMember(dest => dest.DapAnDung, opt => opt.MapFrom(src => src.DapAnDung))
                .ForMember(dest => dest.GiaiThich, opt => opt.Ignore())          // lấy từ The
                .ForMember(dest => dest.DiemNhan, opt => opt.Ignore())           // service set
                .ForMember(dest => dest.TrangThaiSRSMoi, opt => opt.Ignore())    // service set
                .ForMember(dest => dest.TheIndex, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoThe, opt => opt.Ignore());

            // =====================================================
            // LIST ENTITY -> DanhSachChiTietTraLoiResponse
            // =====================================================
            CreateMap<List<ChiTietTraLoi>, DanhSachChiTietTraLoiResponse>()
                .ForMember(dest => dest.MaPhien,
                    opt => opt.MapFrom(src => src.Any() ? src.First().MaPhien : 0))
                .ForMember(dest => dest.ChiTietTraLois,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SoDung,
                    opt => opt.MapFrom(src => src.Count(x => x.TraLoiDung)))
                .ForMember(dest => dest.SoSai,
                    opt => opt.MapFrom(src => src.Count(x => !x.TraLoiDung)));
        }
    }
}
