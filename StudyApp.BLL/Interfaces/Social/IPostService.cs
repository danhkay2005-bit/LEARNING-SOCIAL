using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    //quản lí bài đăng
    public interface IPostService
    {
        // CRUD Bài đăng
        Task<BaiDangResponse> CreatePostAsync(TaoBaiDangRequest request);
        Task<BaiDangResponse> UpdatePostAsync(int postId, CapNhatBaiDangRequest request);
        Task<bool> DeletePostAsync(int postId);
        Task<BaiDangResponse?> GetPostByIdAsync(int postId);

        // Newsfeed
        Task<List<BaiDangResponse>> GetNewsfeedAsync(Guid userId, int page = 1, int pageSize = 20);
        Task<List<BaiDangResponse>> GetUserPostsAsync(Guid userId, int page = 1);

        // Tương tác
        Task<bool> TogglePinPostAsync(int postId, bool pin);
        Task<int> GetTotalPostsCountAsync();
    }
}