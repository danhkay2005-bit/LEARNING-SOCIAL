namespace DTO.NguoiDung
{
    public class BoostDangHoatDongDTO
    {
        public int MaBoost { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }

        public string? LoaiBoost { get; set; }  // XP, Vang
        public double HeSoNhan { get; set; }   // 1.5, 2, 3... 

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public bool ConHieuLuc { get; set; }
    }
}