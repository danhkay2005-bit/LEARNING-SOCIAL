using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Enums
{
    public enum GioiTinhEnum { Khac = 0, Nam = 1, Nu = 2 }

    public enum VaiTroEnum { Admin = 1, Member = 2 }

    public enum LoaiTienTeEnum { Vang, KimCuong, XP }

    public enum LoaiBaoVeStreakEnum { Freeze, HoiSinh }

    public enum LoaiBoostEnum { XP, Vang }

    public enum LoaiNhiemVuEnum { HangNgay, HangTuan, ThanhTuu, SuKien }

    public enum LoaiThanhTuuEnum { HocTap, ChuoiNgay, XaHoi, SangTao, ThachDau, KhamPha, AnDanh }

    public enum LoaiGiaoDichEnum
    {
        MuaVatPham, NhanThuong, HoanThanhNhiemVu, DiemDanh,
        ThachDau, SuDungBoost
    }

    public enum LoaiHoatDongEnum
    {
        DangNhap, DangXuat, HocThe, TaoBoDe, DatThanhTuu,
        LenLevel, ThachDau, ChuoiNgay, DiemDanh, MuaVatPham,
        NhanThuong, ChiaSeBoDe, DangKy
    }

    public enum LoaiTienTeGiaoDichEnum { Vang, KimCuong }   
    public enum LoaiDieuKienNhiemVuEnum { HocThe, ThachDau, DiemDanh, MuaVatPham, TuongTac, ChiaSeBoDe, LenLevel }
}
