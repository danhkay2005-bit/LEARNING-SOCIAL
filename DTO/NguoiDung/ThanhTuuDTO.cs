namespace DTO.NguoiDung
{
    public class ThanhTuuDTO
    {
        public int MaThanhTuu { get; set; }
        public string? TenThanhTuu { get; set; }
        public string? MoTa { get; set; }
        public string? BieuTuong { get; set; }
        public string? HinhHuy { get; set; }

        public string? LoaiThanhTuu { get; set; }  // HocTap, ChuoiNgay, XaHoi... 
        public string? DieuKienLoai { get; set; }
        public int DieuKienGiaTri { get; set; }

        public int ThuongXP { get; set; }
        public int ThuongVang { get; set; }
        public int ThuongKimCuong { get; set; }

        public byte DoHiem { get; set; }  // 1-5 sao
        public bool BiAn { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}