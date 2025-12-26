using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Enums
{
    public enum CamXucHocEnum : byte
    {
        RatTe = 1,
        HoiMet = 2,
        BinhThuong = 3,
        HungThu = 4,
        TuyetVoi = 5
    }
    public enum LoaiDanhDauTheEnum
    {
        YeuThich,
        Kho,
        CanOnLai,
        DaThanhThao,
        TamBo
    }
    public enum LoaiMucTieuEnum
    {
        TheHangNgay,    // Số thẻ học mỗi ngày
        PhutHangNgay,   // Số phút học mỗi ngày
        ChuoiNgay,      // Đạt chuỗi streak
        HoanThanhBoDe,  // Học hết một bộ đề
        TyLeDung         // Đạt tỷ lệ đúng mục tiêu
    }
    public enum LoaiPhienHocEnum
    {
        HocMoi,
        OnTap,
        KiemTra,
        ThachDau,
        TuyChon
    }
    public enum LoaiTheEnum
    {
        CoBan,      // Thẻ mặt trước - mặt sau đơn giản
        TracNghiem, // Chọn 1 hoặc nhiều đáp án
        DienKhuyet, // Điền từ vào chỗ trống
        GhepCap,    // Nối vế trái với vế phải
        SapXep,     // Sắp xếp các phần tử theo thứ tự
        NgheViet,   // Nghe audio và viết lại
        HinhAnh     // Nhìn ảnh đoán từ/khái niệm
    }
    public enum LyDoBaoCaoEnum
    {
        NoiDungSai,
        ViPhamBanQuyen,
        NgonNguKhongPhuHop,
        Spam,
        Khac
    }
    public enum MucDoKhoEnum : byte
    {
        RatDe = 1,
        De = 2,
        TrungBinh = 3,
        Kho = 4,
        RatKho = 5
    }
    public enum ThuTuHocEnum
    {
        TuDong,
        ThuTu,
        NgauNhien
    }
    public enum TrangThaiBaoCaoEnum
    {
        ChoDuyet,
        DaXuLy,
        TuChoi
    }
    public enum TrangThaiSRSEnum : byte
    {
        New = 0,      // Thẻ mới chưa học
        Learning = 1, // Đang trong giai đoạn ghi nhớ ngắn hạn
        Review = 2,   // Giai đoạn ôn tập dài hạn
        Mastered = 3  // Đã thành thạo
    }
}
