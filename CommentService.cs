// File: StudyApp.BLL\Services\Social\CommentService.cs

public class CommentService : ICommentService
{
    private readonly SocialDbContext _socialContext;
    private readonly UserDbContext _userContext;
    private readonly IMapper _mapper;

    public CommentService(
        SocialDbContext socialContext,
        UserDbContext userContext,
        IMapper mapper)
    {
        _socialContext = socialContext;
        _userContext = userContext;
        _mapper = mapper;
    }

    // ========================================
    // CREATE COMMENT (TOP-LEVEL OR REPLY)
    // ========================================
    public async Task<BinhLuanBaiDangResponse> CreateCommentAsync(TaoBinhLuanRequest request)
    {
        using var transaction = await _socialContext.Database.BeginTransactionAsync();
        try
        {
            // B??C 1: Validate bài ??ng t?n t?i
            var post = await _socialContext.BaiDangs
                .FirstOrDefaultAsync(p => p.MaBaiDang == request.MaBaiDang && p.DaXoa != true);

            if (post == null)
                throw new Exception("Bài ??ng không t?n t?i ho?c ?ã b? xóa");

            // B??C 2: N?u là REPLY, validate comment cha t?n t?i
            if (request.MaBinhLuanCha.HasValue)
            {
                var parentComment = await _socialContext.BinhLuanBaiDangs
                    .FirstOrDefaultAsync(c => c.MaBinhLuan == request.MaBinhLuanCha.Value
                                           && c.MaBaiDang == request.MaBaiDang
                                           && c.DaXoa != true);

                if (parentComment == null)
                    throw new Exception("Bình lu?n cha không t?n t?i");
            }

            // B??C 3: Map Request ? Entity
            var comment = _mapper.Map<BinhLuanBaiDang>(request);
            comment.ThoiGianTao = DateTime.Now;
            comment.DaXoa = false;
            comment.DaChinhSua = false;
            comment.SoLuotReaction = 0;

            // B??C 4: Insert comment
            _socialContext.BinhLuanBaiDangs.Add(comment);

            // B??C 5: ? T?ng counter bài ??ng
            post.SoBinhLuan = (post.SoBinhLuan ?? 0) + 1;
            _socialContext.BaiDangs.Update(post);

            await _socialContext.SaveChangesAsync();
            await transaction.CommitAsync();

            // B??C 6: Load thông tin user và map response
            var response = await EnrichCommentResponseAsync(comment);

            return response;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // ========================================
    // GET TOP-LEVEL COMMENTS
    // ========================================
    public async Task<DanhSachBinhLuanResponse> GetCommentsByPostAsync(
        int postId, 
        int page = 1, 
        int pageSize = 10)
    {
        // B??C 1: ??? CH? L?Y TOP-LEVEL COMMENTS ???
        var query = _socialContext.BinhLuanBaiDangs
            .Where(c => c.MaBaiDang == postId
                     && c.DaXoa != true
                     && c.MaBinhLuanCha == null)  // ? NULL = top-level
            .OrderByDescending(c => c.ThoiGianTao);

        // B??C 2: Pagination
        var totalCount = await query.CountAsync();
        var comments = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // B??C 3: Load user info + enrich
        var enrichedComments = new List<BinhLuanBaiDangResponse>();
        foreach (var comment in comments)
        {
            var enriched = await EnrichCommentResponseAsync(comment);
            
            // ? ??m s? l??ng replies
            enriched.SoLuotReplies = await CountRepliesAsync(comment.MaBinhLuan);
            
            enrichedComments.Add(enriched);
        }

        return new DanhSachBinhLuanResponse
        {
            Comments = enrichedComments,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    // ========================================
    // ??? GET REPLIES (NESTED COMMENTS) ???
    // ========================================
    public async Task<DanhSachBinhLuanResponse> GetRepliesAsync(
        int parentCommentId, 
        int page = 1, 
        int pageSize = 5)
    {
        // B??C 1: ??? CH? L?Y REPLIES C?A COMMENT CHA ???
        var query = _socialContext.BinhLuanBaiDangs
            .Where(c => c.MaBinhLuanCha == parentCommentId  // ? Filter by parent
                     && c.DaXoa != true)
            .OrderBy(c => c.ThoiGianTao);  // Replies: c? ? m?i (th? t? thread)

        // B??C 2: Pagination
        var totalCount = await query.CountAsync();
        var replies = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // B??C 3: Load user info + enrich
        var enrichedReplies = new List<BinhLuanBaiDangResponse>();
        foreach (var reply in replies)
        {
            var enriched = await EnrichCommentResponseAsync(reply);
            
            // ? Load thông tin ng??i ???c reply (comment cha)
            if (reply.MaBinhLuanCha.HasValue)
            {
                var parentComment = await _socialContext.BinhLuanBaiDangs
                    .FirstOrDefaultAsync(c => c.MaBinhLuan == reply.MaBinhLuanCha.Value);

                if (parentComment != null)
                {
                    var parentUser = await _userContext.NguoiDungs
                        .FirstOrDefaultAsync(u => u.MaNguoiDung == parentComment.MaNguoiDung);

                    enriched.TenNguoiDungCha = parentUser?.HoVaTen ?? parentUser?.TenDangNhap;
                }
            }

            enrichedReplies.Add(enriched);
        }

        return new DanhSachBinhLuanResponse
        {
            Comments = enrichedReplies,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    // ========================================
    // GET COMMENT DETAIL (WITH NESTED REPLIES)
    // ========================================
    public async Task<BinhLuanBaiDangResponse?> GetCommentDetailAsync(
        int commentId, 
        bool includeReplies = true)
    {
        var comment = await _socialContext.BinhLuanBaiDangs
            .FirstOrDefaultAsync(c => c.MaBinhLuan == commentId && c.DaXoa != true);

        if (comment == null)
            return null;

        var response = await EnrichCommentResponseAsync(comment);

        // ? N?u yêu c?u load replies
        if (includeReplies)
        {
            var repliesResult = await GetRepliesAsync(commentId, page: 1, pageSize: 100);
            response.Replies = repliesResult.Comments;
            response.SoLuotReplies = repliesResult.TotalCount;
        }

        return response;
    }

    // ========================================
    // COUNT REPLIES
    // ========================================
    public async Task<int> CountRepliesAsync(int parentCommentId)
    {
        return await _socialContext.BinhLuanBaiDangs
            .Where(c => c.MaBinhLuanCha == parentCommentId && c.DaXoa != true)
            .CountAsync();
    }

    // ========================================
    // DELETE COMMENT
    // ========================================
    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        using var transaction = await _socialContext.Database.BeginTransactionAsync();
        try
        {
            var comment = await _socialContext.BinhLuanBaiDangs
                .Include(c => c.InverseMaBinhLuanChaNavigation) // Load replies
                .FirstOrDefaultAsync(c => c.MaBinhLuan == commentId);

            if (comment == null)
                return false;

            // ?? CHECK: Có replies hay không?
            var hasReplies = comment.InverseMaBinhLuanChaNavigation.Any(r => r.DaXoa != true);

            if (hasReplies)
            {
                // ? STRATEGY 1: Soft delete (gi? l?i c?u trúc cây)
                comment.DaXoa = true;
                comment.NoiDung = "[Bình lu?n ?ã b? xóa]";
                comment.ThoiGianSua = DateTime.Now;
            }
            else
            {
                // ? STRATEGY 2: Hard delete (không có replies)
                _socialContext.BinhLuanBaiDangs.Remove(comment);
            }

            // ? Gi?m counter bài ??ng
            var post = await _socialContext.BaiDangs
                .FirstOrDefaultAsync(p => p.MaBaiDang == comment.MaBaiDang);

            if (post != null && post.SoBinhLuan > 0)
            {
                post.SoBinhLuan--;
                _socialContext.BaiDangs.Update(post);
            }

            await _socialContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // ========================================
    // HELPER: ENRICH COMMENT RESPONSE
    // ========================================
    private async Task<BinhLuanBaiDangResponse> EnrichCommentResponseAsync(BinhLuanBaiDang comment)
    {
        var response = _mapper.Map<BinhLuanBaiDangResponse>(comment);

        // Load user info t? UserDb
        var user = await _userContext.NguoiDungs
            .FirstOrDefaultAsync(u => u.MaNguoiDung == comment.MaNguoiDung);

        if (user != null)
        {
            response.TenDangNhap = user.TenDangNhap;
            response.HoVaTen = user.HoVaTen;
            response.HinhDaiDien = user.HinhDaiDien;
        }

        return response;
    }
}