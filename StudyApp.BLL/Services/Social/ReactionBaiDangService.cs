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
            // ✅ FIX TRIỆT ĐỂ: Clear tracker và query với AsNoTracking
            _context.ChangeTracker.Clear();

            var reactionHienTai = await _context.ReactionBaiDangs
                .AsNoTracking() // ✅ THÊM: Query không track
                .FirstOrDefaultAsync(r => r.MaBaiDang == request.MaBaiDang && r.MaNguoiDung == request.MaNguoiDung);

            if (reactionHienTai != null)
            {
                // ✅ Cập nhật reaction hiện tại - Attach rồi mới update
                reactionHienTai.LoaiReaction = request.LoaiReaction.ToString();
                reactionHienTai.ThoiGian = DateTime.Now;
                
                // ✅ Attach entity vào context rồi mark là Modified
                _context.ReactionBaiDangs.Attach(reactionHienTai);
                _context.Entry(reactionHienTai).State = EntityState.Modified;
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
            
            // ✅ Detach sau khi save để tránh tracking conflict
            _context.Entry(reactionHienTai).State = EntityState.Detached;
            
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