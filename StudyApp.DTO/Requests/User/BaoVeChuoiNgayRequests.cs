using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.User
{
    /// <summary>
    /// Request sử dụng vật phẩm bảo vệ chuỗi ngày
    /// </summary>
    public class SuDungBaoVeChuoiNgayRequest
    {
        public Guid MaNguoiDung { get; set; }

        public DateOnly NgaySuDung { get; set; }

        public LoaiBaoVeChuoiEnum LoaiBaoVe { get; set; }
    }
}
