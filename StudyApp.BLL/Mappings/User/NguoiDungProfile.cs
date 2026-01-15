using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Mappings.User
{
    public class NguoiDungProfile : Profile
    {
        public NguoiDungProfile()
        {
            // =========================
            // ENTITY → BASIC PROFILE
            // =========================
            CreateMap<NguoiDung, NguoiDungResponse>()
                .ForMember(d => d.VaiTro,
                    o => o.MapFrom(s => (VaiTroEnum)(s.MaVaiTro ?? 2))) // default Member
                .ForMember(d => d.CapDo,
                    o => o.MapFrom(s => s.MaCapDo ?? 1))
                .ForMember(d => d.GioiTinh,
                    o => o.MapFrom(s =>
                        s.GioiTinh.HasValue
                            ? (GioiTinhEnum)s.GioiTinh.Value
                            : (GioiTinhEnum?)null))
                .ForMember(d => d.DaXacThucEmail,
                    o => o.MapFrom(s => s.DaXacThucEmail ?? false))
                .ForMember(d => d.TrangThaiOnline,
                    o => o.MapFrom(s => s.TrangThaiOnline ?? false));

            // =========================
            // ENTITY → GAMIFICATION
            // =========================
            CreateMap<NguoiDung, NguoiDungGamificationResponse>()
                .ForMember(d => d.TongDiemXp,
                    o => o.MapFrom(s => s.TongDiemXp ?? 0))
                .ForMember(d => d.Vang,
                    o => o.MapFrom(s => s.Vang ?? 0))
                .ForMember(d => d.KimCuong,
                    o => o.MapFrom(s => s.KimCuong ?? 0))
                .ForMember(d => d.TongSoTheHoc,
                    o => o.MapFrom(s => s.TongSoTheHoc ?? 0))
                .ForMember(d => d.TongSoTheDung,
                    o => o.MapFrom(s => s.TongSoTheDung ?? 0))
                .ForMember(d => d.TongThoiGianHocPhut,
                    o => o.MapFrom(s => s.TongThoiGianHocPhut ?? 0))
                .ForMember(d => d.ChuoiNgayHocLienTiep,
                    o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
                .ForMember(d => d.ChuoiNgayDaiNhat,
                    o => o.MapFrom(s => s.ChuoiNgayDaiNhat ?? 0))
                .ForMember(d => d.SoStreakFreeze,
                    o => o.MapFrom(s => s.SoStreakFreeze ?? 0))
                .ForMember(d => d.SoStreakHoiSinh,
                    o => o.MapFrom(s => s.SoStreakHoiSinh ?? 0));

            // =========================
            // ENTITY → THÁCH ĐẤU
            // =========================
            CreateMap<NguoiDung, NguoiDungThachDauResponse>()
                .ForMember(d => d.SoTranThachDau,
                    o => o.MapFrom(s => s.SoTranThachDau ?? 0))
                .ForMember(d => d.SoTranThang,
                    o => o.MapFrom(s => s.SoTranThang ?? 0))
                .ForMember(d => d.SoTranThua,
                    o => o.MapFrom(s => s.SoTranThua ?? 0));

            // =========================
            // ENTITY → USER STATS (dùng cho GamificationService)
            // =========================
            CreateMap<NguoiDung, UserStatsResponses>()
                .ForMember(d => d.TongDiemXP,
                    o => o.MapFrom(s => s.TongDiemXp ?? 0))
                .ForMember(d => d.CapDoHienTai,
                    o => o.MapFrom(s => s.MaCapDo ?? 1))
                .ForMember(d => d.ChuoiNgay,
                    o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
                .ForMember(d => d.SoStreakFreeze,
                    o => o.MapFrom(s => s.SoStreakFreeze ?? 0))
                .ForMember(d => d.ChuoiNgayHoc,
                    o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
                .ForMember(d => d.TongSoTheDaHoc,
                    o => o.MapFrom(s => s.TongSoTheHoc ?? 0))
                .ForMember(d => d.TongThoiGianHocPhut,
                    o => o.MapFrom(s => s.TongThoiGianHocPhut ?? 0))
                .ForMember(d => d.TyLeChinhXac,
                    o => o.MapFrom(_ => 0));

            // =========================
            // ENTITY → DTO (dùng cho AuthService / WinForms)
            // =========================
            CreateMap<NguoiDung, NguoiDungDTO>()
                .ForMember(d => d.MaVaiTro,
                    o => o.MapFrom(s => s.MaVaiTro ?? 2))
                .ForMember(d => d.Vang,
                    o => o.MapFrom(s => s.Vang ?? 0))
                .ForMember(d => d.KimCuong,
                    o => o.MapFrom(s => s.KimCuong ?? 0))
                .ForMember(d => d.TongDiemXp,
                    o => o.MapFrom(s => s.TongDiemXp ?? 0))
                .ForMember(d => d.ChuoiNgayHocLienTiep,
                    o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
                .ForMember(d => d.TongSoTheHoc,
                    o => o.MapFrom(s => s.TongSoTheHoc ?? 0))
                .ForMember(d => d.TongSoTheDung,
                    o => o.MapFrom(s => s.TongSoTheDung ?? 0));
        }
    }
}
