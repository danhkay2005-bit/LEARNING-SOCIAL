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
    /// Mapping cho Bình luận bộ đề & Thích bình luận
    /// </summary>
    public class BinhLuanBoDeMapping : Profile
    {
        public BinhLuanBoDeMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // TẠO BÌNH LUẬN
            // =====================================================
            CreateMap<TaoBinhLuanBoDeRequest, BinhLuanBoDe>()
                .ForMember(dest => dest.MaBinhLuan, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore()) // lấy từ context
                .ForMember(dest => dest.SoLuotThich, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGian,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ThoiGianSua, opt => opt.Ignore())
                .ForMember(dest => dest.InverseMaBinhLuanChaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ThichBinhLuanBoDes, opt => opt.Ignore())
                .ForMember(dest => dest.MaBoDeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MaBinhLuanChaNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // CẬP NHẬT BÌNH LUẬN
            // =====================================================
            CreateMap<CapNhatBinhLuanBoDeRequest, BinhLuanBoDe>()
                .ForMember(dest => dest.DaChinhSua, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThoiGianSua,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // BÌNH LUẬN BỘ ĐỀ
            // =====================================================
            CreateMap<BinhLuanBoDe, BinhLuanBoDeResponse>()
                // Ẩn nội dung nếu đã xoá
                .ForMember(dest => dest.NoiDung,
                    opt => opt.MapFrom(src =>
                        src.DaXoa == true ? "[Bình luận đã bị xóa]" : src.NoiDung))
                .ForMember(dest => dest.SoLuotThich,
                    opt => opt.MapFrom(src => src.SoLuotThich ?? 0))
                .ForMember(dest => dest.DaChinhSua,
                    opt => opt.MapFrom(src => src.DaChinhSua ?? false))
                .ForMember(dest => dest.NguoiBinhLuan, opt => opt.Ignore()) // service set
                .ForMember(dest => dest.DaThich, opt => opt.Ignore())       // service set
                .ForMember(dest => dest.LaCuaToi, opt => opt.Ignore())      // service set
                .ForMember(dest => dest.SoTraLoi,
                    opt => opt.MapFrom(src =>
                        src.InverseMaBinhLuanChaNavigation.Count(x => x.DaXoa != true)))
                .ForMember(dest => dest.TraLois,
                    opt => opt.MapFrom(src =>
                        src.InverseMaBinhLuanChaNavigation
                            .Where(x => x.DaXoa != true)
                            .OrderBy(x => x.ThoiGian)));

            // =====================================================
            // ENTITY -> RESPONSE
            // KẾT QUẢ BÌNH LUẬN
            // =====================================================
            CreateMap<BinhLuanBoDe, KetQuaBinhLuanBoDeResponse>()
                .ForMember(dest => dest.ThanhCong, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ThongBao, opt => opt.Ignore())
                .ForMember(dest => dest.BinhLuan, opt => opt.MapFrom(src => src));

            // =====================================================
            // LIST ENTITY -> DANH SÁCH BÌNH LUẬN
            // =====================================================
            CreateMap<List<BinhLuanBoDe>, DanhSachBinhLuanBoDeResponse>()
                .ForMember(dest => dest.BinhLuans,
                    opt => opt.MapFrom(src =>
                        src.Where(x => x.DaXoa != true && x.MaBinhLuanCha == null)))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src =>
                        src.Count(x => x.DaXoa != true)))
                .ForMember(dest => dest.TrangHienTai, opt => opt.Ignore())
                .ForMember(dest => dest.TongSoTrang, opt => opt.Ignore());

            // =====================================================
            // ENTITY -> RESPONSE
            // NGƯỜI THÍCH BÌNH LUẬN
            // =====================================================
            CreateMap<ThichBinhLuanBoDe, NguoiThichBinhLuanBoDeResponse>()
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore()); // service set

            // =====================================================
            // LIST ENTITY -> DANH SÁCH NGƯỜI THÍCH
            // =====================================================
            CreateMap<List<ThichBinhLuanBoDe>, DanhSachNguoiThichBinhLuanResponse>()
                .ForMember(dest => dest.NguoiThichs,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TongSo,
                    opt => opt.MapFrom(src => src.Count));
        }
    }
}
