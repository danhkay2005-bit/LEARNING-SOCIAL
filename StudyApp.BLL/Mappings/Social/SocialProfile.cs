using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Mappings.Social
{
    /// <summary>
    /// AutoMapper Profile tổng hợp cho các entity Social
    /// (Hiện tại để trống vì đã có các Profile riêng cho từng entity)
    /// </summary>
    public class SocialProfile : Profile
    {
        public SocialProfile()
        {
            // Các mapping đã được định nghĩa ở: 
            // - BaiDangProfile
            // - BinhLuanBaiDangProfile
            // - ChiaSeBaiDangProfile
            // - HashtagProfile
            // - ReactionBaiDangProfile
            // - ReactionBinhLuanProfile
            // - TheoDoiNguoiDungProfile
            // - ThongBaoProfile

            // File này có thể dùng để thêm các custom mapping chung nếu cần
        }
    }
}