using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Social
{
    // Response người bị chặn
    public class NguoiBiChanResponse
    {
        public Guid MaNguoiBiChan { get; set; }
        public NguoiDungTomTatResponse? NguoiBiChan { get; set; }
        public string? LyDo { get; set; }
        public DateTime? ThoiGian { get; set; }
    }

    // Response danh sách người bị chặn
    public class DanhSachNguoiBiChanResponse
    {
        public List<NguoiBiChanResponse> NguoiBiChans { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response kết quả chặn/bỏ chặn
    public class KetQuaChanResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
    }
}