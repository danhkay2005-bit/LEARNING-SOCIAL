using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using StudyApp.DTO.Enums;

namespace StudyApp.BLL.Mappers.Learning
{
    public class CheDoHocCaNhanMapping : Profile
    {
        public CheDoHocCaNhanMapping()
        {
            // ============================
            // REQUEST → ENTITY
            // ============================

            // Cập nhật chế độ học cá nhân
            CreateMap<CapNhatCheDoHocRequest, CheDoHocCaNhan>()
                .ForMember(dest => dest.ThuTuHoc,
                    opt => opt.MapFrom(src => src.ThuTuHoc != null
                        ? src.ThuTuHoc.ToString()
                        : null))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore());

            // ============================
            // ENTITY → RESPONSE
            // ============================

            CreateMap<CheDoHocCaNhan, CheDoHocCaNhanResponse>()
                .ForMember(dest => dest.ThuTuHoc,
                    opt => opt.MapFrom(src => MappingHelpers.ParseThuTuHoc(src.ThuTuHoc)))

                // Giá trị mặc định an toàn
                .ForMember(dest => dest.SoTheMoiMoiNgay,
                    opt => opt.MapFrom(src => src.SoTheMoiMoiNgay ?? 20))
                .ForMember(dest => dest.SoTheOnTapToiDa,
                    opt => opt.MapFrom(src => src.SoTheOnTapToiDa ?? 100))
                .ForMember(dest => dest.ThoiGianHienCauHoi,
                    opt => opt.MapFrom(src => src.ThoiGianHienCauHoi ?? 5))
                .ForMember(dest => dest.ThoiGianHienDapAn,
                    opt => opt.MapFrom(src => src.ThoiGianHienDapAn ?? 5))
                .ForMember(dest => dest.ThoiGianMoiTheToiDa,
                    opt => opt.MapFrom(src => src.ThoiGianMoiTheToiDa ?? 60))

                // Bool mặc định
                .ForMember(dest => dest.UuTienTheKho,
                    opt => opt.MapFrom(src => src.UuTienTheKho ?? false))
                .ForMember(dest => dest.UuTienTheSapHetHan,
                    opt => opt.MapFrom(src => src.UuTienTheSapHetHan ?? false))
                .ForMember(dest => dest.TronDapAnTracNghiem,
                    opt => opt.MapFrom(src => src.TronDapAnTracNghiem ?? true))
                .ForMember(dest => dest.HienGoiY,
                    opt => opt.MapFrom(src => src.HienGoiY ?? true))
                .ForMember(dest => dest.HienGiaiThich,
                    opt => opt.MapFrom(src => src.HienGiaiThich ?? true))
                .ForMember(dest => dest.HienThongKeSauPhien,
                    opt => opt.MapFrom(src => src.HienThongKeSauPhien ?? true))
                .ForMember(dest => dest.BatAmThanh,
                    opt => opt.MapFrom(src => src.BatAmThanh ?? true))
                .ForMember(dest => dest.TuDongPhatAm,
                    opt => opt.MapFrom(src => src.TuDongPhatAm ?? false));
        }
    }
}