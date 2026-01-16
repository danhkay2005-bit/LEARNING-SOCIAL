using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class CommentService : ICommentService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BinhLuanBaiDangResponse> CreateCommentAsync(TaoBinhLuanRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var comment = _mapper.Map<BinhLuanBaiDang>(request);
                comment.ThoiGianTao = DateTime.Now;
                comment.DaXoa = false;

                _context.Set<BinhLuanBaiDang>().Add(comment);

                // Tăng số lượng bình luận của bài đăng
                var post = await _context.Set<BaiDang>().FindAsync(request.MaBaiDang);
                if (post != null)
                {
                    post.SoBinhLuan = (post.SoBinhLuan ?? 0) + 1;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<BinhLuanBaiDangResponse>(comment);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BinhLuanBaiDangResponse> UpdateCommentAsync(int commentId, CapNhatBinhLuanRequest request)
        {
            var existing = await _context.Set<BinhLuanBaiDang>()
                .FirstOrDefaultAsync(x => x.MaBinhLuan == commentId && x.DaXoa != true);

            if (existing == null)
                throw new KeyNotFoundException("Bình luận không tồn tại.");

            _mapper.Map(request, existing);
            existing.ThoiGianSua = DateTime.Now;
            existing.DaChinhSua = true;

            await _context.SaveChangesAsync();

            return _mapper.Map<BinhLuanBaiDangResponse>(existing);
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var comment = await _context.Set<BinhLuanBaiDang>().FindAsync(commentId);
                if (comment == null)
                    return false;

                // Soft delete
                comment.DaXoa = true;
                comment.ThoiGianSua = DateTime.Now;

                // Giảm số lượng bình luận của bài đăng
                var post = await _context.Set<BaiDang>().FindAsync(comment.MaBaiDang);
                if (post != null && post.SoBinhLuan > 0)
                {
                    post.SoBinhLuan--;
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

        public async Task<List<BinhLuanBaiDangResponse>> GetCommentsByPostAsync(int postId)
        {
            var comments = await _context.Set<BinhLuanBaiDang>()
                .Where(x => x.MaBaiDang == postId && x.DaXoa != true && x.MaBinhLuanCha == null) // Chỉ lấy comment gốc
                .OrderByDescending(x => x.ThoiGianTao)
                .ToListAsync();

            return _mapper.Map<List<BinhLuanBaiDangResponse>>(comments);
        }

        public async Task<List<BinhLuanBaiDangResponse>> GetRepliesAsync(int parentCommentId)
        {
            var replies = await _context.Set<BinhLuanBaiDang>()
                .Where(x => x.MaBinhLuanCha == parentCommentId && x.DaXoa != true)
                .OrderBy(x => x.ThoiGianTao) // Reply hiển thị từ cũ đến mới
                .ToListAsync();

            return _mapper.Map<List<BinhLuanBaiDangResponse>>(replies);
        }
    }
}