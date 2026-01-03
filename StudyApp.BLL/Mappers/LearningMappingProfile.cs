using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers;

public class LearningMappingProfile : Profile
{
    public LearningMappingProfile()
    {
        #region BoDeHoc
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
        #endregion

        #region TheFlashcard
        CreateMap<TaoTheFlashcardRequest, TheFlashcard>()
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.LoaiThe, o => o.MapFrom(s => s.LoaiThe.ToString()))
            .ForMember(d => d.DoKho, o => o.MapFrom(s => s.DoKho.HasValue ? (byte)s.DoKho.Value : (byte?)null))
            .ForMember(d => d.SoLuotHoc, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLanDung, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLanSai, o => o.MapFrom(_ => 0))
            .ForMember(d => d.TyLeDungTb, o => o.MapFrom(_ => 0.0))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CapNhatTheFlashcardRequest, TheFlashcard>()
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.MaBoDe, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.LoaiThe, o => o.MapFrom((s, d) => s.LoaiThe.HasValue ? s.LoaiThe.Value.ToString() : d.LoaiThe))
            .ForMember(d => d.DoKho, o => o.MapFrom((s, d) => s.DoKho.HasValue ? (byte)s.DoKho.Value : d.DoKho))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<TheFlashcard, TheFlashcardResponse>()
            .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
            .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
            .ForMember(d => d.SoLuotHoc, o => o.MapFrom(s => s.SoLuotHoc ?? 0))
            .ForMember(d => d.SoLanDung, o => o.MapFrom(s => s.SoLanDung ?? 0))
            .ForMember(d => d.SoLanSai, o => o.MapFrom(s => s.SoLanSai ?? 0))
            .ForMember(d => d.TyLeDungTb, o => o.MapFrom(s => s.TyLeDungTb ?? 0))
            .ForMember(d => d.DapAnTracNghiems, o => o.MapFrom(s => s.DapAnTracNghiems))
            .ForMember(d => d.CapGheps, o => o.MapFrom(s => s.CapGheps))
            .ForMember(d => d.PhanTuSapXeps, o => o.MapFrom(s => s.PhanTuSapXeps))
            .ForMember(d => d.TuDienKhuyets, o => o.MapFrom(s => s.TuDienKhuyets))
            .ForMember(d => d.TienDoCuaToi, o => o.Ignore())
            .ForMember(d => d.DanhDauCuaToi, o => o.Ignore())
            .ForMember(d => d.GhiChuCuaToi, o => o.Ignore());

        CreateMap<TheFlashcard, TheFlashcardTomTatResponse>()
            .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
            .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
            .ForMember(d => d.MatTruocRutGon, o => o.MapFrom(s => Truncate(s.MatTruoc, 100)))
            .ForMember(d => d.MatSauRutGon, o => o.MapFrom(s => Truncate(s.MatSau, 100)))
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
            .ForMember(d => d.TrangThaiSRS, o => o.Ignore());

        CreateMap<TheFlashcard, TheHocResponse>()
            .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
            .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
            .ForMember(d => d.DapAnTracNghiems, o => o.Ignore())
            .ForMember(d => d.CapGheps, o => o.Ignore())
            .ForMember(d => d.PhanTuSapXeps, o => o.Ignore())
            .ForMember(d => d.TuDienKhuyets, o => o.Ignore())
            .ForMember(d => d.LaTheMoi, o => o.Ignore())
            .ForMember(d => d.SoLanDaHoc, o => o.Ignore())
            .ForMember(d => d.LanHocCuoi, o => o.Ignore());
        #endregion

        #region DapAnTracNghiem
        CreateMap<TaoDapAnTracNghiemRequest, DapAnTracNghiem>()
            .ForMember(d => d.MaDapAn, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<CapNhatDapAnTracNghiemRequest, DapAnTracNghiem>()
            .ForMember(d => d.MaDapAn, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<DapAnTracNghiem, DapAnTracNghiemResponse>()
            .ForMember(d => d.LaDapAnDung, o => o.MapFrom(s => s.LaDapAnDung ?? false))
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

        CreateMap<DapAnTracNghiem, DapAnTracNghiemHocResponse>()
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));
        #endregion

        #region CapGhep
        CreateMap<TaoCapGhepRequest, CapGhep>()
            .ForMember(d => d.MaCap, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<CapNhatCapGhepRequest, CapGhep>()
            .ForMember(d => d.MaCap, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<CapGhep, CapGhepResponse>()
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

        CreateMap<CapGhep, CapGhepHocResponse>()
            .ForMember(d => d.ThuTuVeTrai, o => o.MapFrom(s => s.ThuTu ?? 0));
        #endregion

        #region PhanTuSapXep
        CreateMap<TaoPhanTuSapXepRequest, PhanTuSapXep>()
            .ForMember(d => d.MaPhanTu, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<CapNhatPhanTuSapXepRequest, PhanTuSapXep>()
            .ForMember(d => d.MaPhanTu, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<PhanTuSapXep, PhanTuSapXepResponse>();

        CreateMap<PhanTuSapXep, PhanTuSapXepHocResponse>()
            .ForMember(d => d.ThuTuHienThi, o => o.Ignore()); // Sẽ được shuffle trong service
        #endregion

        #region TuDienKhuyet
        CreateMap<TaoTuDienKhuyetRequest, TuDienKhuyet>()
            .ForMember(d => d.MaTuDienKhuyet, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<CapNhatTuDienKhuyetRequest, TuDienKhuyet>()
            .ForMember(d => d.MaTuDienKhuyet, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<TuDienKhuyet, TuDienKhuyetResponse>();

        CreateMap<TuDienKhuyet, TuDienKhuyetHocResponse>()
            .ForMember(d => d.DoRongTu, o => o.MapFrom(s => Math.Max(s.TuCanDien.Length + 2, 5)));
        #endregion

        #region PhienHoc
        CreateMap<PhienHoc, PhienHocResponse>()
            .ForMember(d => d.LoaiPhien, o => o.MapFrom(s => ParseEnum<LoaiPhienHocEnum>(s.LoaiPhien)))
            .ForMember(d => d.ThoiGianHocGiay, o => o.MapFrom(s => s.ThoiGianHocGiay ?? 0))
            .ForMember(d => d.ThoiGianHocHienThi, o => o.MapFrom(s => FormatDuration(s.ThoiGianHocGiay)))
            .ForMember(d => d.TongSoThe, o => o.MapFrom(s => s.TongSoThe ?? 0))
            .ForMember(d => d.SoTheMoi, o => o.MapFrom(s => s.SoTheMoi ?? 0))
            .ForMember(d => d.SoTheOnTap, o => o.MapFrom(s => s.SoTheOnTap ?? 0))
            .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
            .ForMember(d => d.SoTheSai, o => o.MapFrom(s => s.SoTheSai ?? 0))
            .ForMember(d => d.SoTheBo, o => o.MapFrom(s => s.SoTheBo ?? 0))
            .ForMember(d => d.DiemDat, o => o.MapFrom(s => s.DiemDat ?? 0))
            .ForMember(d => d.DiemToiDa, o => o.MapFrom(s => s.DiemToiDa ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
            .ForMember(d => d.VangNhan, o => o.MapFrom(s => s.VangNhan ?? 0))
            .ForMember(d => d.CamXuc, o => o.MapFrom(s => ByteToEnum<CamXucHocEnum>(s.CamXuc)))
            .ForMember(d => d.BoDe, o => o.Ignore())
            .ForMember(d => d.ThongKe, o => o.Ignore());

        CreateMap<PhienHoc, PhienHocTomTatResponse>()
            .ForMember(d => d.LoaiPhien, o => o.MapFrom(s => ParseEnum<LoaiPhienHocEnum>(s.LoaiPhien)))
            .ForMember(d => d.ThoiGianHocGiay, o => o.MapFrom(s => s.ThoiGianHocGiay ?? 0))
            .ForMember(d => d.TongSoThe, o => o.MapFrom(s => s.TongSoThe ?? 0))
            .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
            .ForMember(d => d.BoDe, o => o.Ignore());
        #endregion

        #region ChiTietTraLoi
        CreateMap<GuiCauTraLoiRequest, ChiTietTraLoi>()
            .ForMember(d => d.MaTraLoi, o => o.Ignore())
            .ForMember(d => d.DapAnDung, o => o.Ignore())
            .ForMember(d => d.DoKhoUserDanhGia, o => o.MapFrom(s => s.DoKhoUserDanhGia.HasValue ? (byte)s.DoKhoUserDanhGia.Value : (byte?)null))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaPhienNavigation, o => o.Ignore())
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<ChiTietTraLoi, ChiTietTraLoiResponse>()
            .ForMember(d => d.ThoiGianTraLoiGiay, o => o.MapFrom(s => s.ThoiGianTraLoiGiay ?? 0))
            .ForMember(d => d.DoKhoUserDanhGia, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoUserDanhGia)))
            .ForMember(d => d.The, o => o.Ignore());
        #endregion

        #region TienDoHocTap
        CreateMap<TienDoHocTap, TienDoTheResponse>()
            .ForMember(d => d.TrangThai, o => o.MapFrom(s => (TrangThaiSRSEnum)(s.TrangThai ?? 0)))
            .ForMember(d => d.HeSoDe, o => o.MapFrom(s => s.HeSoDe ?? 2.5))
            .ForMember(d => d.KhoangCachNgay, o => o.MapFrom(s => s.KhoangCachNgay ?? 0))
            .ForMember(d => d.SoLanLap, o => o.MapFrom(s => s.SoLanLap ?? 0))
            .ForMember(d => d.SoLanDung, o => o.MapFrom(s => s.SoLanDung ?? 0))
            .ForMember(d => d.SoLanSai, o => o.MapFrom(s => s.SoLanSai ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.ThoiGianTraLoiTbGiay, o => o.MapFrom(s => s.ThoiGianTraLoiTbgiay ?? 0))
            .ForMember(d => d.DoKhoCanNhan, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoCanNhan)));
        #endregion

        #region ThuMuc
        CreateMap<TaoThuMucRequest, ThuMuc>()
            .ForMember(d => d.MaThuMuc, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.BoDeHocs, o => o.Ignore())
            .ForMember(d => d.InverseMaThuMucChaNavigation, o => o.Ignore())
            .ForMember(d => d.MaThuMucChaNavigation, o => o.Ignore());

        CreateMap<CapNhatThuMucRequest, ThuMuc>()
            .ForMember(d => d.MaThuMuc, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<ThuMuc, ThuMucResponse>()
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
            .ForMember(d => d.SoBoDeTrongThuMuc, o => o.MapFrom(s => s.BoDeHocs.Count))
            .ForMember(d => d.SoThuMucCon, o => o.MapFrom(s => s.InverseMaThuMucChaNavigation.Count))
            .ForMember(d => d.ThuMucCons, o => o.Ignore())
            .ForMember(d => d.BoDes, o => o.Ignore());

        CreateMap<ThuMuc, ThuMucTomTatResponse>()
            .ForMember(d => d.SoBoDe, o => o.MapFrom(s => s.BoDeHocs.Count))
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0));

        CreateMap<ThuMuc, ThuMucNodeResponse>()
            .ForMember(d => d.SoBoDe, o => o.MapFrom(s => s.BoDeHocs.Count))
            .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
            .ForMember(d => d.Children, o => o.Ignore());
        #endregion

        #region ChuDe
        CreateMap<ChuDe, ChuDeResponse>()
            .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.SoLuotDung ?? 0));
        #endregion

        #region Tag
        CreateMap<Tag, TagResponse>()
            .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.SoLuotDung ?? 0));
        #endregion

        #region TagBoDe
        CreateMap<TagBoDe, TagResponse>()
            .ForMember(d => d.MaTag, o => o.MapFrom(s => s.MaTag))
            .ForMember(d => d.TenTag, o => o.MapFrom(s => s.MaTagNavigation.TenTag))
            .ForMember(d => d.SoLuotDung, o => o.MapFrom(s => s.MaTagNavigation.SoLuotDung ?? 0))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(s => s.MaTagNavigation.ThoiGianTao));
        #endregion

        #region DanhGiaBoDe
        CreateMap<DanhGiaBoDeRequest, DanhGiaBoDe>()
            .ForMember(d => d.MaDanhGia, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.DoKhoThucTe, o => o.MapFrom(s => s.DoKhoThucTe.HasValue ? (byte)s.DoKhoThucTe.Value : (byte?)null))
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

        CreateMap<CapNhatDanhGiaRequest, DanhGiaBoDe>()
            .ForMember(d => d.MaDanhGia, o => o.Ignore())
            .ForMember(d => d.MaBoDe, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.Ignore())
            .ForMember(d => d.DoKhoThucTe, o => o.MapFrom((s, d) => s.DoKhoThucTe.HasValue ? (byte)s.DoKhoThucTe.Value : d.DoKhoThucTe))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<DanhGiaBoDe, DanhGiaBoDeResponse>()
            .ForMember(d => d.DoKhoThucTe, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKhoThucTe)))
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
            .ForMember(d => d.NguoiDanhGia, o => o.Ignore())
            .ForMember(d => d.DaThich, o => o.Ignore())
            .ForMember(d => d.LaCuaToi, o => o.Ignore());
        #endregion

        #region BinhLuanBoDe
        CreateMap<TaoBinhLuanBoDeRequest, BinhLuanBoDe>()
            .ForMember(d => d.MaBinhLuan, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(_ => 0))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => false))
            .ForMember(d => d.DaXoa, o => o.MapFrom(_ => false))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianSua, o => o.Ignore())
            .ForMember(d => d.MaBoDeNavigation, o => o.Ignore())
            .ForMember(d => d.MaBinhLuanChaNavigation, o => o.Ignore())
            .ForMember(d => d.InverseMaBinhLuanChaNavigation, o => o.Ignore())
            .ForMember(d => d.ThichBinhLuanBoDes, o => o.Ignore());

        CreateMap<CapNhatBinhLuanBoDeRequest, BinhLuanBoDe>()
            .ForMember(d => d.MaBinhLuan, o => o.Ignore())
            .ForMember(d => d.MaBoDe, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.MaBinhLuanCha, o => o.Ignore())
            .ForMember(d => d.ThoiGian, o => o.Ignore())
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(_ => true))
            .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<BinhLuanBoDe, BinhLuanBoDeResponse>()
            .ForMember(d => d.SoLuotThich, o => o.MapFrom(s => s.SoLuotThich ?? 0))
            .ForMember(d => d.SoTraLoi, o => o.MapFrom(s => s.InverseMaBinhLuanChaNavigation.Count))
            .ForMember(d => d.DaChinhSua, o => o.MapFrom(s => s.DaChinhSua ?? false))
            .ForMember(d => d.NguoiBinhLuan, o => o.Ignore())
            .ForMember(d => d.DaThich, o => o.Ignore())
            .ForMember(d => d.LaCuaToi, o => o.Ignore())
            .ForMember(d => d.TraLois, o => o.Ignore());
        #endregion

        #region ThichBinhLuanBoDe
        CreateMap<ThichBinhLuanBoDe, NguoiThichBinhLuanBoDeResponse>()
            .ForMember(d => d.NguoiDung, o => o.Ignore());
        #endregion

        #region BaoCaoBoDe
        CreateMap<BaoCaoBoDeRequest, BaoCaoBoDe>()
            .ForMember(d => d.MaBaoCao, o => o.Ignore())
            .ForMember(d => d.MaNguoiBaoCao, o => o.Ignore())
            .ForMember(d => d.LyDo, o => o.MapFrom(s => s.LyDo.ToString()))
            .ForMember(d => d.TrangThai, o => o.MapFrom(_ => TrangThaiBaoCaoEnum.ChoDuyet.ToString()))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

        CreateMap<BaoCaoBoDe, BaoCaoBoDeResponse>()
            .ForMember(d => d.LyDo, o => o.MapFrom(s => ParseEnum<LyDoBaoCaoEnum>(s.LyDo)))
            .ForMember(d => d.TrangThai, o => o.MapFrom(s => ParseEnum<TrangThaiBaoCaoEnum>(s.TrangThai)))
            .ForMember(d => d.BoDe, o => o.Ignore())
            .ForMember(d => d.NguoiBaoCao, o => o.Ignore());
        #endregion

        #region BoDeYeuThich
        CreateMap<ThemBoDeYeuThichRequest, BoDeYeuThich>()
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGianLuu, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaBoDeNavigation, o => o.Ignore());

        CreateMap<BoDeYeuThich, BoDeYeuThichResponse>()
            .ForMember(d => d.BoDe, o => o.Ignore());
        #endregion

        #region MucTieuCaNhan
        CreateMap<TaoMucTieuRequest, MucTieuCaNhan>()
            .ForMember(d => d.MaMucTieu, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiMucTieu, o => o.MapFrom(s => s.LoaiMucTieu.ToString()))
            .ForMember(d => d.GiaTriHienTai, o => o.MapFrom(_ => 0))
            .ForMember(d => d.DaHoanThanh, o => o.MapFrom(_ => false))
            .ForMember(d => d.NgayHoanThanh, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CapNhatMucTieuRequest, MucTieuCaNhan>()
            .ForMember(d => d.MaMucTieu, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiMucTieu, o => o.Ignore())
            .ForMember(d => d.NgayBatDau, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<MucTieuCaNhan, MucTieuCaNhanResponse>()
            .ForMember(d => d.LoaiMucTieu, o => o.MapFrom(s => ParseEnum<LoaiMucTieuEnum>(s.LoaiMucTieu)))
            .ForMember(d => d.GiaTriHienTai, o => o.MapFrom(s => s.GiaTriHienTai ?? 0))
            .ForMember(d => d.DaHoanThanh, o => o.MapFrom(s => s.DaHoanThanh ?? false))
            .ForMember(d => d.TienDoPhanTram, o => o.MapFrom(s => CalculateProgress(s.GiaTriHienTai, s.GiaTriMucTieu)))
            .ForMember(d => d.SoNgayConLai, o => o.MapFrom(s => CalculateDaysRemaining(s.NgayKetThuc)));
        #endregion

        #region CheDoHocCaNhan
        CreateMap<CapNhatCheDoHocRequest, CheDoHocCaNhan>()
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
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
        #endregion

        #region ThongKeNgay
        CreateMap<ThongKeNgay, ThongKeNgayResponse>()
            .ForMember(d => d.TongTheHoc, o => o.MapFrom(s => s.TongTheHoc ?? 0))
            .ForMember(d => d.SoTheMoi, o => o.MapFrom(s => s.SoTheMoi ?? 0))
            .ForMember(d => d.SoTheOnTap, o => o.MapFrom(s => s.SoTheOnTap ?? 0))
            .ForMember(d => d.SoTheDung, o => o.MapFrom(s => s.SoTheDung ?? 0))
            .ForMember(d => d.SoTheSai, o => o.MapFrom(s => s.SoTheSai ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
            .ForMember(d => d.SoPhienHoc, o => o.MapFrom(s => s.SoPhienHoc ?? 0))
            .ForMember(d => d.SoBoDeHoc, o => o.MapFrom(s => s.SoBoDeHoc ?? 0))
            .ForMember(d => d.XpNhan, o => o.MapFrom(s => s.Xpnhan ?? 0))
            .ForMember(d => d.VangNhan, o => o.MapFrom(s => s.VangNhan ?? 0))
            .ForMember(d => d.DaHoanThanhMucTieu, o => o.MapFrom(s => s.DaHoanThanhMucTieu ?? false));
        #endregion

        #region DanhDauThe
        CreateMap<DanhDauTheRequest, DanhDauThe>()
            .ForMember(d => d.MaDanhDau, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.LoaiDanhDau, o => o.MapFrom(s => s.LoaiDanhDau.ToString()))
            .ForMember(d => d.ThoiGian, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<DanhDauThe, DanhDauTheResponse>()
            .ForMember(d => d.LoaiDanhDau, o => o.MapFrom(s => ParseEnum<LoaiDanhDauTheEnum>(s.LoaiDanhDau)))
            .ForMember(d => d.The, o => o.Ignore());
        #endregion

        #region GhiChuThe
        CreateMap<TaoGhiChuTheRequest, GhiChuThe>()
            .ForMember(d => d.MaGhiChu, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.MaTheNavigation, o => o.Ignore());

        CreateMap<CapNhatGhiChuTheRequest, GhiChuThe>()
            .ForMember(d => d.MaGhiChu, o => o.Ignore())
            .ForMember(d => d.MaNguoiDung, o => o.Ignore())
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.ThoiGianTao, o => o.Ignore())
            .ForMember(d => d.ThoiGianSua, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

        CreateMap<GhiChuThe, GhiChuTheResponse>();
        #endregion

        #region LichSuHocBoDe
        CreateMap<LichSuHocBoDe, LichSuHocBoDeResponse>()
            .ForMember(d => d.SoTheHoc, o => o.MapFrom(s => s.SoTheHoc ?? 0))
            .ForMember(d => d.TyLeDung, o => o.MapFrom(s => s.TyLeDung ?? 0))
            .ForMember(d => d.ThoiGianHocPhut, o => o.MapFrom(s => s.ThoiGianHocPhut ?? 0))
            .ForMember(d => d.BoDe, o => o.Ignore());
        #endregion

        #region LichSuClone
        CreateMap<LichSuClone, LichSuCloneResponse>()
            .ForMember(d => d.BoDeGoc, o => o.Ignore())
            .ForMember(d => d.BoDeClone, o => o.Ignore())
            .ForMember(d => d.NguoiClone, o => o.Ignore());
        #endregion
    }
}