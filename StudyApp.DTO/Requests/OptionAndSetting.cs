using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StudyApp.DTO.Requests
{
    public class CapNhatHoSoTuyChinhRequest
    {
        public string? MaAvatarDangDung { get; set; }
        public string? MaKhungDangDung { get; set; }
        public string? MaHinhNenDangDung { get; set; }
        public string? MaHieuUngDangDung { get; set; }
        public string? MaThemeDangDung { get; set; }
        public string? MaBadgeDangDung { get; set; }
        public string? MaHuyHieuHienThi { get; set; }
        public string? CauChamNgon { get; set; }

    }

    public class CapNhatCaiDatNguoiDungRequest
    {
        public bool? CheDoToi {  get; set; }
        public bool? CoHieuUng {  get; set; }

        public bool? ThongBaoNhacHoc {  get; set; }
        public TimeSpan? GioNhacHoc { get; set; }
        public bool? ThongBaoThachDau { get; set; }
        public bool? ThongBaoMangXaHoi { get; set; }
        public bool? HienThiTrangThaiOnline { get; set; }
        public bool? ChoPhepThachDau { get; set; }
        public bool? ChoPhepNhanTin { get; set; }
        public bool? ChoPhepTag { get; set; }
    }
}
