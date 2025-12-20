namespace DTO.NguoiDung
{
    public class BaoVeChuoiNgayDTO
    {
        public int MaBaoVe { get; set; }
        public Guid MaNguoiDung { get; set; }
        public DateTime NgaySuDung { get; set; }
        public string? LoaiBaoVe { get; set; }  // Freeze, HoiSinh
        public int? ChuoiNgayTruocKhi { get; set; }
        public int? ChuoiNgaySauKhi { get; set; }
    }
}