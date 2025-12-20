namespace DTO.NguoiDung
{
    public class VatPhamDTO
    {
        public int MaVatPham { get; set; }
        public string? TenVatPham { get; set; }
        public string? MoTa { get; set; }
        public int Gia { get; set; }
        public int MaLoaiTien { get; set; }
        public int MaDanhMuc { get; set; }
        public string? DuongDanHinh { get; set; }
        public string? DuongDanFile { get; set; }

        // Thuộc tính đặc biệt
        public int? ThoiHanPhut { get; set; }
        public double? GiaTriHieuUng { get; set; }

        public byte DoHiem { get; set; }  // 1-5 sao
        public int SoLuongBanRa { get; set; }
        public bool ConHang { get; set; }
        public int GioiHanSoLuong { get; set; }  // -1 = không giới hạn

        public DateTime ThoiGianTao { get; set; }
    }
}