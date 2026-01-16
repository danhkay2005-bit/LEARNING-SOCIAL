using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class ReactionService : IReactionService
    {
        private readonly SocialDbContext _context;

        public ReactionService(SocialDbContext context)
        {
            _context = context;
        }

        #region Reaction Bài Đăng

        public async Task<bool> ReactToPostAsync(TaoHoacCapNhatReactionBaiDangRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Set<ReactionBaiDang>()
                    .FirstOrDefaultAsync(x => x.MaBaiDang == request.MaBaiDang && x.MaNguoiDung == request.MaNguoiDung);

                if (existing != null)
                {
                    // Nếu reaction giống nhau -> XÓA (unlike)
                    if (existing.LoaiReaction == request.LoaiReaction.ToString())
                    {
                        _context.Set<ReactionBaiDang>().Remove(existing);

                        // Giảm số reaction
                        var post = await _context.Set<BaiDang>().FindAsync(request.MaBaiDang);
                        if (post != null && post.SoReaction > 0)
                        {
                            post.SoReaction--;
                        }
                    }
                    else
                    {
                        // Thay đổi loại reaction
                        existing.LoaiReaction = request.LoaiReaction.ToString();
                        existing.ThoiGian = DateTime.Now;
                    }
                }
                else
                {
                    // Tạo mới reaction
                    var newReaction = new ReactionBaiDang
                    {
                        MaBaiDang = request.MaBaiDang,
                        MaNguoiDung = request.MaNguoiDung,
                        LoaiReaction = request.LoaiReaction.ToString(),
                        ThoiGian = DateTime.Now
                    };

                    _context.Set<ReactionBaiDang>().Add(newReaction);

                    // Tăng số reaction
                    var post = await _context.Set<BaiDang>().FindAsync(request.MaBaiDang);
                    if (post != null)
                    {
                        post.SoReaction = (post.SoReaction ?? 0) + 1;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> RemovePostReactionAsync(int postId, Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var reaction = await _context.Set<ReactionBaiDang>()
                    .FirstOrDefaultAsync(x => x.MaBaiDang == postId && x.MaNguoiDung == userId);

                if (reaction == null)
                    return false;

                _context.Set<ReactionBaiDang>().Remove(reaction);

                // Giảm số reaction
                var post = await _context.Set<BaiDang>().FindAsync(postId);
                if (post != null && post.SoReaction > 0)
                {
                    post.SoReaction--;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetPostReactionCountAsync(int postId, LoaiReactionEnum? type = null)
        {
            var query = _context.Set<ReactionBaiDang>()
                .Where(x => x.MaBaiDang == postId);

            if (type.HasValue)
            {
                query = query.Where(x => x.LoaiReaction == type.Value.ToString());
            }

            return await query.CountAsync();
        }

        #endregion

        #region Reaction Bình Luận

        public async Task<bool> ReactToCommentAsync(TaoHoacCapNhatReactionBinhLuanRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Set<ReactionBinhLuan>()
                    .FirstOrDefaultAsync(x => x.MaBinhLuan == request.MaBinhLuan && x.MaNguoiDung == request.MaNguoiDung);

                if (existing != null)
                {
                    // Nếu reaction giống nhau -> XÓA
                    if (existing.LoaiReaction == request.LoaiReaction.ToString())
                    {
                        _context.Set<ReactionBinhLuan>().Remove(existing);

                        // Giảm số reaction
                        var comment = await _context.Set<BinhLuanBaiDang>().FindAsync(request.MaBinhLuan);
                        if (comment != null && comment.SoLuotReaction > 0)
                        {
                            comment.SoLuotReaction--;
                        }
                    }
                    else
                    {
                        // Thay đổi loại reaction
                        existing.LoaiReaction = request.LoaiReaction.ToString();
                        existing.ThoiGian = DateTime.Now;
                    }
                }
                else
                {
                    // Tạo mới reaction
                    var newReaction = new ReactionBinhLuan
                    {
                        MaBinhLuan = request.MaBinhLuan,
                        MaNguoiDung = request.MaNguoiDung,
                        LoaiReaction = request.LoaiReaction.ToString(),
                        ThoiGian = DateTime.Now
                    };

                    _context.Set<ReactionBinhLuan>().Add(newReaction);

                    // Tăng số reaction
                    var comment = await _context.Set<BinhLuanBaiDang>().FindAsync(request.MaBinhLuan);
                    if (comment != null)
                    {
                        comment.SoLuotReaction = (comment.SoLuotReaction ?? 0) + 1;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> RemoveCommentReactionAsync(int commentId, Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var reaction = await _context.Set<ReactionBinhLuan>()
                    .FirstOrDefaultAsync(x => x.MaBinhLuan == commentId && x.MaNguoiDung == userId);

                if (reaction == null)
                    return false;

                _context.Set<ReactionBinhLuan>().Remove(reaction);

                // Giảm số reaction
                var comment = await _context.Set<BinhLuanBaiDang>().FindAsync(commentId);
                if (comment != null && comment.SoLuotReaction > 0)
                {
                    comment.SoLuotReaction--;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetCommentReactionCountAsync(int commentId)
        {
            return await _context.Set<ReactionBinhLuan>()
                .Where(x => x.MaBinhLuan == commentId)
                .CountAsync();
        }

        #endregion
    }
}