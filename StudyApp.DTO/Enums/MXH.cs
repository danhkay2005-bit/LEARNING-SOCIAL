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
        ChiFollower = 2,
      
    }

    // =========================
    // REACTION & TƯƠNG TÁC
    // =========================

    /// <summary>
    /// Loại reaction bài đăng
    /// </summary>
    public enum LoaiReactionEnum
    {
        Thich = 1,
        YeuThich = 2,
        Haha = 3,
        Wow = 4,
        Buon = 5,
        TucGian = 6
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
    namespace StudyApp.DTO.Enums
    {
        public enum LoaiThongBaoEnum
        {
            MentionBaiDang = 1,
            MentionBinhLuan = 2,
            BinhLuanBaiDang = 3,
            TraLoiBinhLuan = 4,
            ThichBaiDang = 5,
            ThichBinhLuan = 6,
            ChiaSeBaiDang = 7,
            TheoDoi = 8,

            DenHanOnTap = 20,
            HoanThanhBoDe = 21,
            MoiThachDau = 22,
            KetQuaThachDau = 23,

            HeThong = 99
        }
        
    }

}
