using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Enums
{
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
    public enum LoaiCuocTroChuyenEnum
    {
        Nhom,
        CaNhan
    }
    public enum LoaiReactionEnum
    {
        Thich,
        Tim,
        HaHa,
        Wow,
        Buon,
        TucGian
    }
    public enum LoaiTinNhanEnum
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
    public enum QuyenRiengTuEnum
    {
        RiengTu = 0,
        CongKhai = 1,
        ChiFollower = 2
    }
    public enum TrangThaiBanBeEnum
    {
        ChoDuyet,
        DaKetBan,
        TuChoi
    }
    public enum VaiTroThanhVienChatEnum
    {
        QuanTri,
        ThanhVien
    }
    public enum LoaiThongBao
    {
        MentionBaiDang,
        MentionBinhLuan,
        BinhLuanMoi,
        TraLoiBinhLuan,
        ReactionBaiDang,
        ThichBinhLuan,
        ChiaSeBaiDang
    }
}
