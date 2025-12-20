namespace DTO.NguoiDung
{
    public class TienDoNhiemVuDTO
    {
        public Guid MaNguoiDung { get; set; }
        public int MaNhiemVu { get; set; }
        public int TienDoHienTai { get; set; }
        public bool DaHoanThanh { get; set; }
        public bool DaNhanThuong { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
    }
}