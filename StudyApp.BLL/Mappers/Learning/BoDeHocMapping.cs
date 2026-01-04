using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BoDeHocMapping : Profile
    {
        public BoDeHocMapping()
        {
            
        CreateMap<TaoBoDeRequest, BoDeHoc>()
            .ForMember(d => d.MaBoDe, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.MucDoKho, o => o.MapFrom(s => s.MucDoKho.HasValue ? (byte)s.MucDoKho.Value : (byte?)null))
            .ForMember(d => d.SoLuongThe, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLuotHoc, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLuotChiaSe, o => o.MapFrom(_ => 0))
            .ForMember(d => d.DiemDanhGiaTb, o => o.MapFrom(_ => 0.0))
            .ForMember(d => d.SoDanhGia, o => o.MapFrom(_ => 0))
            .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CapNhatBoDeRequest, BoDeHoc>()
                .ForMember(d => d.MaBoDe, o => o.Ignore())
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.MucDoKho, o => o.MapFrom((s, d) => s.MucDoKho.HasValue ? (byte)s.MucDoKho.Value : d.MucDoKho))
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

            CreateMap<BoDeHoc, BoDeHocResponse>()
                .ForMember(d => d.MucDoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.MucDoKho)))
                .ForMember(d => d.LaCongKhai, o => o.MapFrom(s => s.LaCongKhai ?? true))
                .ForMember(d => d.ChoPhepBinhLuan, o => o.MapFrom(s => s.ChoPhepBinhLuan ?? true))
                .ForMember(d => d.SoLuongThe, o => o.MapFrom(s => s.SoLuongThe ?? 0))
                .ForMember(d => d.SoLuotHoc, o => o.MapFrom(s => s.SoLuotHoc ?? 0))
                .ForMember(d => d.SoLuotChiaSe, o => o.MapFrom(s => s.SoLuotChiaSe ?? 0))
                .ForMember(d => d.DiemDanhGiaTb, o => o.MapFrom(s => s.DiemDanhGiaTb ?? 0))
                .ForMember(d => d.SoDanhGia, o => o.MapFrom(s => s.SoDanhGia ?? 0))
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.TagBoDes.Select(t => t.MaTagNavigation)))
                .ForMember(d => d.NguoiTao, o => o.Ignore())
                .ForMember(d => d.ChuDe, o => o.Ignore())
                .ForMember(d => d.ThuMuc, o => o.Ignore())
                .ForMember(d => d.BoDeGoc, o => o.Ignore())
                .ForMember(d => d.LaCuaToi, o => o.Ignore())
                .ForMember(d => d.DaLuuYeuThich, o => o.Ignore())
                .ForMember(d => d.DaHoc, o => o.Ignore())
                .ForMember(d => d.DaDanhGia, o => o.Ignore())
                .ForMember(d => d.TienDoHoc, o => o.Ignore())
                .ForMember(d => d.TheMau, o => o.Ignore());

            CreateMap<BoDeHoc, BoDeHocTomTatResponse>()
                .ForMember(d => d.MucDoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.MucDoKho)))
                .ForMember(d => d.SoLuongThe, o => o.MapFrom(s => s.SoLuongThe ?? 0))
                .ForMember(d => d.SoLuotHoc, o => o.MapFrom(s => s.SoLuotHoc ?? 0))
                .ForMember(d => d.DiemDanhGiaTb, o => o.MapFrom(s => s.DiemDanhGiaTb ?? 0))
                .ForMember(d => d.SoDanhGia, o => o.MapFrom(s => s.SoDanhGia ?? 0))
                .ForMember(d => d.NguoiTao, o => o.Ignore())
                .ForMember(d => d.DaLuuYeuThich, o => o.Ignore())
                .ForMember(d => d.TienDoHocPhanTram, o => o.Ignore());

            CreateMap<BoDeHoc, BoDeGocTomTatResponse>()
                .ForMember(d => d.ConTonTai, o => o.MapFrom(s => !(s.DaXoa ?? false)))
                .ForMember(d => d.NguoiTao, o => o.Ignore());
            }
        }
    }

