using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using System;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IReactionService
    {
        //Quản lý reactions
        // Bài đăng
        Task<bool> ReactToPostAsync(TaoHoacCapNhatReactionBaiDangRequest request);
        Task<bool> RemovePostReactionAsync(int postId, Guid userId);
        Task<int> GetPostReactionCountAsync(int postId, LoaiReactionEnum? type = null);

        // Bình luận
        Task<bool> ReactToCommentAsync(TaoHoacCapNhatReactionBinhLuanRequest request);
        Task<bool> RemoveCommentReactionAsync(int commentId, Guid userId);
        Task<int> GetCommentReactionCountAsync(int commentId);
    }
}