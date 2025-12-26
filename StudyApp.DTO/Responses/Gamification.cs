using StudyApp.DTO.Commons;
using StudyApp.DTO.Entities;
using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Responses
{
    public class NhanThuongThanhTuuResponse: DtoKetQuaCoBan
    {
        public ThanhTuuDto? ThanhTuu { get; set; }
    }

    public class DiemDanhHangNgayResponse: DtoKetQuaCoBan {
     public DiemDanhHangNgayDto? DiemDanh { get; set; }
    }
    public class TienDoNhiemVuResponse: DtoKetQuaCoBan {
    public NhiemVuDto? NhiemVu { get; set; }
    }
    public class BaoVeChuoiNgayResponse: DtoKetQuaCoBan {
        public BaoVeDto? BaoVe { get; set; }
    } 
}
