namespace StudyApp.DTO.Requests.Learn
{
    public class LichSuThachDauRequest
    {
        // ID người dùng tham gia (Dùng Guid để khớp với Identity)
        public Guid MaNguoiDung { get; set; }

        // Mã PIN của phòng thách đấu (MaThachDau trong bảng ThachDau)
        public int MaThachDau { get; set; }

        // Mã bộ đề đang thách đấu
        public int MaBoDe { get; set; }
    }

    public class CapNhatKetQuaThachDauRequest
    {
        public int MaThachDau { get; set; }
        public Guid MaNguoiDung { get; set; }

        // THÊM DÒNG NÀY
        public int MaBoDe { get; set; }

        public int? Diem { get; set; }
        public int? SoTheDung { get; set; }
        public int? SoTheSai { get; set; }
        public int? ThoiGianLamBaiGiay { get; set; }
    }
}