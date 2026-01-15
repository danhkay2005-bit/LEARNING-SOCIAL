using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface ICommentService
    {
        Task<BinhLuanBaiDangResponse> CreateCommentAsync(TaoBinhLuanRequest request);
        Task<BinhLuanBaiDangResponse> UpdateCommentAsync(int commentId, CapNhatBinhLuanRequest request);
        Task<bool> DeleteCommentAsync(int commentId);

        Task<List<BinhLuanBaiDangResponse>> GetCommentsByPostAsync(int postId);
        Task<List<BinhLuanBaiDangResponse>> GetRepliesAsync(int parentCommentId);
    }
}