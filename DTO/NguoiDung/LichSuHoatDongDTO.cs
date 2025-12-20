namespace DTO.NguoiDung
{
    public class LichSuHoatDongDTO
    {
        public int MaHoatDong { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string? LoaiHoatDong { get; set; }  // DangNhap, HocThe, TaoBoDe... 
        public string? MoTa { get; set; }
        public string? DuLieuThem { get; set; }  // JSON

        public DateTime ThoiGian { get; set; }
    }
}