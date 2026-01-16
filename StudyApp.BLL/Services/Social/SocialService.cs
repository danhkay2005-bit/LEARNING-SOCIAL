using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class SocialService : ISocialService
    {
        private readonly SocialDbContext _socialContext;
        private readonly UserDbContext _userContext;

        public SocialService(SocialDbContext socialContext, UserDbContext userContext)
        {
            _socialContext = socialContext;
            _userContext = userContext;
        }

        #region Follow/Unfollow

        public async Task<bool> FollowUserAsync(Guid followerId, Guid followingId)
        {
            // Ki?m tra không t? follow chính mình
            if (followerId == followingId)
                return false;

            // Ki?m tra ?ã follow ch?a
            var existing = await _socialContext.Set<TheoDoiNguoiDung>()
                .FirstOrDefaultAsync(x => x.MaNguoiTheoDoi == followerId && x.MaNguoiDuocTheoDoi == followingId);

            if (existing != null)
                return true; // ?ã follow r?i

            // T?o m?i
            var follow = new TheoDoiNguoiDung
            {
                MaNguoiTheoDoi = followerId,
                MaNguoiDuocTheoDoi = followingId,
                ThoiGian = DateTime.Now
            };

            _socialContext.Set<TheoDoiNguoiDung>().Add(follow);

            return await _socialContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnfollowUserAsync(Guid followerId, Guid followingId)
        {
            var existing = await _socialContext.Set<TheoDoiNguoiDung>()
                .FirstOrDefaultAsync(x => x.MaNguoiTheoDoi == followerId && x.MaNguoiDuocTheoDoi == followingId);

            if (existing == null)
                return false;

            _socialContext.Set<TheoDoiNguoiDung>().Remove(existing);

            return await _socialContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsFollowingAsync(Guid followerId, Guid followingId)
        {
            return await _socialContext.Set<TheoDoiNguoiDung>()
                .AnyAsync(x => x.MaNguoiTheoDoi == followerId && x.MaNguoiDuocTheoDoi == followingId);
        }

        #endregion

        #region Statistics

        public async Task<int> GetFollowerCountAsync(Guid userId)
        {
            return await _socialContext.Set<TheoDoiNguoiDung>()
                .Where(x => x.MaNguoiDuocTheoDoi == userId)
                .CountAsync();
        }

        public async Task<int> GetFollowingCountAsync(Guid userId)
        {
            return await _socialContext.Set<TheoDoiNguoiDung>()
                .Where(x => x.MaNguoiTheoDoi == userId)
                .CountAsync();
        }

        #endregion

        #region Suggested Users

        public async Task<List<NguoiDungDTO>> GetSuggestedUsersAsync(Guid userId, int take = 5)
        {
            // Logic:  G?i ý ng??i dùng mà user ch?a follow
            var followingIds = await _socialContext.Set<TheoDoiNguoiDung>()
                .Where(x => x.MaNguoiTheoDoi == userId)
                .Select(x => x.MaNguoiDuocTheoDoi)
                .ToListAsync();

            followingIds.Add(userId); // Lo?i tr? chính mình

            // L?y ng??i dùng có nhi?u follower nh?t mà ch?a follow
            var suggestedUserIds = await _socialContext.Set<TheoDoiNguoiDung>()
                .Where(x => !followingIds.Contains(x.MaNguoiDuocTheoDoi))
                .GroupBy(x => x.MaNguoiDuocTheoDoi)
                .OrderByDescending(g => g.Count())
                .Take(take)
                .Select(g => g.Key)
                .ToListAsync();

            // L?y thông tin t? UserDb
            var users = await _userContext.Set<StudyApp.DAL.Entities.User.NguoiDung>()
                .Where(x => suggestedUserIds.Contains(x.MaNguoiDung))
                .Select(x => new NguoiDungDTO
                {
                    MaNguoiDung = x.MaNguoiDung,
                    TenDangNhap = x.TenDangNhap,
                    HoVaTen = x.HoVaTen,
                    HinhDaiDien = x.HinhDaiDien
                })
                .ToListAsync();

            return users;
        }

        #endregion
    }
}