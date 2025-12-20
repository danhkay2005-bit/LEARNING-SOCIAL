namespace Common
{
    // ======== NGƯỜI DÙNG ========
    public enum VaiTro
    {
        Admin = 1,
        Member = 2
    }

    public enum LoaiHoatDong
    {
        DangNhap,
        DangXuat,
        HocThe,
        TaoBoDe,
        DatThanhTuu,
        LenLevel,
        ThachDau,
        ChuoiNgay,
        DiemDanh,
        MuaVatPham,
        NhanThuong,
        ChiaSeBoDe,
        DangKy,
        DoiMatKhau
    }

    public enum GioiTinh
    {
        Khac = 0,
        Nam = 1,
        Nu = 2
    }

    // ======== HỌC TẬP ========
    public enum LoaiThe
    {
        CoBan,
        TracNghiem,
        DienKhuyet,
        GhepCap,
        SapXep,
        NgheViet,
        HinhAnh
    }

    public enum TrangThaiSRS
    {
        New = 0,
        Learning = 1,
        Review = 2,
        Mastered = 3
    }

    public enum LoaiPhienHoc
    {
        HocMoi,
        OnTap,
        KiemTra,
        ThachDau,
        TuyChon
    }

    public enum DoKho
    {
        RatDe = 1,
        De = 2,
        TrungBinh = 3,
        Kho = 4,
        RatKho = 5
    }

    // ======== MXH ========
    public enum LoaiBaiDang
    {
        VanBan,
        HinhAnh,
        Video,
        ChiaSeBoDe,
        ThanhTuu,
        ChuoiNgay,
        KetQuaThachDau
    }

    public enum LoaiReaction
    {
        Thich,
        Tim,
        HaHa,
        Wow,
        Buon,
        TucGian
    }

    public enum LoaiStory
    {
        HinhAnh,
        Video,
        VanBan,
        ThanhTuu,
        ChuoiNgay,
        ChiaSeBoDe
    }

    public enum LoaiTinNhan
    {
        VanBan,
        HinhAnh,
        Video,
        File,
        Sticker,
        GhiAm,
        ChiaSeBoDe,
        ThachDau,
        HeThong
    }

    public enum TrangThaiThachDau
    {
        ChoXacNhan,
        DaChapNhan,
        DangDien,
        HoanThanh,
        TuChoi,
        HetHan,
        HuyBo
    }

    public enum LoaiNhom
    {
        CongKhai,
        RiengTu,
        BiMat
    }

    public enum VaiTroNhom
    {
        ChuNhom,
        QuanTri,
        ThanhVien
    }

    public enum QuyenRiengTu
    {
        RiengTu = 0,
        CongKhai = 1,
        ChiFollower = 2
    }

    // ======== THÔNG BÁO ========
    public enum LoaiThongBao
    {
        Thich,
        Reaction,
        BinhLuan,
        TraLoiBinhLuan,
        TheoDoi,
        Mention,
        ThachDauMoi,
        ThachDauChapNhan,
        ThachDauKetQua,
        TinNhan,
        ThanhTuu,
        LenLevel,
        ChuoiNgay,
        NhacHoc,
        HeThong
    }
}