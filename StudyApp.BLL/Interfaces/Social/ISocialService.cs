using StudyApp.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    //(Interface chính cho tính năng MXH
    /// <summary>
    /// Service xử lý các tính năng MXH cơ bản
    /// </summary>
    public interface ISocialService
    {
        // Follow/Unfollow
        Task<bool> FollowUserAsync(Guid followerId, Guid followingId);
        Task<bool> UnfollowUserAsync(Guid followerId, Guid followingId);
        Task<bool> IsFollowingAsync(Guid followerId, Guid followingId);

        // Thống kê
        Task<int> GetFollowerCountAsync(Guid userId);
        Task<int> GetFollowingCountAsync(Guid userId);

        // Gợi ý người dùng
        Task<List<NguoiDungDTO>> GetSuggestedUsersAsync(Guid userId, int take = 5);
    }
}