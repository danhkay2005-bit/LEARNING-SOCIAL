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
    public class ChiaSeBaiDangService : IChiaSeBaiDangService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public ChiaSeBaiDangService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChiaSeBaiDangResponse> ChiaSeBaiDangAsync(ChiaSeBaiDangRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Kiểm tra bài đăng gốc có tồn tại không
                var baiDangGoc = await _context.BaiDangs
                    .FirstOrDefaultAsync(x => x.MaBaiDang == request.MaBaiDangGoc && x.DaXoa == false);

                if (baiDangGoc == null)
                {
                    throw new Exception("Bài đăng gốc không tồn tại hoặc đã bị xóa.");
                }

                // Kiểm tra quyền truy cập bài đăng gốc
                if (baiDangGoc.QuyenRiengTu == (byte)QuyenRiengTuEnum.RiengTu && 
                    baiDangGoc.MaNguoiDung != request.MaNguoiChiaSe)
                {
                    throw new Exception("Bạn không có quyền chia sẻ bài đăng này.");
                }

                // Tạo bài đăng mới khi chia sẻ 
                var baiDangMoi = new BaiDang
                {
                    MaNguoiDung = request.MaNguoiChiaSe,
                    NoiDung = request.NoiDungThem,
                    LoaiBaiDang = "ChiaSe",
                    QuyenRiengTu = (byte)request.QuyenRiengTu,
                    SoReaction = 0,
                    SoBinhLuan = 0,
                    GhimBaiDang = false,
                    TatBinhLuan = false,
                    DaChinhSua = false,
                    DaXoa = false,
                    ThoiGianTao = DateTime.Now
                };

                _context.BaiDangs.Add(baiDangMoi);
                await _context.SaveChangesAsync();

                // Tạo record chia sẻ
                var chiaSe = new ChiaSeBaiDang
                {
                    MaBaiDangGoc = request.MaBaiDangGoc,
                    MaNguoiChiaSe = request.MaNguoiChiaSe,
                    NoiDungThem = request.NoiDungThem,
                    MaBaiDangMoi = baiDangMoi.MaBaiDang,
                    ThoiGian = DateTime.Now
                };

                _context.ChiaSeBaiDangs.Add(chiaSe);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Load lại dữ liệu để trả về đầy đủ
                var result = await _context.ChiaSeBaiDangs
                    .Include(x => x.MaBaiDangGocNavigation)
                    .Include(x => x.MaBaiDangMoiNavigation)
                    .FirstOrDefaultAsync(x => x.MaChiaSe == chiaSe.MaChiaSe);

                return _mapper.Map<ChiaSeBaiDangResponse>(result);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ChiaSeBaiDangResponse>> GetDanhSachChiaSeByBaiDangAsync(int maBaiDang)
        {
            var list = await _context.ChiaSeBaiDangs
                .Include(x => x.MaBaiDangGocNavigation)
                .Include(x => x.MaBaiDangMoiNavigation)
                .Where(x => x.MaBaiDangGoc == maBaiDang)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChiaSeBaiDangResponse>>(list);
        }

        public async Task<IEnumerable<ChiaSeBaiDangResponse>> GetDanhSachChiaSeByNguoiDungAsync(Guid maNguoiDung)
        {
            var list = await _context.ChiaSeBaiDangs
                .Include(x => x.MaBaiDangGocNavigation)
                .Include(x => x.MaBaiDangMoiNavigation)
                .Where(x => x.MaNguoiChiaSe == maNguoiDung)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChiaSeBaiDangResponse>>(list);
        }

        public async Task<ThongKeChiaSeBaiDangResponse> GetThongKeChiaSeAsync(int maBaiDang)
        {
            var thongKe = await _context.ChiaSeBaiDangs
                .Where(x => x.MaBaiDangGoc == maBaiDang)
                .GroupBy(x => x.MaBaiDangGoc)
                .Select(g => new ThongKeChiaSeBaiDangResponse
                {
                    MaBaiDang = g.Key,
                    TongSoChiaSe = g.Count(),
                    LanChiaSeMoiNhat = g.Max(x => x.ThoiGian)
                })
                .FirstOrDefaultAsync();

            return thongKe ?? new ThongKeChiaSeBaiDangResponse
            {
                MaBaiDang = maBaiDang,
                TongSoChiaSe = 0,
                LanChiaSeMoiNhat = null
            };
        }

        public async Task<bool> XoaChiaSeAsync(int maChiaSe, Guid maNguoiDung)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var chiaSe = await _context.ChiaSeBaiDangs
                    .Include(x => x.MaBaiDangMoiNavigation)
                    .FirstOrDefaultAsync(x => x.MaChiaSe == maChiaSe && x.MaNguoiChiaSe == maNguoiDung);

                if (chiaSe == null)
                {
                    return false;
                }

                // Xóa bài đăng mới (soft delete)
                if (chiaSe.MaBaiDangMoiNavigation != null)
                {
                    chiaSe.MaBaiDangMoiNavigation.DaXoa = true;
                }

                // Xóa record chia sẻ
                _context.ChiaSeBaiDangs.Remove(chiaSe);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DaChiaSeAsync(int maBaiDang, Guid maNguoiDung)
        {
            return await _context.ChiaSeBaiDangs
                .AnyAsync(x => x.MaBaiDangGoc == maBaiDang && x.MaNguoiChiaSe == maNguoiDung);
        }
    }
}
