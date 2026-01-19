// File: StudyApp.BLL\Interfaces\Social\ICommentService.cs

public interface ICommentService
{
    // ===== CREATE =====
    /// <summary>
    /// T?o bình lu?n m?i (top-level ho?c reply)
    /// </summary>
    Task<BinhLuanBaiDangResponse> CreateCommentAsync(TaoBinhLuanRequest request);

    // ===== UPDATE =====
    Task<BinhLuanBaiDangResponse> UpdateCommentAsync(int commentId, CapNhatBinhLuanRequest request);

    // ===== DELETE =====
    Task<bool> DeleteCommentAsync(int commentId);

    // ===== READ =====
    /// <summary>
    /// L?y danh sách TOP-LEVEL comments c?a bài ??ng
    /// </summary>
    Task<DanhSachBinhLuanResponse> GetCommentsByPostAsync(
        int postId, 
        int page = 1, 
        int pageSize = 10);

    /// <summary>
    /// ??? L?y danh sách REPLIES c?a 1 comment ???
    /// </summary>
    Task<DanhSachBinhLuanResponse> GetRepliesAsync(
        int parentCommentId, 
        int page = 1, 
        int pageSize = 5);

    /// <summary>
    /// L?y chi ti?t 1 comment (bao g?m replies)
    /// </summary>
    Task<BinhLuanBaiDangResponse?> GetCommentDetailAsync(int commentId, bool includeReplies = true);

    /// <summary>
    /// ??m s? l??ng replies c?a 1 comment
    /// </summary>
    Task<int> CountRepliesAsync(int parentCommentId);
}