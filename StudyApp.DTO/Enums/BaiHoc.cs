using System;

namespace StudyApp.DTO.Enums
{
    // =========================
    // BỘ ĐỀ & FLASHCARD
    // =========================

    /// <summary>
    /// Mức độ khó của bộ đề / thẻ (BoDeHoc.MucDoKho, TheFlashcard.DoKho)
    /// </summary>
    public enum MucDoKhoEnum : byte
    {
        RatDe = 1,
        De = 2,
        TrungBinh = 3,
        Kho = 4,
        RatKho = 5
    }

    /// <summary>
    /// Loại thẻ flashcard (TheFlashcard.LoaiThe)
    /// </summary>
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

    // =========================
    // HỌC TẬP & SRS
    // =========================

    /// <summary>
    /// Trạng thái học của thẻ (TienDoHocTap.TrangThai)
    /// </summary>
    public enum TrangThaiHocEnum : byte
    {
        New = 0,
        Learning = 1,
        Review = 2,
        Mastered = 3
    }

    /// <summary>
    /// Loại phiên học (PhienHoc.LoaiPhien)
    /// </summary>
    public enum LoaiPhienHocEnum
    {
        HocMoi,
        OnTap,
        KiemTra,
        ThachDau,
        TuyChon
    }

    // =========================
    // THÁCH ĐẤU
    // =========================

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

    // =========================
    // LOGIC ĐÚNG / SAI
    // =========================

    /// <summary>
    /// Kết quả trả lời (ChiTietTraLoi.TraLoiDung)
    /// </summary>
    public enum KetQuaTraLoiEnum : byte
    {
        Sai = 0,
        Dung = 1
    }

    // =========================
    // AI & SYSTEM LOG
    // =========================

    /// <summary>
    /// Trạng thái sinh dữ liệu AI (LogsGenerateAI.TrangThai)
    /// </summary>
    public enum TrangThaiAIEnum
    {
        ThanhCong,
        ThatBai,
        DangXuLy
    }
}
