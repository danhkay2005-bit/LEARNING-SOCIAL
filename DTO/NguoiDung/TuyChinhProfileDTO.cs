namespace DTO.NguoiDung
{
    public class TuyChinhProfileDTO
    {
        public Guid MaNguoiDung { get; set; }

        public int? MaAvatarDangDung { get; set; }
        public int? MaKhungDangDung { get; set; }
        public int? MaHinhNenDangDung { get; set; }
        public int? MaHieuUngDangDung { get; set; }
        public int? MaThemeDangDung { get; set; }
        public int? MaNhacNenDangDung { get; set; }
        public int? MaBadgeDangDung { get; set; }

        public int? MaHuyHieuHienThi { get; set; }
        public string? CauChamNgon { get; set; }

        public DateTime ThoiGianCapNhat { get; set; }
    }
}