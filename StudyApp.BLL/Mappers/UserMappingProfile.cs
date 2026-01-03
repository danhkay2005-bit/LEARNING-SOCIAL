using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        #region NguoiDung
        CreateMap<NguoiDung, NguoiDungResponse>()
            .ForMember(d => d.GioiTinh, o => o.MapFrom(s => ByteToEnum<GioiTinhEnum>(s.GioiTinh)))
            .ForMember(d => d.Vang, o => o.MapFrom(s => s.Vang ?? 0))
            .ForMember(d => d.KimCuong, o => o.MapFrom(s => s.KimCuong ?? 0))
            .ForMember(d => d.TongDiemXp, o => o.MapFrom(s => s.TongDiemXp ?? 0))
            .ForMember(d => d.TongSoTheHoc, o => o.MapFrom(s => s.TongSoTheHoc ?? 0))
            .ForMember(d => d.TongSoTheDung, o => o.MapFrom(s => s.TongSoTheDung ?? 0))
            .ForMember(d => d.TongThoiGianHocPhut, o => o.MapFrom(s => s.TongThoiGianHocPhut ?? 0))
            .ForMember(d => d.ChuoiNgayHocLienTiep, o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
            .ForMember(d => d.ChuoiNgayDaiNhat, o => o.MapFrom(s => s.ChuoiNgayDaiNhat ?? 0))
            .ForMember(d => d.SoStreakFreeze, o => o.MapFrom(s => s.SoStreakFreeze ?? 0))
            .ForMember(d => d.SoStreakHoiSinh, o => o.MapFrom(s => s.SoStreakHoiSinh ?? 0))
            .ForMember(d => d.SoTranThachDau, o => o.MapFrom(s => s.SoTranThachDau ?? 0))
            .ForMember(d => d.SoTranThang, o => o.MapFrom(s => s.SoTranThang ?? 0))
            .ForMember(d => d.SoTranThua, o => o.MapFrom(s => s.SoTranThua ?? 0))
            .ForMember(d => d.DaXacThucEmail, o => o.MapFrom(s => s.DaXacThucEmail ?? false))
            .ForMember(d => d.TrangThaiOnline, o => o.MapFrom(s => s.TrangThaiOnline ?? false))
            .ForMember(d => d.CapDo, o => o.Ignore())
            .ForMember(d => d.VaiTro, o => o.Ignore())
            .ForMember(d => d.TuyChinhProfile, o => o.Ignore());

        CreateMap<NguoiDung, NguoiDungTomTatResponse>()
            .ForMember(d => d.TenHienThi, o => o.MapFrom(s => s.HoVaTen ?? s.TenDangNhap))
            .ForMember(d => d.AnhDaiDien, o => o.MapFrom(s => s.HinhDaiDien))
            .ForMember(d => d.DaXacThuc, o => o.MapFrom(s => s.DaXacThucEmail ?? false))
            .ForMember(d => d.DangOnline, o => o.MapFrom(s => s.TrangThaiOnline ?? false));

        CreateMap<NguoiDung, NguoiDungProfileResponse>()
            .ForMember(d => d.GioiTinh, o => o.MapFrom(s => ByteToEnum<GioiTinhEnum>(s.GioiTinh)))
            .ForMember(d => d.TongDiemXp, o => o.MapFrom(s => s.TongDiemXp ?? 0))
            .ForMember(d => d.ChuoiNgayHocLienTiep, o => o.MapFrom(s => s.ChuoiNgayHocLienTiep ?? 0))
            .ForMember(d => d.ChuoiNgayDaiNhat, o => o.MapFrom(s => s.ChuoiNgayDaiNhat ?? 0))
            .ForMember(d => d.TongSoTheHoc, o => o.MapFrom(s => s.TongSoTheHoc ?? 0))
            .ForMember(d => d.TongThoiGianHocPhut, o => o.MapFrom(s => s.TongThoiGianHocPhut ?? 0))
            .ForMember(d => d.DangOnline, o => o.MapFrom(s => s.TrangThaiOnline ?? false))
            .ForMember(d => d.CapDo, o => o.Ignore())
            .ForMember(d => d.ThanhTuuNoiBat, o => o.Ignore())
            .ForMember(d => d.SoBanBe, o => o.Ignore())
            .ForMember(d => d.SoNguoiTheoDoi, o => o.Ignore())
            .ForMember(d => d.SoDangTheoDoi, o => o.Ignore())
            .ForMember(d => d.DangTheoDoi, o => o.Ignore())
            .ForMember(d => d.LaBanBe, o => o.Ignore());
        #endregion

        #region CapDo
        CreateMap<CapDo, CapDoResponse>();
        #endregion

        #region VaiTro
        CreateMap<VaiTro, VaiTroResponse>();
        #endregion

        #region ThanhTuu
        CreateMap<ThanhTuu, ThanhTuuResponse>()
            .ForMember(d => d.LoaiThanhTuu, o => o.MapFrom(s => ParseEnum<LoaiThanhTuuEnum>(s.LoaiThanhTuu)))
            .ForMember(d => d.DoHiem, o => o.MapFrom(s => ByteToEnum<DoHiemEnum>(s.DoHiem)))
            .ForMember(d => d.BiAn, o => o.MapFrom(s => s.BiAn ?? false));

        CreateMap<ThanhTuu, ThanhTuuTomTatResponse>()
            .ForMember(d => d.DoHiem, o => o.MapFrom(s => ByteToEnum<DoHiemEnum>(s.DoHiem)));
        #endregion

        #region ThanhTuuDatDuoc
        CreateMap<ThanhTuuDatDuoc, ThanhTuuDatDuocResponse>()
            .ForMember(d => d.DaXem, o => o.MapFrom(s => s.DaXem ?? false))
            .ForMember(d => d.DaChiaSe, o => o.MapFrom(s => s.DaChiaSe ?? false))
            .ForMember(d => d.ThanhTuu, o => o.Ignore());
        #endregion

        #region NhiemVu
        CreateMap<NhiemVu, NhiemVuResponse>()
            .ForMember(d => d.LoaiNhiemVu, o => o.MapFrom(s => ParseEnum<LoaiNhiemVuEnum>(s.LoaiNhiemVu)))
            .ForMember(d => d.ConHieuLuc, o => o.MapFrom(s => s.ConHieuLuc ?? true))
            .ForMember(d => d.ThuongVang, o => o.MapFrom(s => s.ThuongVang ?? 0))
            .ForMember(d => d.ThuongKimCuong, o => o.MapFrom(s => s.ThuongKimCuong ?? 0))
            .ForMember(d => d.ThuongXp, o => o.MapFrom(s => s.ThuongXp ?? 0));

        CreateMap<NhiemVu, NhiemVuTomTatResponse>()
            .ForMember(d => d.LoaiNhiemVu, o => o.MapFrom(s => ParseEnum<LoaiNhiemVuEnum>(s.LoaiNhiemVu)))
            .ForMember(d => d.TienDo, o => o.Ignore())
            .ForMember(d => d.DaHoanThanh, o => o.Ignore());
        #endregion

        #region TienDoNhiemVu
        CreateMap<TienDoNhiemVu, TienDoNhiemVuResponse>()
            .ForMember(d => d.GiaTriHienTai, o => o.MapFrom(s => s.GiaTriHienTai ?? 0))
            .ForMember(d => d.DaHoanThanh, o => o.MapFrom(s => s.DaHoanThanh ?? false))
            .ForMember(d => d.DaNhanThuong, o => o.MapFrom(s => s.DaNhanThuong ?? false))
            .ForMember(d => d.TienDoPhanTram, o => o.Ignore())
            .ForMember(d => d.NhiemVu, o => o.Ignore());
        #endregion

        #region VatPham
        CreateMap<VatPham, VatPhamResponse>()
            .ForMember(d => d.LoaiTienTe, o => o.MapFrom(s => (LoaiTienTeEnum)s.LoaiTienTe))
            .ForMember(d => d.DoHiem, o => o.MapFrom(s => ByteToEnum<DoHiemEnum>(s.DoHiem)))
            .ForMember(d => d.ConHang, o => o.MapFrom(s => s.ConHang ?? true))
            .ForMember(d => d.DanhMuc, o => o.Ignore());

        CreateMap<VatPham, VatPhamTomTatResponse>()
            .ForMember(d => d.LoaiTienTe, o => o.MapFrom(s => (LoaiTienTeEnum)s.LoaiTienTe))
            .ForMember(d => d.DoHiem, o => o.MapFrom(s => ByteToEnum<DoHiemEnum>(s.DoHiem)));
        #endregion

        #region KhoNguoiDung
        CreateMap<KhoNguoiDung, VatPhamTrongKhoResponse>()
            .ForMember(d => d.SoLuong, o => o.MapFrom(s => s.SoLuong ?? 0))
            .ForMember(d => d.DaTrangBi, o => o.MapFrom(s => s.DaTrangBi ?? false))
            .ForMember(d => d.DaHetHan, o => o.MapFrom(s => s.ThoiGianHetHan.HasValue && s.ThoiGianHetHan.Value < DateTime.UtcNow))
            .ForMember(d => d.VatPham, o => o.Ignore());
        #endregion

        #region DanhMucSanPham
        CreateMap<DanhMucSanPham, DanhMucSanPhamResponse>()
            .ForMember(d => d.SoVatPham, o => o.MapFrom(s => s.VatPhams.Count));
        #endregion

        #region LichSuGiaoDich
        CreateMap<LichSuGiaoDich, LichSuGiaoDichResponse>()
            .ForMember(d => d.LoaiGiaoDich, o => o.MapFrom