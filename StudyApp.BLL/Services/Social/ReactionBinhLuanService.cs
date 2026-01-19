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
            try
            {
                // Clear tracking để tránh conflict
                _context.ChangeTracker.Clear();
                
                // Không dùng AsNoTracking vì cần update entity này
                var reactionHienTai = await _context.ReactionBinhLuans
                    .FirstOrDefaultAsync(r => r.MaBinhLuan == request.MaBinhLuan && r.MaNguoiDung == request.MaNguoiDung);

                if (reactionHienTai != null)
                {
                    // Cập nhật reaction hiện tại (entity đã được track)
                    reactionHienTai.LoaiReaction = request.LoaiReaction.ToString();
                    reactionHienTai.ThoiGian = DateTime.Now;
                    
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // ⭐ Tạo entity mới TRỰC TIẾP thay vì dùng AutoMapper
                    var reactionMoi = new ReactionBinhLuan
                    {
                        MaBinhLuan = request.MaBinhLuan,
                        MaNguoiDung = request.MaNguoiDung,
                        LoaiReaction = request.LoaiReaction.ToString(),
                        ThoiGian = DateTime.Now
                        // ❌ KHÔNG set MaBinhLuanNavigation - để EF tự handle
                    };
                    
                    // Bước 1: ExecuteUpdate BinhLuan TRƯỚC (transaction riêng)
                    await _context.BinhLuanBaiDangs
                        .Where(b => b.MaBinhLuan == request.MaBinhLuan)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(b => b.SoLuotReaction, b => (b.SoLuotReaction ?? 0) + 1));

                    // Bước 2: Clear tracking sau ExecuteUpdate
                    _context.ChangeTracker.Clear();

                    // Bước 3: Add Reaction SAU
                    await _context.ReactionBinhLuans.AddAsync(reactionMoi);

                    // Bước 4: SaveChanges
                    await _context.SaveChangesAsync();
                    
                    reactionHienTai = reactionMoi;
                }
                
                // Clear tracking sau khi hoàn thành để tránh conflict cho lần click tiếp theo
                _context.ChangeTracker.Clear();
                
                return _mapper.Map<ReactionBinhLuanResponse>(reactionHienTai);
            }
            catch (Exception ex)
            {
                // Clear tracking khi có lỗi
                _context.ChangeTracker.Clear();
                
                // Log chi tiết lỗi
                var innerMsg = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Lỗi khi tạo/cập nhật reaction: {innerMsg}", ex);
            }
        }

        public async Task<bool> XoaReactionAsync(XoaReactionBinhLuanRequest request)
        {
            try
            {
                // Clear tracking để tránh conflict
                _context.ChangeTracker.Clear();
                
                // Không dùng AsNoTracking vì cần Remove entity này
                var reaction = await _context.ReactionBinhLuans
                    .FirstOrDefaultAsync(r => r.MaBinhLuan == request.MaBinhLuan && r.MaNguoiDung == request.MaNguoiDung);

                if (reaction == null)
                {
                    return false;
                }

                // Giảm số lượng reaction của bình luận TRƯỚC - trong transaction riêng
                await _context.BinhLuanBaiDangs
                    .Where(b => b.MaBinhLuan == request.MaBinhLuan && b.SoLuotReaction > 0)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(b => b.SoLuotReaction, b => b.SoLuotReaction - 1));

                // Sau đó mới remove reaction
                _context.ReactionBinhLuans.Remove(reaction);
                await _context.SaveChangesAsync();
                
                // Clear tracked entities để tránh conflict trong WinForms
                _context.ChangeTracker.Clear();
                
                return true;
            }
            catch (Exception ex)
            {
                // Clear tracking khi có lỗi
                _context.ChangeTracker.Clear();
                throw new Exception($"Lỗi khi xóa reaction: {ex.Message}", ex);
            }
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
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.MaBinhLuan == maBinhLuan && r.MaNguoiDung == maNguoiDung);

            return reaction != null ? _mapper.Map<ReactionBinhLuanResponse>(reaction) : null;
        }

        public async Task<int> LayTongSoReactionAsync(int maBinhLuan)
        {
            return await _context.ReactionBinhLuans
                .Where(r => r.MaBinhLuan == maBinhLuan)
                .CountAsync();
        }

        public async Task<List<BinhLuanBaiDangResponse>> LayToanBoCayCommentTheoBaiDangAsync(int postId)
        {
            // Load toàn bộ cây comment với 1 query
            var comments = await _context.BinhLuanBaiDangs
                .FromSqlRaw(@"
                    WITH CommentTree AS (
                        -- Anchor: Top-level comments
                        SELECT 
                            MaBinhLuan,
                            MaBaiDang,
                            NoiDung,
                            MaBinhLuanCha,
                            MaNguoiDung,
                            ThoiGianTao,
                            0 AS Level
                        FROM BinhLuanBaiDang
                        WHERE MaBaiDang = {0}
                          AND MaBinhLuanCha IS NULL
                          AND DaXoa != 1

                        UNION ALL

                        -- Recursive: Replies
                        SELECT 
                            c.MaBinhLuan,
                            c.MaBaiDang,
                            c.NoiDung,
                            c.MaBinhLuanCha,
                            c.MaNguoiDung,
                            c.ThoiGianTao,
                            ct.Level + 1
                        FROM BinhLuanBaiDang c
                        INNER JOIN CommentTree ct ON c.MaBinhLuanCha = ct.MaBinhLuan
                        WHERE c.DaXoa != 1
                    )
                    SELECT * FROM CommentTree
                    ORDER BY Level, ThoiGianTao;
                ", postId)
                .ToListAsync();

            // Map Entity → DTO
            return _mapper.Map<List<BinhLuanBaiDangResponse>>(comments);
        }
    }
}