namespace DTO.NguoiDung
{
    public class ThanhTuuDatDuocDTO
    {
        public Guid MaNguoiDung { get; set; }
        public int MaThanhTuu { get; set; }
        public DateTime NgayDat { get; set; }
        public bool DaXem { get; set; }
        public bool DaChiaSe { get; set; }
    }
}