namespace StudyApp.DTO.Enums
{
    // =========================
    // BÀI ĐĂNG
    // =========================

    /// <summary>
    /// Loại bài đăng MXH
    /// </summary>
    public enum LoaiBaiDangEnum
    {
        VanBan,
        HinhAnh,
        Video,
        ChiaSeBoDe,
        ChiaSeKhoaHoc,
        ThanhTuu,
        ChuoiNgay,
        KetQuaThachDau
    }

    /// <summary>
    /// Quyền riêng tư bài đăng
    /// </summary>
    public enum QuyenRiengTuEnum : byte
    {
        RiengTu = 0,
        CongKhai = 1,
        ChiFollower = 2
    }

    // =========================
    // REACTION & TƯƠNG TÁC
    // =========================

    /// <summary>
    /// Loại reaction bài đăng
    /// </summary>
    public enum LoaiReactionEnum
    {
        Thich,
        Tim,
        HaHa,
        Wow,
        Buon,
        TucGian
    }

    /// <summary>
    /// Trạng thái logic chung cho bài đăng / bình luận
    /// </summary>
    public enum TrangThaiNoiDungEnum
    {
        BinhThuong = 0,
        DaChinhSua = 1,
        DaXoa = 2
    }

    // =========================
    // HASHTAG
    // =========================

    /// <summary>
    /// Trạng thái hashtag
    /// </summary>
    public enum TrangThaiHashtagEnum
    {
        BinhThuong = 0,
        ThinhHanh = 1
    }

    // =========================
    // MENTION (OPTIONAL)
    // =========================

    /// <summary>
    /// Loại mention (dùng cho logic xử lý thông báo)
    /// </summary>
    public enum LoaiMentionEnum
    {
        BaiDang,
        BinhLuan
    }
}
