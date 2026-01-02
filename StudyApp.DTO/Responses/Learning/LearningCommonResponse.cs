using System;

namespace StudyApp.DTO.Responses.Learning
{
    // Response kết quả chung
    public class KetQuaLearningResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public string? MaLoi { get; set; }
    }

    // Response kết quả với dữ liệu
    public class KetQuaLearningResponse<T> : KetQuaLearningResponse
    {
        public T? Data { get; set; }
    }

    // Response phân trang chung cho Learning
    public class PhanTrangLearningResponse<T>
    {
        public List<T> Items { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public int SoItemMoiTrang { get; set; }
        public bool CoTrangTruoc { get; set; }
        public bool CoTrangTiep { get; set; }
    }

    // Response dashboard học tập
    public class DashboardHocTapResponse
    {
        public MucTieuHomNayResponse? MucTieuHomNay { get; set; }
        public int SoTheCanOnTapHomNay { get; set; }
        public int ChuoiNgayHienTai { get; set; }
        public List<BoDeHocTomTatResponse>? BoDeGanDay { get; set; }
        public List<BoDeHocTomTatResponse>? BoDeGoiY { get; set; }
        public ThongKeTongHopResponse? ThongKeTuan { get; set; }
        public List<ThanhTuuMoKhoaResponse>? ThanhTuuGanDay { get; set; }
    }

    // Response xếp hạng
    public class XepHangResponse
    {
        public int ThuHang { get; set; }
        public Guid MaNguoiDung { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? AnhDaiDien { get; set; }
        public int Diem { get; set; }
        public int ChuoiNgay { get; set; }
        public int TongTheHoc { get; set; }
    }

    // Response bảng xếp hạng
    public class BangXepHangResponse
    {
        public List<XepHangResponse> XepHangs { get; set; } = [];
        public XepHangResponse? XepHangCuaToi { get; set; }
        public string LoaiXepHang { get; set; } = null!; // "ngay", "tuan", "thang", "tat_ca"
        public DateTime? ThoiGianCapNhat { get; set; }
    }
}