using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class ReactionBaiDangService : IReactionBaiDangService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public ReactionBaiDangService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReactionBaiDangResponse> TaoHoacCapNhatReactionAsync(TaoHoacCapNhatReactionBaiDangRequest request)
        {
            var reactionHienTai = await _context.ReactionBaiDangs
                .FirstOrDefaultAsync(r => r.MaBaiDang == request.MaBaiDang && r.MaNguoiDung == request.MaNguoiDung);

            if (reactionHienTai != null)
            {
                // Cập nhật reaction hiện tại
                reactionHienTai.LoaiReaction = request.LoaiReaction.ToString();
                reactionHienTai.ThoiGian = DateTime.Now;
                _context.ReactionBaiDangs.Update(reactionHienTai);
            }
            else
            {
                // Thêm reaction mới
                var reactionMoi = _mapper.Map<ReactionBaiDang>(request);
                await _context.ReactionBaiDangs.AddAsync(reactionMoi);

                // Tăng số lượng reaction của bài đăng
                var baiDang = await _context.BaiDangs.FindAsync(request.MaBaiDang);
                if (baiDang != null)
                {
                    baiDang.SoReaction = (baiDang.SoReaction ?? 0) + 1;
                    _context.BaiDangs.Update(baiDang);
                }

                reactionHienTai = reactionMoi;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ReactionBaiDangResponse>(reactionHienTai);
        }

        public async Task<bool> XoaReactionAsync(XoaReactionBaiDangRequest request)
        {
            var reaction = await _context.ReactionBaiDangs
                .FirstOrDefaultAsync(r => r.MaBaiDang == request.MaBaiDang && r.MaNguoiDung == request.MaNguoiDung);

            if (reaction == null)
            {
                return false;
            }

            _context.ReactionBaiDangs.Remove(reaction);

            // Giảm số lượng reaction của bài đăng
            var baiDang = await _context.BaiDangs.FindAsync(request.MaBaiDang);
            if (baiDang != null && baiDang.SoReaction > 0)
            {
                baiDang.SoReaction = baiDang.SoReaction - 1;
                _context.BaiDangs.Update(baiDang);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReactionBaiDangResponse>> LayDanhSachReactionTheoMaBaiDangAsync(int maBaiDang)
        {
            var reactions = await _context.ReactionBaiDangs
                .Where(r => r.MaBaiDang == maBaiDang)
                .OrderByDescending(r => r.ThoiGian)
                .ToListAsync();

            return _mapper.Map<List<ReactionBaiDangResponse>>(reactions);
        }

        public async Task<List<ThongKeReactionResponse>> LayThongKeReactionTheoMaBaiDangAsync(int maBaiDang)
        {
            var thongKe = await _context.ReactionBaiDangs
                .Where(r => r.MaBaiDang == maBaiDang)
                .GroupBy(r => r.LoaiReaction)
                .Select(g => new ThongKeReactionResponse
                {
                    LoaiReaction = Enum.Parse<LoaiReactionEnum>(g.Key ?? "Thich"),
                    SoLuong = g.Count()
                })
                .OrderByDescending(t => t.SoLuong)
                .ToListAsync();

            return thongKe;
        }

        public async Task<ReactionBaiDangResponse?> KiemTraReactionCuaNguoiDungAsync(int maBaiDang, Guid maNguoiDung)
        {
            var reaction = await _context.ReactionBaiDangs
                .FirstOrDefaultAsync(r => r.MaBaiDang == maBaiDang && r.MaNguoiDung == maNguoiDung);

            return reaction != null ? _mapper.Map<ReactionBaiDangResponse>(reaction) : null;
        }

        public async Task<int> LayTongSoReactionAsync(int maBaiDang)
        {
            return await _context.ReactionBaiDangs
                .Where(r => r.MaBaiDang == maBaiDang)
                .CountAsync();
        }
    }
}