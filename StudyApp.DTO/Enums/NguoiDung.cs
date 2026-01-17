namespace StudyApp.DTO.Enums
{
    // =========================
    // USER & AUTH
    // =========================

    public enum VaiTroEnum
    {
        Admin = 1,
        Member = 2
    }

    public enum GioiTinhEnum : byte
    {
        Khac = 0,
        Nam = 1,
        Nu = 2
    }

    // =========================
    // CURRENCY & TRANSACTION
    // =========================

    // Dùng cho VatPham.LoaiTienTe (INT)
    public enum LoaiTienTeEnum
    {
        Vang = 1,
        KimCuong = 2
    }

    // Dùng cho LichSuGiaoDich.LoaiTien (VARCHAR)
    public enum LoaiTienGiaoDichEnum
    {
        Vang,
        KimCuong,
        XP
    }

    public enum LoaiGiaoDichEnum
    {
        MuaVatPham,
        NhanThuong,
        HoanThanhNhiemVu,
        DiemDanh,
        ThachDau,
        SuDungBoost,
        SuDungVatPham
    }

    // =========================
    // SHOP & STREAK
    // =========================

    public enum LoaiBaoVeChuoiEnum
    {
        Freeze,
        HoiSinh
    }

    public enum DoHiemEnum : byte
    {
        PhoBien = 1,
        ItGap = 2,
        Hiem = 3,
        SuThi = 4,
        HuyenThoai = 5
    }

    // =========================
    // GAMIFICATION
    // =========================

    public enum LoaiThanhTuuEnum
    {
        HocTap,
        ChuoiNgay,
        XaHoi,
        SangTao,
        ThachDau,
        KhamPha,
        AnDanh
    }

    public enum LoaiNhiemVuEnum
    {
        HangNgay,
        HangTuan,
        ThanhTuu,
        SuKien
    }

    // =========================
    // CONDITIONS (khuyến nghị)
    // =========================

    public enum LoaiDieuKienEnum
    {
        TongSoTheHoc,
        ChuoiNgayLienTiep,
        TongDiemXP,
        SoTranThang,
        SoLanDangNhap,
        ThoiGianHoc
    }
    public enum ResetPasswordResult
    {
        Success,
        EmailNotFound,
        Fail
    }
    public enum LoginResult
    {
        Success,
        UserNotFound,
        InvalidCredentials,
        AccountLocked
    }

    public enum RegisterResult
    {
        Success,
        UsernameExists,
        EmailExists,
        Fail
    }
}
