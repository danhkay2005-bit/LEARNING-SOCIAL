namespace StudyApp.DTO.Enums
{
    public enum MucDoKhoEnum : byte
    {
        RatDe = 1,
        De = 2,
        TrungBinh = 3,
        Kho = 4,
        RatKho = 5
    }

    public enum LoaiTheEnum
    {
        CoBan,
        TracNghiem,
        DienKhuyet,
        GhepCap,
        SapXep,
        NgheViet,
        HinhAnh
    }

    public enum LoaiPhienHocEnum
    {
        HocMoi,
        OnTap,
        KiemTra,
        ThachDau,
        TuyChon
    }

    public enum TrangThaiHocEnum : byte
    {
        New = 0,
        Learning = 1,
        Review = 2,
        Mastered = 3
    }

    public enum TrangThaiAIEnum
    {
        ThanhCong,
        ThatBai,
        DangXuLy
    }

    /// <summary>
    /// Kết quả trả lời (ChiTietTraLoi.TraLoiDung)
    /// </summary>
    public enum KetQuaTraLoiEnum : byte
    {
        Sai = 0,
        Dung = 1
    }

    /// <summary>
    /// Trạng thái thách đấu (ThachDau.TrangThai)
    /// </summary>
    public enum TrangThaiThachDauEnum
    {
        ChoNguoiChoi,
        DangDau,
        DaKetThuc,
        Huy
    }
    public enum CheDoHocEnum
    {
        HocMotMinh,
        ThachDau
    }
}