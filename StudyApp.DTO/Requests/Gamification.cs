using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Requests
{
    public class NhanThuongThanhTuuRequest
    {
        public string? MaThanhTuu { get; set; }
    }

    public class  DiemDanhHangNgayRequest
    {
        
    }

    public class TienDoNhiemVuRequest
    {
        public string? MaNhiemVu { get; set; }
        public int TienDo { get; set; }
    }

    public class  BaoVeChuoiNgayRequest
    {
        public LoaiBaoVeStreakEnum LoaiBaoVe { get; set; }
    }
}
