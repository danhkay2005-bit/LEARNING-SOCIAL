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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class PostService : IPostService
    {
        private readonly SocialDbContext _context;
        private readonly UserDbContext _userContext;
        private readonly IMapper _mapper;

        public PostService(SocialDbContext context, UserDbContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        #region Helper Methods (Xử lý Hashtag)

        /// <summary>
        /// Tự động tách hashtag từ nội dung bài đăng và cập nhật bảng Hashtag
        /// </summary>
        private async Task HandleHashtagsAsync(int maBaiDang, string? noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung))
                return;

            // Regex lấy danh sách hashtag (#từ_khóa)
            var hashtagMatches = Regex.Matches(noiDung, @"#([\p{L}\p{N}_]+)");
            var hashtagNames = hashtagMatches.Cast<Match>()
                                             .Select(m => m.Groups[1].Value.ToLower().Trim())
                                             .Distinct()
                                             .ToList();

            if (!hashtagNames.Any())
                return;

            // 1. Xóa các liên kết hashtag cũ và giảm số lượt dùng
            var existingLinks = await _context.Set<BaiDang>()
                .Where(x => x.MaBaiDang == maBaiDang)
                .SelectMany(x => x.MaHashtags)
                .ToListAsync();

            foreach (var tag in existingLinks)
            {
                if (tag.SoLuotDung > 0)
                    tag.SoLuotDung--;
            }

            // Xóa liên kết cũ (many-to-many)
            var baiDang = await _context.Set<BaiDang>()
                .Include(x => x.MaHashtags)
                .FirstOrDefaultAsync(x => x.MaBaiDang == maBaiDang);

            if (baiDang != null)
            {
                baiDang.MaHashtags.Clear();
                await _context.SaveChangesAsync();
            }

            // 2. Xử lý hashtag mới
            foreach (var name in hashtagNames)
            {
                var tag = await _context.Set<Hashtag>()
                    .FirstOrDefaultAsync(t => t.TenHashtag.ToLower() == name);

                if (tag == null)
                {
                    // Tạo mới
                    tag = new Hashtag
                    {
                        TenHashtag = name,
                        SoLuotDung = 1,
                        DangThinhHanh = false,
                        ThoiGianTao = DateTime.Now
                    };
                    _context.Set<Hashtag>().Add(tag);
                }
                else
                {
                    // Tăng số lượt dùng
                    tag.SoLuotDung++;
                }

                await _context.SaveChangesAsync();

                // Thêm liên kết mới
                if (baiDang != null)
                {
                    baiDang.MaHashtags.Add(tag);
                }
            }

            await _context.SaveChangesAsync();
        }

        #endregion

        #region CRUD Operations

        public async Task<BaiDangResponse> CreatePostAsync(TaoBaiDangRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var baiDang = _mapper.Map<BaiDang>(request);
                baiDang.ThoiGianTao = DateTime.Now;
                baiDang.DaXoa = false;

                _context.Set<BaiDang>().Add(baiDang);
                await _context.SaveChangesAsync();

                // Xử lý hashtag tự động
                await HandleHashtagsAsync(baiDang.MaBaiDang, request.NoiDung);

                await transaction.CommitAsync();

                return _mapper.Map<BaiDangResponse>(baiDang);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BaiDangResponse> UpdatePostAsync(int postId, CapNhatBaiDangRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Set<BaiDang>()
                    .FirstOrDefaultAsync(x => x.MaBaiDang == postId && x.DaXoa != true);

                if (existing == null)
                    throw new KeyNotFoundException("Bài đăng không tồn tại.");

                _mapper.Map(request, existing);
                existing.ThoiGianSua = DateTime.Now;
                existing.DaChinhSua = true;

                await _context.SaveChangesAsync();

                // Xử lý hashtag
                await HandleHashtagsAsync(postId, request.NoiDung);

                await transaction.CommitAsync();

                return _mapper.Map<BaiDangResponse>(existing);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var existing = await _context.Set<BaiDang>().FindAsync(postId);
            if (existing == null)
                return false;

            // Soft delete
            existing.DaXoa = true;
            existing.ThoiGianSua = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<BaiDangResponse?> GetPostByIdAsync(int postId)
        {
            var baiDang = await _context.Set<BaiDang>()
                .Include(x => x.MaHashtags)
                .FirstOrDefaultAsync(x => x.MaBaiDang == postId && x.DaXoa != true);

            return _mapper.Map<BaiDangResponse>(baiDang);
        }

        #endregion

        #region Newsfeed & User Posts

        public async Task<List<BaiDangResponse>> GetNewsfeedAsync(Guid userId, int page = 1, int pageSize = 20)
        {
            var followingIds = await _context.Set<TheoDoiNguoiDung>()
                .Where(x => x.MaNguoiTheoDoi == userId)
                .Select(x => x.MaNguoiDuocTheoDoi)
                .ToListAsync();

            followingIds.Add(userId);

            var posts = await _context.Set<BaiDang>()
                .Where(x => followingIds.Contains(x.MaNguoiDung) && x.DaXoa != true)
                .OrderByDescending(x => x.ThoiGianTao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<BaiDangResponse>>(posts);

            // ✅ Load thông tin người dùng (avatar, tên)
            var userIds = result.Select(x => x.MaNguoiDung).Distinct().ToList();
            var users = await _userContext.NguoiDungs
                .Where(u => userIds.Contains(u.MaNguoiDung))
                .Select(u => new
                {
                    u.MaNguoiDung,
                    u.HoVaTen,
                    u.TenDangNhap,
                    u.HinhDaiDien
                })
                .ToListAsync();

            foreach (var post in result)
            {
                var user = users.FirstOrDefault(u => u.MaNguoiDung == post.MaNguoiDung);
                if (user != null)
                {
                    post.TenNguoiDung = user.HoVaTen ?? user.TenDangNhap;
                    post.HinhDaiDien = user.HinhDaiDien;
                }
            }

            return result;
        }

        public async Task<List<BaiDangResponse>> GetUserPostsAsync(Guid userId, int page = 1)
        {
            var posts = await _context.Set<BaiDang>()
                .Where(x => x.MaNguoiDung == userId && x.DaXoa != true)
                .OrderByDescending(x => x.ThoiGianTao)
                .Skip((page - 1) * 20)
                .Take(20)
                .ToListAsync();

            var result = _mapper.Map<List<BaiDangResponse>>(posts);

            // ✅ FIX: Load thông tin người dùng (avatar, tên) - Giống như GetNewsfeedAsync
            var userIds = result.Select(x => x.MaNguoiDung).Distinct().ToList();
            var users = await _userContext.NguoiDungs
                .Where(u => userIds.Contains(u.MaNguoiDung))
                .Select(u => new
                {
                    u.MaNguoiDung,
                    u.HoVaTen,
                    u.TenDangNhap,
                    u.HinhDaiDien
                })
                .ToListAsync();

            foreach (var post in result)
            {
                var user = users.FirstOrDefault(u => u.MaNguoiDung == post.MaNguoiDung);
                if (user != null)
                {
                    post.TenNguoiDung = user.HoVaTen ?? user.TenDangNhap;
                    post.HinhDaiDien = user.HinhDaiDien;
                }
            }

            return result;
        }

        #endregion

        #region Special Actions

        public async Task<bool> TogglePinPostAsync(int postId, bool pin)
        {
            var baiDang = await _context.Set<BaiDang>().FindAsync(postId);
            if (baiDang == null)
                return false;

            baiDang.GhimBaiDang = pin;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}