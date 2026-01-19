using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class HashtagService : IHashtagService
    {
        private readonly SocialDbContext _context;
        private readonly UserDbContext _userContext;
        private readonly IMapper _mapper;

        public HashtagService(SocialDbContext context, UserDbContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        /// <summary>
        /// ?? Tìm ki?m bài vi?t theo hashtag
        /// 
        /// ? CH? L?Y BÀI CÔNG KHAI ?? m?i ng??i ??u có th? tìm ki?m
        /// </summary>
        public async Task<List<BaiDangResponse>> TimKiemBaiDangTheoHashtagAsync(string tenHashtag, int page = 1, int pageSize = 20)
        {
            // Chu?n hóa tên hashtag (b? d?u # n?u có, lowercase)
            var normalizedTag = tenHashtag.TrimStart('#').ToLower().Trim();

            // Tìm hashtag
            var hashtag = await _context.Set<Hashtag>()
                .Include(h => h.MaBaiDangs)
                .FirstOrDefaultAsync(h => h.TenHashtag.ToLower() == normalizedTag);

            if (hashtag == null)
                return new List<BaiDangResponse>();

            // ? FIX: CH? L?Y BÀI CÔNG KHAI
            var posts = await _context.Set<BaiDang>()
                .Where(p => p.MaHashtags.Any(h => h.MaHashtag == hashtag.MaHashtag) 
                         && p.DaXoa != true
                         && p.QuyenRiengTu == 1) // ? 1 = CongKhai (enum value)
                .OrderByDescending(p => p.ThoiGianTao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<BaiDangResponse>>(posts);

            // ? Load thông tin ng??i dùng (avatar, tên) - QUAN TR?NG!
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

        /// <summary>
        /// ?? L?y danh sách hashtag trending (TOP s? d?ng nhi?u nh?t)
        /// </summary>
        public async Task<List<HashtagResponse>> LayDanhSachHashtagTrendingAsync(int top = 10)
        {
            var hashtags = await _context.Set<Hashtag>()
                .OrderByDescending(h => h.SoLuotDung)
                .Take(top)
                .ToListAsync();

            return _mapper.Map<List<HashtagResponse>>(hashtags);
        }

        /// <summary>
        /// ?? G?i ý hashtag khi user gõ (autocomplete)
        /// </summary>
        public async Task<List<HashtagResponse>> GoiYHashtagAsync(string tuKhoa, int limit = 5)
        {
            var normalizedKeyword = tuKhoa.TrimStart('#').ToLower().Trim();

            var hashtags = await _context.Set<Hashtag>()
                .Where(h => h.TenHashtag.ToLower().Contains(normalizedKeyword))
                .OrderByDescending(h => h.SoLuotDung)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<List<HashtagResponse>>(hashtags);
        }

        /// <summary>
        /// ?? L?y thông tin chi ti?t 1 hashtag
        /// </summary>
        public async Task<HashtagResponse?> LayThongTinHashtagAsync(string tenHashtag)
        {
            var normalizedTag = tenHashtag.TrimStart('#').ToLower().Trim();

            var hashtag = await _context.Set<Hashtag>()
                .FirstOrDefaultAsync(h => h.TenHashtag.ToLower() == normalizedTag);

            if (hashtag == null)
                return null;

            return _mapper.Map<HashtagResponse>(hashtag);
        }
    }
}
