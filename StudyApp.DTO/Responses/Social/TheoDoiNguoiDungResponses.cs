using System;

namespace StudyApp.DTO.Responses.Social
{
    /// <summary>
    /// Response thông tin theo dõi người dùng
    /// </summary>
    public class TheoDoiNguoiDungResponse
    {
        public Guid MaNguoiTheoDoi { get; set; }
        public Guid MaNguoiDuocTheoDoi { get; set; }

        public DateTime? ThoiGian { get; set; }
    }

    /// <summary>
    /// Response trạng thái theo dõi
    /// </summary>
    public class TrangThaiTheoDoiResponse
    {
        public bool DangTheoDoi { get; set; }
    }
}
