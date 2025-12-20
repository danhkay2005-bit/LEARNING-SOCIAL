namespace DTO.NguoiDung
{
    public class NhiemVuDTO
    {
        public int MaNhiemVu { get; set; }
        public string? TenNhiemVu { get; set; }
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }

        public string? LoaiNhiemVu { get; set; }  // HangNgay, HangTuan, ThanhTuu, SuKien
        public string? LoaiDieuKien { get; set; }
        public int DieuKienDatDuoc { get; set; }

        public int ThuongVang { get; set; }
        public int ThuongKimCuong { get; set; }
        public int ThuongXP { get; set; }

        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public bool ConHieuLuc { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}