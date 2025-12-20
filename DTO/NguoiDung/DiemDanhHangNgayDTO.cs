namespace DTO.NguoiDung
{
    public class DiemDanhHangNgayDTO
    {
        public int MaDiemDanh { get; set; }
        public Guid MaNguoiDung { get; set; }
        public DateTime NgayDiemDanh { get; set; }
        public int NgayThuMay { get; set; }  // 1-7

        public int ThuongVang { get; set; }
        public int ThuongXP { get; set; }
        public string? ThuongDacBiet { get; set; }
    }
}