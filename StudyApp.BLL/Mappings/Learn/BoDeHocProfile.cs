using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;

namespace StudyApp.BLL.Mappings.Learn
{
    public class BoDeHocProfile : Profile
    {
        public BoDeHocProfile()
        {
            // 1. ENTITY -> RESPONSE (Hiển thị danh sách bộ đề)
            CreateMap<BoDeHoc, BoDeHocResponse>()
                .ForMember(dest => dest.MucDoKho, opt => opt.MapFrom(src => (MucDoKhoEnum)(src.MucDoKho ?? (byte)MucDoKhoEnum.TrungBinh)))
                .ForMember(dest => dest.LaCongKhai, opt => opt.MapFrom(src => src.LaCongKhai ?? true))
                .ForMember(dest => dest.SoLuongThe, opt => opt.MapFrom(src => src.SoLuongThe ?? 0))
                .ForMember(dest => dest.SoLuotHoc, opt => opt.MapFrom(src => src.SoLuotHoc ?? 0))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(src => src.ThoiGianTao ?? DateTime.MinValue));

            // 2. CREATE/UPDATE REQUEST -> ENTITY (Quản lý bộ đề)
            CreateMap<TaoBoDeHocRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho, opt => opt.MapFrom(src => (byte)src.MucDoKho))
                .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<CapNhatBoDeHocRequest, BoDeHoc>()
                .ForMember(dest => dest.MucDoKho, opt => opt.MapFrom(src => (byte)src.MucDoKho))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.MapFrom(_ => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // 3. THÁCH ĐẤU: ENTITY -> RESPONSE (Lịch sử & BXH)
            CreateMap<LichSuThachDau, LichSuThachDauResponse>()
                .ForMember(dest => dest.TenBoDe, opt => opt.MapFrom(src =>
                    src.MaBoDeNavigation != null ? src.MaBoDeNavigation.TieuDe : "Bộ đề đã bị xóa"));

            // Ánh xạ sang BXH (Cần đảm bảo ThoiGianLamBaiGiay có dữ liệu)
            CreateMap<LichSuThachDau, ThachDauNguoiChoiResponse>()
                .ForMember(dest => dest.ThoiGianLamBaiGiay, opt => opt.MapFrom(src => src.ThoiGianLamBaiGiay));

            // 4. THÁCH ĐẤU: REQUEST -> ENTITY (Lưu kết quả)

            // Lúc mới tham gia trận đấu (Khởi tạo bản ghi rỗng)
            CreateMap<LichSuThachDauRequest, LichSuThachDau>()
                .ForMember(dest => dest.MaThachDauGoc, opt => opt.MapFrom(src => src.MaThachDau))
                .ForMember(dest => dest.Diem, opt => opt.MapFrom(_ => (int?)null))
                .ForMember(dest => dest.LaNguoiThang, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.MapFrom(_ => DateTime.Now));

            // ✅ QUAN TRỌNG: Cập nhật kết quả khi thi xong
            CreateMap<CapNhatKetQuaThachDauRequest, LichSuThachDau>()
                .ForMember(dest => dest.MaThachDauGoc, opt => opt.MapFrom(src => src.MaThachDau))
                .ForMember(dest => dest.Diem, opt => opt.MapFrom(src => src.Diem))
                .ForMember(dest => dest.SoTheDung, opt => opt.MapFrom(src => src.SoTheDung))
                .ForMember(dest => dest.SoTheSai, opt => opt.MapFrom(src => src.SoTheSai))
                .ForMember(dest => dest.ThoiGianLamBaiGiay, opt => opt.MapFrom(src => src.ThoiGianLamBaiGiay))
                .ForMember(dest => dest.ThoiGianKetThuc, opt => opt.MapFrom(_ => DateTime.Now))
                // Bỏ qua không map lại ID và Người dùng tránh sai lệch dữ liệu gốc
                .ForMember(dest => dest.MaLichSu, opt => opt.Ignore())
                .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore());
        }
    }
}