namespace DTO.NguoiDung
{
    public class NguoiDungDTO
    {
        // Khóa chính
        public Guid MaNguoiDung { get; set; }

        // Thông tin đăng nhập
        public string? TenDangNhap { get; set; }
        public string? MatKhauMaHoa { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }

        // Thông tin cá nhân
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public byte? GioiTinh { get; set; }  // 0: Khác, 1: Nam, 2: Nữ

        // Liên kết
        public int MaVaiTro { get; set; }
        public int MaCapDo { get; set; }

        // Hình ảnh & Bio
        public string? HinhDaiDien { get; set; }
        public string? AnhBia { get; set; }
        public string? TieuSu { get; set; }

        // Tài chính ảo
        public int Vang { get; set; }
        public int KimCuong { get; set; }
        public int TongDiemXP { get; set; }

        // Thống kê học tập
        public int TongSoTheHoc { get; set; }
        public int TongSoTheDung { get; set; }
        public int TongThoiGianHocPhut { get; set; }

        // Streak
        public int ChuoiNgayHocLienTiep { get; set; }
        public int ChuoiNgayDaiNhat { get; set; }
        public int SoStreakFreeze { get; set; }
        public int SoStreakHoiSinh { get; set; }
        public DateTime? NgayHoatDongCuoi { get; set; }

        // Thách đấu
        public int SoTranThachDau { get; set; }
        public int SoTranThang { get; set; }
        public int SoTranThua { get; set; }

        // Trạng thái
        public bool DaXacThucEmail { get; set; }
        public bool TrangThaiOnline { get; set; }
        public DateTime? LanOnlineCuoi { get; set; }
        public bool DaXoa { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}