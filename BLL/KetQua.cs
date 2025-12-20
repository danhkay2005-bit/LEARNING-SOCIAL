namespace BLL
{
    public class KetQua
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public object? DuLieu { get; set; }

        // Constructor cơ bản
        public KetQua(bool thanhCong, string thongBao)
        {
            ThanhCong = thanhCong;
            ThongBao = thongBao;
            DuLieu = null;
        }

        // Constructor có dữ liệu
        public KetQua(bool thanhCong, string thongBao, object duLieu)
        {
            ThanhCong = thanhCong;
            ThongBao = thongBao;
            DuLieu = duLieu;
        }
    }
}