using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class MucTieuCaNhanMapping : Profile
    {
        public MucTieuCaNhanMapping()
        {
            CreateMap<TaoMucTieuRequest, MucTieuCaNhan>()
                .ForMember(d => d.MaMucTieu, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore()) // Gán từ User ID trong Service
                .ForMember(d => d.LoaiMucTieu, o => o.MapFrom(s => s.LoaiMucTieu.ToString()))
                .ForMember(d => d.GiaTriHienTai, o => o.MapFrom(_ => 0))
                .ForMember(d => d.DaHoanThanh, o => o.MapFrom(_ => false))
                .ForMember(d => d.NgayHoanThanh, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CapNhatMucTieuRequest, MucTieuCaNhan>()
                .ForMember(d => d.MaMucTieu, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.LoaiMucTieu, o => o.Ignore()) // Không đổi loại mục tiêu khi đã tạo
                .ForMember(d => d.NgayBatDau, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<MucTieuCaNhan, MucTieuCaNhanResponse>()
                .ForMember(d => d.LoaiMucTieu, o => o.MapFrom(s => ParseEnum<LoaiMucTieuEnum>(s.LoaiMucTieu)))
                .ForMember(d => d.GiaTriHienTai, o => o.MapFrom(s => s.GiaTriHienTai ?? 0))
                .ForMember(d => d.DaHoanThanh, o => o.MapFrom(s => s.DaHoanThanh ?? false))
                .ForMember(d => d.TienDoPhanTram, o => o.MapFrom(s => CalculateProgress(s.GiaTriHienTai, s.GiaTriMucTieu)))
                .ForMember(d => d.SoNgayConLai, o => o.MapFrom(s => CalculateDaysRemaining(s.NgayKetThuc)));
        }
    }
}
