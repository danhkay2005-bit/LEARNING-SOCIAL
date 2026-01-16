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
    public class MentionBinhLuanService : IMentionBinhLuanService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public MentionBinhLuanService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MentionBinhLuanResponse>> GetDanhSachMentionByBinhLuanAsync(int maBinhLuan)
        {
            var mentions = await _context.MentionBinhLuans
                .Where(x => x.MaBinhLuan == maBinhLuan)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MentionBinhLuanResponse>>(mentions);
        }

        public async Task<IEnumerable<MentionBinhLuanResponse>> GetDanhSachBinhLuanDuocMentionAsync(Guid maNguoiDung)
        {
            var mentions = await _context.MentionBinhLuans
                .Where(x => x.MaNguoiDuocMention == maNguoiDung)
                .Include(x => x.MaBinhLuanNavigation)
                .Where(x => x.MaBinhLuanNavigation.DaXoa == false)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MentionBinhLuanResponse>>(mentions);
        }

        public async Task<MentionBinhLuanResponse> ThemMentionAsync(int maBinhLuan, Guid maNguoiDuocMention)
        {
            // Kiểm tra bình luận có tồn tại không
            var binhLuan = await _context.BinhLuanBaiDangs
                .FirstOrDefaultAsync(x => x.MaBinhLuan == maBinhLuan && x.DaXoa == false);

            if (binhLuan == null)
            {
                throw new Exception("Bình luận không tồn tại hoặc đã bị xóa.");
            }

            // Kiểm tra mention đã tồn tại chưa
            var mentionExist = await _context.MentionBinhLuans
                .AnyAsync(x => x.MaBinhLuan == maBinhLuan && x.MaNguoiDuocMention == maNguoiDuocMention);

            if (mentionExist)
            {
                throw new Exception("Người dùng đã được mention trong bình luận này.");
            }

            // Tạo mention mới
            var mention = new MentionBinhLuan
            {
                MaBinhLuan = maBinhLuan,
                MaNguoiDuocMention = maNguoiDuocMention,
                ThoiGian = DateTime.Now
            };

            _context.MentionBinhLuans.Add(mention);
            await _context.SaveChangesAsync();

            return _mapper.Map<MentionBinhLuanResponse>(mention);
        }

        public async Task<bool> XoaMentionAsync(int maBinhLuan, Guid maNguoiDuocMention)
        {
            var mention = await _context.MentionBinhLuans
                .FirstOrDefaultAsync(x => x.MaBinhLuan == maBinhLuan && x.MaNguoiDuocMention == maNguoiDuocMention);

            if (mention == null)
            {
                return false;
            }

            _context.MentionBinhLuans.Remove(mention);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> KiemTraDuocMentionAsync(int maBinhLuan, Guid maNguoiDung)
        {
            return await _context.MentionBinhLuans
                .AnyAsync(x => x.MaBinhLuan == maBinhLuan && x.MaNguoiDuocMention == maNguoiDung);
        }

        public async Task<Dictionary<int, IEnumerable<MentionBinhLuanResponse>>> GetDanhSachMentionByBinhLuansAsync(List<int> maBinhLuans)
        {
            var mentions = await _context.MentionBinhLuans
                .Where(x => maBinhLuans.Contains(x.MaBinhLuan))
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            var result = mentions
                .GroupBy(x => x.MaBinhLuan)
                .ToDictionary(
                    g => g.Key,
                    g => _mapper.Map<IEnumerable<MentionBinhLuanResponse>>(g.ToList())
                );

            return result;
        }
    }
}