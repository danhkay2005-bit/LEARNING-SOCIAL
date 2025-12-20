namespace DTO.NguoiDung
{
    public class LichSuGiaoDichDTO
    {
        public int MaGiaoDich { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string? LoaiGiaoDich { get; set; }  // MuaVatPham, NhanThuong, DiemDanh... 
        public string? LoaiTien { get; set; }       // Vang, KimCuong, XP
        public int SoLuong { get; set; }           // Âm = trừ, Dương = cộng
        public int SoDuTruoc { get; set; }
        public int SoDuSau { get; set; }

        public string? MoTa { get; set; }
        public int? MaVatPham { get; set; }
        public Guid? MaNguoiLienQuan { get; set; }

        public DateTime ThoiGian { get; set; }
    }
}