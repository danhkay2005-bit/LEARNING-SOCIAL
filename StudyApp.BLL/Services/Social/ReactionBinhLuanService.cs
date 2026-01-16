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
    public class ReactionBinhLuanService : IReactionBinhLuanService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public ReactionBinhLuanService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReactionBinhLuanResponse> TaoHoacCapNhatReactionAsync(TaoHoacCapNhatReactionBinhLuanRequest request)
        {
            var reactionHienTai = await _context.ReactionBinhLuans
                .FirstOrDefaultAsync(r => r.MaBinhLuan == request.MaBinhLuan && r.MaNguoiDung == request.MaNguoiDung);

            if (reactionHienTai != null)
            {
                // Cập nhật reaction hiện tại
                reactionHienTai.LoaiReaction = request.LoaiReaction.ToString();
                reactionHienTai.ThoiGian = DateTime.Now;
                _context.ReactionBinhLuans.Update(reactionHienTai);
            }
            else
            {
                // Thêm reaction mới
                var reactionMoi = _mapper.Map<ReactionBinhLuan>(request);
                await _context.ReactionBinhLuans.AddAsync(reactionMoi);

                // Tăng số lượng reaction của bình luận
                var binhLuan = await _context.BinhLuanBaiDangs.FindAsync(request.MaBinhLuan);
                if (binhLuan != null)
                {
                    binhLuan.SoLuotReaction = (binhLuan.SoLuotReaction ?? 0) + 1;
                    _context.BinhLuanBaiDangs.Update(binhLuan);
                }

                reactionHienTai = reactionMoi;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ReactionBinhLuanResponse>(reactionHienTai);
        }

        public async Task<bool> XoaReactionAsync(XoaReactionBinhLuanRequest request)
        {
            var reaction = await _context.ReactionBinhLuans
                .FirstOrDefaultAsync(r => r.MaBinhLuan == request.MaBinhLuan && r.MaNguoiDung == request.MaNguoiDung);

            if (reaction == null)
            {
                return false;
            }

            _context.ReactionBinhLuans.Remove(reaction);

            // Giảm số lượng reaction của bình luận
            var binhLuan = await _context.BinhLuanBaiDangs.FindAsync(request.MaBinhLuan);
            if (binhLuan != null && binhLuan.SoLuotReaction > 0)
            {
                binhLuan.SoLuotReaction = binhLuan.SoLuotReaction - 1;
                _context.BinhLuanBaiDangs.Update(binhLuan);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReactionBinhLuanResponse>> LayDanhSachReactionTheoMaBinhLuanAsync(int maBinhLuan)
        {
            var reactions = await _context.ReactionBinhLuans
                .Where(r => r.MaBinhLuan == maBinhLuan)
                .OrderByDescending(r => r.ThoiGian)
                .ToListAsync();

            return _mapper.Map<List<ReactionBinhLuanResponse>>(reactions);
        }

        public async Task<List<ThongKeReactionResponse>> LayThongKeReactionTheoMaBinhLuanAsync(int maBinhLuan)
        {
            var thongKe = await _context.ReactionBinhLuans
                .Where(r => r.MaBinhLuan == maBinhLuan)
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

        public async Task<ReactionBinhLuanResponse?> KiemTraReactionCuaNguoiDungAsync(int maBinhLuan, Guid maNguoiDung)
        {
            var reaction = await _context.ReactionBinhLuans
                .FirstOrDefaultAsync(r => r.MaBinhLuan == maBinhLuan && r.MaNguoiDung == maNguoiDung);

            return reaction != null ? _mapper.Map<ReactionBinhLuanResponse>(reaction) : null;
        }

        public async Task<int> LayTongSoReactionAsync(int maBinhLuan)
        {
            return await _context.ReactionBinhLuans
                .Where(r => r.MaBinhLuan == maBinhLuan)
                .CountAsync();
        }
    }
}