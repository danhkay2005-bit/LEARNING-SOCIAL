using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Mapping cho Thành Viên Cuộc Trò Chuyện
    /// </summary>
    public class ThanhVienCuocTroChuyenMapping : Profile
    {
        public ThanhVienCuocTroChuyenMapping()
        {
            MapEntityToResponse();
        }

        private void MapEntityToResponse()
        {
            // ===== CHI TIẾT THÀNH VIÊN =====
            CreateMap<ThanhVienCuocTroChuyen, ThanhVienCuocTroChuyenResponse>()
                .ForMember(d => d.MaCuocTroChuyen,
                    o => o.MapFrom(s => s.MaCuocTroChuyen))
                .ForMember(d => d.MaNguoiDung,
                    o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.BiDanh,
                    o => o.MapFrom(s => s.BiDanh))
                .ForMember(d => d.VaiTro,
                    o => o.MapFrom(s =>
                        Enum.Parse<VaiTroThanhVienChatEnum>(s.VaiTro!)))
                .ForMember(d => d.TatThongBao,
                    o => o.MapFrom(s => s.TatThongBao ?? false))
                .ForMember(d => d.DaRoiNhom,
                    o => o.MapFrom(s => s.DaRoiNhom ?? false))
                .ForMember(d => d.ThoiGianThamGia,
                    o => o.MapFrom(s => s.ThoiGianThamGia))
                .ForMember(d => d.ThoiGianXemCuoi,
                    o => o.MapFrom(s => s.ThoiGianXemCuoi))

                // user-context / realtime
                .ForMember(d => d.DangOnline,
                    o => o.Ignore())

                // navigation
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());

            // ===== TÓM TẮT THÀNH VIÊN =====
            CreateMap<ThanhVienCuocTroChuyen, ThanhVienTomTatResponse>()
                .ForMember(d => d.MaNguoiDung,
                    o => o.MapFrom(s => s.MaNguoiDung))
                .ForMember(d => d.BiDanh,
                    o => o.MapFrom(s => s.BiDanh))
                .ForMember(d => d.VaiTro,
                    o => o.MapFrom(s =>
                        Enum.Parse<VaiTroThanhVienChatEnum>(s.VaiTro!)))

                // lấy từ User / presence
                .ForMember(d => d.TenHienThi,
                    o => o.Ignore())
                .ForMember(d => d.AnhDaiDien,
                    o => o.Ignore())
                .ForMember(d => d.DangOnline,
                    o => o.Ignore());
        }
    }
}
