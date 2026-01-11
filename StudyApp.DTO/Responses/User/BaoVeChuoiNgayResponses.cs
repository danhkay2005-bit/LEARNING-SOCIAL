using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.User
{
    /// <summary>
    /// Response thông tin bảo vệ chuỗi ngày
    /// </summary>
    public class BaoVeChuoiNgayResponse
    {
        public int MaBaoVe { get; set; }

        public Guid MaNguoiDung { get; set; }

        public DateOnly NgaySuDung { get; set; }

        public LoaiBaoVeChuoiEnum LoaiBaoVe { get; set; }

        public int? ChuoiNgayTruocKhi { get; set; }

        public int? ChuoiNgaySauKhi { get; set; }
    }
}
