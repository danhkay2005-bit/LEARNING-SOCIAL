namespace DTO.NguoiDung
{
    public class KhoNguoiDungDTO
    {
        public int MaKho { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaVatPham { get; set; }
        public int SoLuong { get; set; }
        public bool DaTrangBi { get; set; }
        public DateTime ThoiGianMua { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
    }
}