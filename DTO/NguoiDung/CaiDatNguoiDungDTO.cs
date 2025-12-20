namespace DTO.NguoiDung
{
    public class CaiDatNguoiDungDTO
    {
        public Guid MaNguoiDung { get; set; }

        // Giao diện
        public bool CheDoToi { get; set; }
        public bool CoHieuUng { get; set; }

        // Thông báo
        public bool ThongBaoNhacHoc { get; set; }
        public TimeSpan GioNhacHoc { get; set; }
        public bool ThongBaoThanhTuu { get; set; }
        public bool ThongBaoXaHoi { get; set; }
        public bool ThongBaoThachDau { get; set; }

        // Quyền riêng tư
        public bool HienThiTrangThai { get; set; }
        public bool HienThiThongKe { get; set; }
        public bool ChoPhepThachDau { get; set; }
        public bool ChoPhepNhanTin { get; set; }
        public bool ChoPhepTagTrongBaiDang { get; set; }

        public DateTime ThoiGianCapNhat { get; set; }
    }
}