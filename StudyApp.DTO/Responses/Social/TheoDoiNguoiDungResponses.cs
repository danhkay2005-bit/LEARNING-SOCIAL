using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin reaction bình luận
    /// </summary>
    public class TheoDoiNguoiDungResponse
    {
        public Guid MaNguoiTheoDoi { get; set; }

        public Guid MaNguoiDuocTheoDoi { get; set; }

        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response kiểm tra trạng thái theo dõi
    /// </summary>
    public class TrangThaiTheoDoiResponse
    {
        public bool DangTheoDoi { get; set; }
    }
}
