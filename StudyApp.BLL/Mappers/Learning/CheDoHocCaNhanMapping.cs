using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class CheDoHocCaNhanMapping : Profile
    {
        public CheDoHocCaNhanMapping()
        {
            CreateMap<CapNhatCheDoHocRequest, CheDoHocCaNhan>()
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Thường lấy từ context người dùng
                .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.ThuTuHoc, o => o.MapFrom((s, d) => s.ThuTuHoc.HasValue ? s.ThuTuHoc.Value.ToString() : d.ThuTuHoc))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<CheDoHocCaNhan, CheDoHocCaNhanResponse>()
                .ForMember(d => d.SoTheMoiMoiNgay, o => o.MapFrom(s => s.SoTheMoiMoiNgay ?? 20))
                .ForMember(d => d.SoTheOnTapToiDa, o => o.MapFrom(s => s.SoTheOnTapToiDa ?? 100))
                .ForMember(d => d.ThoiGianHienCauHoi, o => o.MapFrom(s => s.ThoiGianHienCauHoi ?? 10))
                .ForMember(d => d.ThoiGianHienDapAn, o => o.MapFrom(s => s.ThoiGianHienDapAn ?? 5))
                .ForMember(d => d.ThoiGianMoiTheToiDa, o => o.MapFrom(s => s.ThoiGianMoiTheToiDa ?? 60))
                .ForMember(d => d.ThuTuHoc, o => o.MapFrom(s => ParseEnum<ThuTuHocEnum>(s.ThuTuHoc)))
                .ForMember(d => d.UuTienTheKho, o => o.MapFrom(s => s.UuTienTheKho ?? false))
                .ForMember(d => d.UuTienTheSapHetHan, o => o.MapFrom(s => s.UuTienTheSapHetHan ?? true))
                .ForMember(d => d.TronDapAnTracNghiem, o => o.MapFrom(s => s.TronDapAnTracNghiem ?? true))
                .ForMember(d => d.HienGoiY, o => o.MapFrom(s => s.HienGoiY ?? true))
                .ForMember(d => d.HienGiaiThich, o => o.MapFrom(s => s.HienGiaiThich ?? true))
                .ForMember(d => d.HienThongKeSauPhien, o => o.MapFrom(s => s.HienThongKeSauPhien ?? true))
                .ForMember(d => d.BatAmThanh, o => o.MapFrom(s => s.BatAmThanh ?? true))
                .ForMember(d => d.TuDongPhatAm, o => o.MapFrom(s => s.TuDongPhatAm ?? false));
        }
    }
}
