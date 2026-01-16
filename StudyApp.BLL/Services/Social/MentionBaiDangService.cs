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
    public class MentionBaiDangService : IMentionBaiDangService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public MentionBaiDangService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MentionBaiDangResponse>> GetDanhSachMentionByBaiDangAsync(int maBaiDang)
        {
            var mentions = await _context.MentionBaiDangs
                .Where(x => x.MaBaiDang == maBaiDang)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MentionBaiDangResponse>>(mentions);
        }

        public async Task<IEnumerable<MentionBaiDangResponse>> GetDanhSachBaiDangDuocMentionAsync(Guid maNguoiDung)
        {
            var mentions = await _context.MentionBaiDangs
                .Where(x => x.MaNguoiDuocMention == maNguoiDung)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MentionBaiDangResponse>>(mentions);
        }

        public async Task<MentionBaiDangResponse> ThemMentionAsync(int maBaiDang, Guid maNguoiDuocMention)
        {
            // Kiểm tra bài đăng có tồn tại không
            var baiDang = await _context.BaiDangs
                .FirstOrDefaultAsync(x => x.MaBaiDang == maBaiDang && x.DaXoa == false);

            if (baiDang == null)
            {
                throw new Exception("Bài đăng không tồn tại hoặc đã bị xóa.");
            }

            // Kiểm tra mention đã tồn tại chưa
            var mentionExist = await _context.MentionBaiDangs
                .AnyAsync(x => x.MaBaiDang == maBaiDang && x.MaNguoiDuocMention == maNguoiDuocMention);

            if (mentionExist)
            {
                throw new Exception("Người dùng đã được mention trong bài đăng này.");
            }

            // Tạo mention mới
            var mention = new MentionBaiDang
            {
                MaBaiDang = maBaiDang,
                MaNguoiDuocMention = maNguoiDuocMention,
                ThoiGian = DateTime.Now
            };

            _context.MentionBaiDangs.Add(mention);
            await _context.SaveChangesAsync();

            return _mapper.Map<MentionBaiDangResponse>(mention);
        }

        public async Task<bool> XoaMentionAsync(int maBaiDang, Guid maNguoiDuocMention)
        {
            var mention = await _context.MentionBaiDangs
                .FirstOrDefaultAsync(x => x.MaBaiDang == maBaiDang && x.MaNguoiDuocMention == maNguoiDuocMention);

            if (mention == null)
            {
                return false;
            }

            _context.MentionBaiDangs.Remove(mention);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> KiemTraDuocMentionAsync(int maBaiDang, Guid maNguoiDung)
        {
            return await _context.MentionBaiDangs
                .AnyAsync(x => x.MaBaiDang == maBaiDang && x.MaNguoiDuocMention == maNguoiDung);
        }
    }
}