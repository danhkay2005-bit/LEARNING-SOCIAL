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
        private readonly SocialDbContext _socialContext;
        private readonly UserDbContext _userContext;
        private readonly IMapper _mapper;

        public CommentService(SocialDbContext socialContext, UserDbContext userContext, IMapper mapper)
        {
            _socialContext = socialContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<BinhLuanBaiDangResponse> CreateCommentAsync(TaoBinhLuanRequest request)
        {
            using var transaction = await _socialContext.Database.BeginTransactionAsync();
            try
            {
                var comment = _mapper.Map<BinhLuanBaiDang>(request);
                comment.ThoiGianTao = DateTime.Now;
                comment.DaXoa = false;

                _socialContext.Set<BinhLuanBaiDang>().Add(comment);

                // Tăng số lượng bình luận của bài đăng
                var post = await _socialContext.Set<BaiDang>().FindAsync(request.MaBaiDang);
                if (post != null)
                {
                    post.SoBinhLuan = (post.SoBinhLuan ?? 0) + 1;
                }

                await _socialContext.SaveChangesAsync();
                await transaction.CommitAsync();

                // Lấy thông tin người dùng để trả về
                var response = _mapper.Map<BinhLuanBaiDangResponse>(comment);
                var nguoiDung = await _userContext.NguoiDungs.FindAsync(comment.MaNguoiDung);
                if (nguoiDung != null)
                {
                    response.TenDangNhap = nguoiDung.TenDangNhap;
                    response.HoVaTen = nguoiDung.HoVaTen;
                    response.HinhDaiDien = nguoiDung.HinhDaiDien;
                }

                return response;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BinhLuanBaiDangResponse> UpdateCommentAsync(int commentId, CapNhatBinhLuanRequest request)
        {
            var existing = await _socialContext.Set<BinhLuanBaiDang>()
                .FirstOrDefaultAsync(x => x.MaBinhLuan == commentId && x.DaXoa != true);

            if (existing == null)
                throw new KeyNotFoundException("Bình luận không tồn tại.");

            _mapper.Map(request, existing);
            existing.ThoiGianSua = DateTime.Now;
            existing.DaChinhSua = true;

            await _socialContext.SaveChangesAsync();

            // Lấy thông tin người dùng để trả về
            var response = _mapper.Map<BinhLuanBaiDangResponse>(existing);
            var nguoiDung = await _userContext.NguoiDungs.FindAsync(existing.MaNguoiDung);
            if (nguoiDung != null)
            {
                response.TenDangNhap = nguoiDung.TenDangNhap;
                response.HoVaTen = nguoiDung.HoVaTen;
                response.HinhDaiDien = nguoiDung.HinhDaiDien;
            }

            return response;
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            using var transaction = await _socialContext.Database.BeginTransactionAsync();
            try
            {
                var comment = await _socialContext.Set<BinhLuanBaiDang>().FindAsync(commentId);
                if (comment == null)
                    return false;

                // Soft delete
                comment.DaXoa = true;
                comment.ThoiGianSua = DateTime.Now;

                // Giảm số lượng bình luận của bài đăng
                var post = await _socialContext.Set<BaiDang>().FindAsync(comment.MaBaiDang);
                if (post != null && post.SoBinhLuan > 0)
                {
                    post.SoBinhLuan--;
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

        public async Task<List<BinhLuanBaiDangResponse>> GetCommentsByPostAsync(int postId)
        {
            // Lấy danh sách bình luận
            var comments = await _socialContext.Set<BinhLuanBaiDang>()
                .Where(x => x.MaBaiDang == postId && x.DaXoa != true && x.MaBinhLuanCha == null)
                .OrderByDescending(x => x.ThoiGianTao)
                .ToListAsync();

            if (!comments.Any())
                return new List<BinhLuanBaiDangResponse>();

            // Lấy danh sách MaNguoiDung
            var danhSachMaNguoiDung = comments.Select(x => x.MaNguoiDung).Distinct().ToList();

            // Lấy thông tin người dùng
            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => danhSachMaNguoiDung.Contains(x.MaNguoiDung))
                .Select(x => new
                {
                    x.MaNguoiDung,
                    x.TenDangNhap,
                    x.HoVaTen,
                    x.HinhDaiDien
                })
                .ToListAsync();

            // GHI LOG RA FILE
            var logPath = @"C:\Temp\comment_log.txt";
            try
            {
                var logContent = $"=== {DateTime.Now} ===\n";
                logContent += $"Số comment: {comments.Count}\n";
                logContent += $"Số user: {thongTinNguoiDung.Count}\n";
                foreach (var user in thongTinNguoiDung)
                {
                    logContent += $"User: {user.TenDangNhap}, HoVaTen: {user.HoVaTen}, HinhDaiDien: {user.HinhDaiDien ?? "NULL"}\n";
                }
                System.IO.File.AppendAllText(logPath, logContent + "\n");
            }
            catch { }

            // Map và join thông tin
            var result = comments.Select(comment =>
            {
                var response = _mapper.Map<BinhLuanBaiDangResponse>(comment);
                var nguoiDung = thongTinNguoiDung.FirstOrDefault(x => x.MaNguoiDung == comment.MaNguoiDung);
                if (nguoiDung != null)
                {
                    response.TenDangNhap = nguoiDung.TenDangNhap;
                    response.HoVaTen = nguoiDung.HoVaTen;
                    response.HinhDaiDien = nguoiDung.HinhDaiDien;
                }
                return response;
            }).ToList();

            return result;
        }

        public async Task<List<BinhLuanBaiDangResponse>> GetRepliesAsync(int parentCommentId)
        {
            // Lấy danh sách reply
            var replies = await _socialContext.Set<BinhLuanBaiDang>()
                .Where(x => x.MaBinhLuanCha == parentCommentId && x.DaXoa != true)
                .OrderBy(x => x.ThoiGianTao)
                .ToListAsync();

            if (!replies.Any())
                return new List<BinhLuanBaiDangResponse>();

            // Lấy danh sách MaNguoiDung
            var danhSachMaNguoiDung = replies.Select(x => x.MaNguoiDung).Distinct().ToList();

            // Lấy thông tin người dùng
            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => danhSachMaNguoiDung.Contains(x.MaNguoiDung))
                .Select(x => new
                {
                    x.MaNguoiDung,
                    x.TenDangNhap,
                    x.HoVaTen,
                    x.HinhDaiDien
                })
                .ToListAsync();

            // Map và join thông tin
            var result = replies.Select(reply =>
            {
                var response = _mapper.Map<BinhLuanBaiDangResponse>(reply);
                var nguoiDung = thongTinNguoiDung.FirstOrDefault(x => x.MaNguoiDung == reply.MaNguoiDung);
                if (nguoiDung != null)
                {
                    response.TenDangNhap = nguoiDung.TenDangNhap;
                    response.HoVaTen = nguoiDung.HoVaTen;
                    response.HinhDaiDien = nguoiDung.HinhDaiDien;
                }
                return response;
            }).ToList();

            return result;
        }
    }
}