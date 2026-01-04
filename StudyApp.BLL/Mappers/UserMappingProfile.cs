using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        #region NguoiDung - Entity to Response
        // NguoiDung -> NguoiDungResponse (thông tin cơ bản)
        CreateMap<NguoiDung, NguoiDungResponse>()
            .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => ByteToEnum<GioiTinhEnum>(src.GioiTinh)))
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.MapFrom(src => src.TrangThaiOnline ?? false))
            .ForMember(dest => dest.VaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.CapDo, opt => opt.Ignore());

        // NguoiDung -> NguoiDungChiTietResponse (thông tin đầy đủ)
        CreateMap<NguoiDung, NguoiDungChiTietResponse>()
            .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => ByteToEnum<GioiTinhEnum>(src.GioiTinh)))
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.MapFrom(src => src.TrangThaiOnline ?? false))
            .ForMember(dest => dest.Vang, opt => opt.MapFrom(src => src.Vang ?? 0))
            .ForMember(dest => dest.KimCuong, opt => opt.MapFrom(src => src.KimCuong ?? 0))
            .ForMember(dest => dest.TongDiemXp, opt => opt.MapFrom(src => src.TongDiemXp ?? 0))
            .ForMember(dest => dest.TongSoTheHoc, opt => opt.MapFrom(src => src.TongSoTheHoc ?? 0))
            .ForMember(dest => dest.TongSoTheDung, opt => opt.MapFrom(src => src.TongSoTheDung ?? 0))
            .ForMember(dest => dest.TongThoiGianHocPhut, opt => opt.MapFrom(src => src.TongThoiGianHocPhut ?? 0))
            .ForMember(dest => dest.ChuoiNgayHocLienTiep, opt => opt.MapFrom(src => src.ChuoiNgayHocLienTiep ?? 0))
            .ForMember(dest => dest.ChuoiNgayDaiNhat, opt => opt.MapFrom(src => src.ChuoiNgayDaiNhat ?? 0))
            .ForMember(dest => dest.SoStreakFreeze, opt => opt.MapFrom(src => src.SoStreakFreeze ?? 0))
            .ForMember(dest => dest.SoStreakHoiSinh, opt => opt.MapFrom(src => src.SoStreakHoiSinh ?? 0))
            .ForMember(dest => dest.SoTranThachDau, opt => opt.MapFrom(src => src.SoTranThachDau ?? 0))
            .ForMember(dest => dest.SoTranThang, opt => opt.MapFrom(src => src.SoTranThang ?? 0))
            .ForMember(dest => dest.SoTranThua, opt => opt.MapFrom(src => src.SoTranThua ?? 0))
            .ForMember(dest => dest.DaXacThucEmail, opt => opt.MapFrom(src => src.DaXacThucEmail ?? false))
            .ForMember(dest => dest.VaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.CapDo, opt => opt.Ignore())
            .ForMember(dest => dest.TuyChinhProfile, opt => opt.Ignore());

        // NguoiDung -> NguoiDungTomTatResponse (thông tin ngắn gọn cho danh sách)
        CreateMap<NguoiDung, NguoiDungTomTatResponse>()
            .ForMember(dest => dest.TongDiemXp, opt => opt.MapFrom(src => src.TongDiemXp ?? 0))
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.MapFrom(src => src.TrangThaiOnline ?? false))
            .ForMember(dest => dest.TenCapDo, opt => opt.Ignore());

        // NguoiDung -> BangXepHangNguoiDungResponse
        CreateMap<NguoiDung, BangXepHangNguoiDungResponse>()
            .ForMember(dest => dest.Hang, opt => opt.Ignore())
            .ForMember(dest => dest.TongDiemXp, opt => opt.MapFrom(src => src.TongDiemXp ?? 0))
            .ForMember(dest => dest.ChuoiNgayHocLienTiep, opt => opt.MapFrom(src => src.ChuoiNgayHocLienTiep ?? 0))
            .ForMember(dest => dest.TenCapDo, opt => opt.Ignore());
        #endregion

        #region NguoiDung - Request to Entity
        // DangKyNguoiDungRequest -> NguoiDung (đăng ký tài khoản mới)
        CreateMap<DangKyNguoiDungRequest, NguoiDung>()
            .ForMember(dest => dest.MaNguoiDung, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.MatKhauMaHoa, opt => opt.Ignore()) // Mã hóa ở service
            .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DaXacThucEmail, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.DaXoa, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Vang, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.KimCuong, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.TongDiemXp, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.TongSoTheHoc, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.TongSoTheDung, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.TongThoiGianHocPhut, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.ChuoiNgayHocLienTiep, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.ChuoiNgayDaiNhat, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SoStreakFreeze, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SoStreakHoiSinh, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SoTranThachDau, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SoTranThang, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SoTranThua, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.MaCapDo, opt => opt.Ignore())
            .ForMember(dest => dest.GioiTinh, opt => opt.Ignore())
            .ForMember(dest => dest.NgaySinh, opt => opt.Ignore())
            .ForMember(dest => dest.HinhDaiDien, opt => opt.Ignore())
            .ForMember(dest => dest.AnhBia, opt => opt.Ignore())
            .ForMember(dest => dest.TieuSu, opt => opt.Ignore())
            .ForMember(dest => dest.NgayHoatDongCuoi, opt => opt.Ignore())
            .ForMember(dest => dest.LanOnlineCuoi, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.MaCapDoNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.MaVaiTroNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.CaiDatNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.TuyChinhProfile, opt => opt.Ignore())
            .ForMember(dest => dest.BaoVeChuoiNgays, opt => opt.Ignore())
            .ForMember(dest => dest.BoostDangHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.DiemDanhHangNgays, opt => opt.Ignore())
            .ForMember(dest => dest.KhoNguoiDungs, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuGiaoDiches, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore())
            .ForMember(dest => dest.TienDoNhiemVus, opt => opt.Ignore());

        // CapNhatThongTinCaNhanRequest -> NguoiDung (cập nhật thông tin)
        CreateMap<CapNhatThongTinCaNhanRequest, NguoiDung>()
            .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.TenDangNhap, opt => opt.Ignore())
            .ForMember(dest => dest.MatKhauMaHoa, opt => opt.Ignore())
            .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.MaCapDo, opt => opt.Ignore())
            .ForMember(dest => dest.HinhDaiDien, opt => opt.Ignore())
            .ForMember(dest => dest.AnhBia, opt => opt.Ignore())
            .ForMember(dest => dest.Vang, opt => opt.Ignore())
            .ForMember(dest => dest.KimCuong, opt => opt.Ignore())
            .ForMember(dest => dest.TongDiemXp, opt => opt.Ignore())
            .ForMember(dest => dest.TongSoTheHoc, opt => opt.Ignore())
            .ForMember(dest => dest.TongSoTheDung, opt => opt.Ignore())
            .ForMember(dest => dest.TongThoiGianHocPhut, opt => opt.Ignore())
            .ForMember(dest => dest.ChuoiNgayHocLienTiep, opt => opt.Ignore())
            .ForMember(dest => dest.ChuoiNgayDaiNhat, opt => opt.Ignore())
            .ForMember(dest => dest.SoStreakFreeze, opt => opt.Ignore())
            .ForMember(dest => dest.SoStreakHoiSinh, opt => opt.Ignore())
            .ForMember(dest => dest.NgayHoatDongCuoi, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThachDau, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThang, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThua, opt => opt.Ignore())
            .ForMember(dest => dest.DaXacThucEmail, opt => opt.Ignore())
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.Ignore())
            .ForMember(dest => dest.LanOnlineCuoi, opt => opt.Ignore())
            .ForMember(dest => dest.DaXoa, opt => opt.Ignore())
            .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.MaCapDoNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.MaVaiTroNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.CaiDatNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.TuyChinhProfile, opt => opt.Ignore())
            .ForMember(dest => dest.BaoVeChuoiNgays, opt => opt.Ignore())
            .ForMember(dest => dest.BoostDangHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.DiemDanhHangNgays, opt => opt.Ignore())
            .ForMember(dest => dest.KhoNguoiDungs, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuGiaoDiches, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore())
            .ForMember(dest => dest.TienDoNhiemVus, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        // CapNhatHinhAnhProfileRequest -> NguoiDung
        CreateMap<CapNhatHinhAnhProfileRequest, NguoiDung>()
            .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.TenDangNhap, opt => opt.Ignore())
            .ForMember(dest => dest.MatKhauMaHoa, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.SoDienThoai, opt => opt.Ignore())
            .ForMember(dest => dest.HoVaTen, opt => opt.Ignore())
            .ForMember(dest => dest.NgaySinh, opt => opt.Ignore())
            .ForMember(dest => dest.GioiTinh, opt => opt.Ignore())
            .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.MaCapDo, opt => opt.Ignore())
            .ForMember(dest => dest.TieuSu, opt => opt.Ignore())
            .ForMember(dest => dest.Vang, opt => opt.Ignore())
            .ForMember(dest => dest.KimCuong, opt => opt.Ignore())
            .ForMember(dest => dest.TongDiemXp, opt => opt.Ignore())
            .ForMember(dest => dest.TongSoTheHoc, opt => opt.Ignore())
            .ForMember(dest => dest.TongSoTheDung, opt => opt.Ignore())
            .ForMember(dest => dest.TongThoiGianHocPhut, opt => opt.Ignore())
            .ForMember(dest => dest.ChuoiNgayHocLienTiep, opt => opt.Ignore())
            .ForMember(dest => dest.ChuoiNgayDaiNhat, opt => opt.Ignore())
            .ForMember(dest => dest.SoStreakFreeze, opt => opt.Ignore())
            .ForMember(dest => dest.SoStreakHoiSinh, opt => opt.Ignore())
            .ForMember(dest => dest.NgayHoatDongCuoi, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThachDau, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThang, opt => opt.Ignore())
            .ForMember(dest => dest.SoTranThua, opt => opt.Ignore())
            .ForMember(dest => dest.DaXacThucEmail, opt => opt.Ignore())
            .ForMember(dest => dest.TrangThaiOnline, opt => opt.Ignore())
            .ForMember(dest => dest.LanOnlineCuoi, opt => opt.Ignore())
            .ForMember(dest => dest.DaXoa, opt => opt.Ignore())
            .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.MaCapDoNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.MaVaiTroNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.CaiDatNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.TuyChinhProfile, opt => opt.Ignore())
            .ForMember(dest => dest.BaoVeChuoiNgays, opt => opt.Ignore())
            .ForMember(dest => dest.BoostDangHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.DiemDanhHangNgays, opt => opt.Ignore())
            .ForMember(dest => dest.KhoNguoiDungs, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuGiaoDiches, opt => opt.Ignore())
            .ForMember(dest => dest.LichSuHoatDongs, opt => opt.Ignore())
            .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore())
            .ForMember(dest => dest.TienDoNhiemVus, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        #endregion

        #region CapDo
        CreateMap<CapDo, CapDoResponse>();
        #endregion

        #region VaiTro
        CreateMap<VaiTro, VaiTroResponse>();

        CreateMap<VaiTro, VaiTroChiTietResponse>()
            .ForMember(dest => dest.SoNguoiDung, opt => opt.Ignore());

        // TaoVaiTroRequest -> VaiTro
        CreateMap<TaoVaiTroRequest, VaiTro>()
            .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.NguoiDungs, opt => opt.Ignore());

        // CapNhatVaiTroRequest -> VaiTro
        CreateMap<CapNhatVaiTroRequest, VaiTro>()
            .ForMember(dest => dest.MaVaiTro, opt => opt.Ignore())
            .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
            .ForMember(dest => dest.NguoiDungs, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        #endregion

        #region ThanhTuu
        // ThanhTuu -> ThanhTuuResponse
        CreateMap<ThanhTuu, ThanhTuuResponse>()
            .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => ParseEnum<LoaiThanhTuuEnum>(src.LoaiThanhTuu)))
            .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.DieuKienLoai)))
            .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
            .ForMember(dest => dest.BiAn, opt => opt.MapFrom(src => src.BiAn ?? false));

        // ThanhTuu -> ThanhTuuVoiTrangThaiResponse
        CreateMap<ThanhTuu, ThanhTuuVoiTrangThaiResponse>()
            .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => ParseEnum<LoaiThanhTuuEnum>(src.LoaiThanhTuu)))
            .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.DieuKienLoai)))
            .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
            .ForMember(dest => dest.BiAn, opt => opt.MapFrom(src => src.BiAn ?? false))
            .ForMember(dest => dest.DaDat, opt => opt.Ignore())
            .ForMember(dest => dest.NgayDat, opt => opt.Ignore())
            .ForMember(dest => dest.DaXem, opt => opt.Ignore())
            .ForMember(dest => dest.DaChiaSe, opt => opt.Ignore())
            .ForMember(dest => dest.TienDoHienTai, opt => opt.Ignore());

        // TaoThanhTuuRequest -> ThanhTuu (Admin)
        CreateMap<TaoThanhTuuRequest, ThanhTuu>()
            .ForMember(dest => dest.MaThanhTuu, opt => opt.Ignore())
            .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => src.LoaiThanhTuu.ToString()))
            .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => src.DieuKienLoai.HasValue ? src.DieuKienLoai.Value.ToString() : null))
            .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore());

        // CapNhatThanhTuuRequest -> ThanhTuu (Admin)
        CreateMap<CapNhatThanhTuuRequest, ThanhTuu>()
            .ForMember(dest => dest.MaThanhTuu, opt => opt.Ignore())
            .ForMember(dest => dest.LoaiThanhTuu, opt => opt.MapFrom(src => src.LoaiThanhTuu.HasValue ? src.LoaiThanhTuu.Value.ToString() : null))
            .ForMember(dest => dest.DieuKienLoai, opt => opt.MapFrom(src => src.DieuKienLoai.HasValue ? src.DieuKienLoai.Value.ToString() : null))
            .ForMember(dest => dest.ThoiGianTao, opt => opt.Ignore())
            .ForMember(dest => dest.ThanhTuuDatDuocs, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        #endregion

        #region ThanhTuuDatDuoc
        // ThanhTuuDatDuoc -> ThanhTuuDatDuocResponse
        CreateMap<ThanhTuuDatDuoc, ThanhTuuDatDuocResponse>()
            .ForMember(dest => dest.MaThanhTuu, opt => opt.MapFrom(src => src.MaThanhTuu))
            .ForMember(dest => dest.TenThanhTuu, opt => opt.Ignore()) // Map từ Navigation
            .ForMember(dest => dest.MoTa, opt => opt.Ignore())
            .ForMember(dest => dest.BieuTuong, opt => opt.Ignore())
            .ForMember(dest => dest.HinhHuy, opt => opt.Ignore())
            .ForMember(dest => dest.DoHiem, opt => opt.Ignore())
            .ForMember(dest => dest.DaXem, opt => opt.MapFrom(src => src.DaXem ?? false))
            .ForMember(dest => dest.DaChiaSe, opt => opt.MapFrom(src => src.DaChiaSe ?? false));
        #endregion

        #region NhiemVu
        // NhiemVu -> NhiemVuResponse
        CreateMap<NhiemVu, NhiemVuResponse>()
            .ForMember(dest => dest.LoaiNhiemVu, opt => opt.MapFrom(src => ParseEnum<LoaiNhiemVuEnum>(src.LoaiNhiemVu)))
            .ForMember(dest => dest.LoaiDieuKien, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.LoaiDieuKien)))
            .ForMember(dest => dest.ConHieuLuc, opt => opt.MapFrom(src => src.ConHieuLuc ?? true))
            .ForMember(dest => dest.ThuongVang, opt => opt.MapFrom(src => src.ThuongVang ?? 0))
            .ForMember(dest => dest.ThuongKimCuong, opt => opt.MapFrom(src => src.ThuongKimCuong ?? 0))
            .ForMember(dest => dest.ThuongXp, opt => opt.MapFrom(src => src.ThuongXp ?? 0));

        // NhiemVu -> NhiemVuVoiTienDoResponse
        CreateMap<NhiemVu, NhiemVuVoiTienDoResponse>()
            .ForMember(dest => dest.LoaiNhiemVu, opt => opt.MapFrom(src => ParseEnum<LoaiNhiemVuEnum>(src.LoaiNhiemVu)))
            .ForMember(dest => dest.LoaiDieuKien, opt => opt.MapFrom(src => ParseEnum<LoaiDieuKienEnum>(src.LoaiDieuKien)))
            .ForMember(dest => dest.ConHieuLuc, opt => opt.MapFrom(src => src.ConHieuLuc ?? true))
            .ForMember(dest => dest.ThuongVang, opt => opt.MapFrom(src => src.ThuongVang ?? 0))
            .ForMember(dest => dest.ThuongKimCuong, opt => opt.MapFrom(src => src.ThuongKimCuong ?? 0))
            .ForMember(dest => dest.ThuongXp, opt => opt.MapFrom(src => src.ThuongXp ?? 0))
            .ForMember(dest => dest.TienDoHienTai, opt => opt.Ignore())
            .ForMember(dest => dest.DaHoanThanh, opt => opt.Ignore())
            .ForMember(dest => dest.DaNhanThuong, opt => opt.Ignore())
            .ForMember(dest => dest.NgayBatDauThamGia, opt => opt.Ignore())
            .ForMember(dest => dest.NgayHoanThanh, opt => opt.Ignore());
        #endregion

        #region TienDoNhiemVu
        // TienDoNhiemVu -> TienDoNhiemVuResponse
        CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
            .ForMember(dest => dest.TienDoHienTai, opt => opt.MapFrom(src => src.TienDoHienTai ?? 0))
            .ForMember(dest => dest.DaHoanThanh, opt => opt.MapFrom(src => src.DaHoanThanh ?? false))
            .ForMember(dest => dest.DaNhanThuong, opt => opt.MapFrom(src => src.DaNhanThuong ?? false))
            .ForMember(dest => dest.TenNhiemVu, opt => opt.Ignore()) // Map từ Navigation
            .ForMember(dest => dest.DieuKienDatDuoc, opt => opt.Ignore());
        #endregion

        #region VatPham
        // VatPham -> VatPhamResponse
        CreateMap<VatPham, VatPhamResponse>()
            .ForMember(dest => dest.LoaiTienTe, opt => opt.MapFrom(src => (LoaiTienTeEnum)src.LoaiTienTe))
            .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem))
            .ForMember(dest => dest.ConHang, opt => opt.MapFrom(src => src.ConHang ?? true))
            .ForMember(dest => dest.DanhMuc, opt => opt.Ignore());

        // VatPham -> VatPhamTomTatResponse
        CreateMap<VatPham, VatPhamTomTatResponse>()
            .ForMember(dest => dest.DoHiem, opt => opt.MapFrom(src => src.DoHiem));
        #endregion

        #region KhoNguoiDung
        // KhoNguoiDung -> KhoNguoiDungResponse
        CreateMap<KhoNguoiDung, KhoNguoiDungResponse>()
            .ForMember(dest => dest.MaKho, opt => opt.MapFrom(src => src.MaKho))
            .ForMember(dest => dest.SoLuong, opt => opt.MapFrom(src => src.SoLuong ?? 0))
            .ForMember(dest => dest.DaTrangBi, opt => opt.MapFrom(src => src.DaTrangBi ?? false))
            .ForMember(dest => dest.TenVatPham, opt => opt.Ignore()) // Map từ Navigation
            .ForMember(dest => dest.DuongDanHinh, opt => opt.Ignore())
            .ForMember(dest => dest.TenDanhMuc, opt => opt.Ignore())
            .ForMember(dest => dest.DoHiem, opt => opt.Ignore());
        #endregion

        #region DanhMucSanPham
        // DanhMucSanPham -> DanhMucSanPhamTomTatResponse
        CreateMap<DanhMucSanPham, DanhMucSanPhamTomTatResponse>();

        // DanhMucSanPham -> DanhMucVoiVatPhamResponse
        CreateMap<DanhMucSanPham, DanhMucVoiVatPhamResponse>()
            .ForMember(dest => dest.VatPhams, opt => opt.Ignore());
        #endregion

        #region LichSuGiaoDich
        CreateMap<LichSuGiaoDich, LichSuGiaoDichResponse>()
            .ForMember(dest => dest.LoaiGiaoDich, opt => opt.MapFrom(src => ParseEnum<LoaiGiaoDichEnum>(src.LoaiGiaoDich)))
            .ForMember(dest => dest.LoaiTien, opt => opt.MapFrom(src => ParseEnum<LoaiTienTeGiaoDichEnum>(src.LoaiTien)))
            .ForMember(dest => dest.TenLoaiGiaoDich, opt => opt.Ignore())
            .ForMember(dest => dest.TenLoaiTien, opt => opt.Ignore())
            .ForMember(dest => dest.LaChi, opt => opt.Ignore())
            .ForMember(dest => dest.ThayDoi, opt => opt.Ignore());
        #endregion

        #region CaiDatNguoiDung
        // CaiDatNguoiDung -> CaiDatNguoiDungResponse
        CreateMap<CaiDatNguoiDung, CaiDatNguoiDungResponse>();

        // CapNhatCaiDatNguoiDungRequest -> CaiDatNguoiDung
        CreateMap<CapNhatCaiDatNguoiDungRequest, CaiDatNguoiDung>()
            .ForMember(dest => dest.MaNguoiDung, opt => opt.Ignore())
            .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.MaNguoiDungNavigation, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        #endregion

        #region TuyChinhProfile
        CreateMap<TuyChinhProfile, TuyChinhProfileResponse>();
        #endregion
    }
}