using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;

namespace StudyApp.BLL.Mappers.Learning
{
    public class BoDeHocMapping : Profile
    {
        public BoDeHocMapping()
        {
            // ============================
            // REQUEST → ENTITY
            // ============================

            // Tạo bộ đề
            CreateMap<TaoBoDeRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (byte?)src.MucDoKho))
                .ForMember(dest => dest.LaCongKhai,
                    opt => opt.MapFrom(src => src.LaCongKhai))
                .ForMember(dest => dest.ChoPhepBinhLuan,
                    opt => opt.MapFrom(src => src.ChoPhepBinhLuan))
                .ForMember(dest => dest.SoLuongThe,
                    opt => opt.MapFrom(src => src.DanhSachThe != null ? src.DanhSachThe.Count : 0))
                .ForMember(dest => dest.SoLuotHoc, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DiemDanhGiaTb, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoDanhGia, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now));

            // Cập nhật bộ đề
            CreateMap<CapNhatBoDeRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (byte?)src.MucDoKho))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // Xóa bộ đề (xóa mềm)
            CreateMap<XoaBoDeRequest, BoDeHoc>()
                .ForMember(dest => dest.DaXoa,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianCapNhat,
                    opt => opt.MapFrom(_ => DateTime.Now));

            // Clone bộ đề
            CreateMap<CloneBoDeRequest, BoDeHoc>()
                .ForMember(dest => dest.MaBoDeGoc,
                    opt => opt.MapFrom(src => src.MaBoDeGoc))
                .ForMember(dest => dest.TieuDe,
                    opt => opt.MapFrom(src => src.TieuDeMoi))
                .ForMember(dest => dest.SoLuotHoc, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoLuotChiaSe, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DiemDanhGiaTb, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.SoDanhGia, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ThoiGianTao,
                    opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.DaXoa,
                    opt => opt.MapFrom(_ => false));

            // ============================
            // ENTITY → RESPONSE
            // ============================

            // Bộ đề chi tiết
            CreateMap<BoDeHoc, BoDeHocResponse>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (MucDoKhoEnum?)src.MucDoKho))
                .ForMember(dest => dest.SoLuongThe,
                    opt => opt.MapFrom(src => src.SoLuongThe ?? 0))
                .ForMember(dest => dest.SoLuotHoc,
                    opt => opt.MapFrom(src => src.SoLuotHoc ?? 0))
                .ForMember(dest => dest.SoLuotChiaSe,
                    opt => opt.MapFrom(src => src.SoLuotChiaSe ?? 0))
                .ForMember(dest => dest.DiemDanhGiaTb,
                    opt => opt.MapFrom(src => src.DiemDanhGiaTb ?? 0))
                .ForMember(dest => dest.SoDanhGia,
                    opt => opt.MapFrom(src => src.SoDanhGia ?? 0))
                // Các field phụ thuộc user context → Service xử lý
                .ForMember(dest => dest.LaCuaToi, opt => opt.Ignore())
                .ForMember(dest => dest.DaLuuYeuThich, opt => opt.Ignore())
                .ForMember(dest => dest.DaHoc, opt => opt.Ignore())
                .ForMember(dest => dest.DaDanhGia, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHoc, opt => opt.Ignore())
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore())
                .ForMember(dest => dest.ChuDe, opt => opt.Ignore())
                .ForMember(dest => dest.ThuMuc, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.TheMau, opt => opt.Ignore())
                .ForMember(dest => dest.BoDeGoc, opt => opt.Ignore());

            // Bộ đề tóm tắt
            CreateMap<BoDeHoc, BoDeHocTomTatResponse>()
                .ForMember(dest => dest.MucDoKho,
                    opt => opt.MapFrom(src => (MucDoKhoEnum?)src.MucDoKho))
                .ForMember(dest => dest.SoLuongThe,
                    opt => opt.MapFrom(src => src.SoLuongThe ?? 0))
                .ForMember(dest => dest.SoLuotHoc,
                    opt => opt.MapFrom(src => src.SoLuotHoc ?? 0))
                .ForMember(dest => dest.DiemDanhGiaTb,
                    opt => opt.MapFrom(src => src.DiemDanhGiaTb ?? 0))
                .ForMember(dest => dest.SoDanhGia,
                    opt => opt.MapFrom(src => src.SoDanhGia ?? 0))
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore())
                .ForMember(dest => dest.DaLuuYeuThich, opt => opt.Ignore())
                .ForMember(dest => dest.TienDoHocPhanTram, opt => opt.Ignore());

            // Bộ đề gốc tóm tắt
            CreateMap<BoDeHoc, BoDeGocTomTatResponse>()
                .ForMember(dest => dest.ConTonTai,
                    opt => opt.MapFrom(src => src.DaXoa != true))
                .ForMember(dest => dest.NguoiTao, opt => opt.Ignore());
        }
    }
}
