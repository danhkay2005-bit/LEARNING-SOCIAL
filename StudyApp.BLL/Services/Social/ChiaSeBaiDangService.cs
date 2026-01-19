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
        private readonly UserDbContext _userContext;
        private readonly IMapper _mapper;
        private readonly IThongBaoService _thongBaoService; // ✅ Thêm
       

        public ChiaSeBaiDangService(SocialDbContext context, UserDbContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<ChiaSeBaiDangResponse> ChiaSeBaiDangAsync(ChiaSeBaiDangRequest request)
        {
            // Kiểm tra bài đăng gốc có tồn tại không
            var baiDangGoc = await _context.BaiDangs
                .FirstOrDefaultAsync(bd => bd.MaBaiDang == request.MaBaiDangGoc && bd.DaXoa != true);

            if (baiDangGoc == null)
            {
                throw new Exception("Bài đăng không tồn tại hoặc đã bị xóa");
            }

            // Kiểm tra quyền xem bài đăng gốc
            if (baiDangGoc.QuyenRiengTu == (byte)QuyenRiengTuEnum.RiengTu 
                && baiDangGoc.MaNguoiDung != request.MaNguoiChiaSe)
            {
                throw new Exception("Bạn không có quyền chia sẻ bài đăng này");
            }

            // Kiểm tra đã chia sẻ bài đăng này chưa
            var daChiaSe = await _context.ChiaSeBaiDangs
                .AnyAsync(cs => cs.MaBaiDangGoc == request.MaBaiDangGoc 
                               && cs.MaNguoiChiaSe == request.MaNguoiChiaSe);

            if (daChiaSe)
            {
                throw new Exception("Bạn đã chia sẻ bài đăng này rồi");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Tạo bài đăng mới với nội dung chia sẻ
                var baiDangMoi = new BaiDang
                {
                    MaNguoiDung = request.MaNguoiChiaSe,
                    NoiDung = request.NoiDungThem,
                    LoaiBaiDang = LoaiBaiDangEnum.ChiaSeKhoaHoc.ToString(),
                    QuyenRiengTu = (byte)request.QuyenRiengTu,
                    SoReaction = 0,
                    SoBinhLuan = 0,
                    GhimBaiDang = false,
                    TatBinhLuan = false,
                    DaChinhSua = false,
                    DaXoa = false,
                    ThoiGianTao = DateTime.Now
                };

                await _context.BaiDangs.AddAsync(baiDangMoi);
                await _context.SaveChangesAsync();

                // Tạo bản ghi chia sẻ
                var chiaSeMoi = new ChiaSeBaiDang
                {
                    MaBaiDangGoc = request.MaBaiDangGoc,
                    MaNguoiChiaSe = request.MaNguoiChiaSe,
                    NoiDungThem = request.NoiDungThem,
                    MaBaiDangMoi = baiDangMoi.MaBaiDang,
                    ThoiGian = DateTime.Now
                };

                await _context.ChiaSeBaiDangs.AddAsync(chiaSeMoi);
                await _context.SaveChangesAsync();

                // Load lại dữ liệu để lấy navigation properties
                var chiaSeDaLuu = await _context.ChiaSeBaiDangs
                    .Include(cs => cs.MaBaiDangGocNavigation)
                    .Include(cs => cs.MaBaiDangMoiNavigation)
                    .FirstOrDefaultAsync(cs => cs.MaChiaSe == chiaSeMoi.MaChiaSe);

                await transaction.CommitAsync();

                return _mapper.Map<ChiaSeBaiDangResponse>(chiaSeDaLuu);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<ChiaSeBaiDangResponse>> LayDanhSachChiaSeTheoMaBaiDangAsync(int maBaiDang)
        {
            var danhSachChiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangGocNavigation)
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .Where(cs => cs.MaBaiDangGoc == maBaiDang)
                .OrderByDescending(cs => cs.ThoiGian)
                .ToListAsync();

            return _mapper.Map<List<ChiaSeBaiDangResponse>>(danhSachChiaSe);
        }

        public async Task<ThongKeChiaSeBaiDangResponse> LayThongKeChiaSeAsync(int maBaiDang)
        {
            var tongSoChiaSe = await _context.ChiaSeBaiDangs
                .CountAsync(cs => cs.MaBaiDangGoc == maBaiDang);

            var lanChiaSeMoiNhat = await _context.ChiaSeBaiDangs
                .Where(cs => cs.MaBaiDangGoc == maBaiDang)
                .OrderByDescending(cs => cs.ThoiGian)
                .Select(cs => cs.ThoiGian)
                .FirstOrDefaultAsync();

            return new ThongKeChiaSeBaiDangResponse
            {
                MaBaiDang = maBaiDang,
                TongSoChiaSe = tongSoChiaSe,
                LanChiaSeMoiNhat = lanChiaSeMoiNhat
            };
        }

        public async Task<ChiaSeBaiDangResponse?> LayChiTietChiaSeAsync(int maChiaSe)
        {
            var chiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangGocNavigation)
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .FirstOrDefaultAsync(cs => cs.MaChiaSe == maChiaSe);

            return chiaSe != null ? _mapper.Map<ChiaSeBaiDangResponse>(chiaSe) : null;
        }

        public async Task<bool> HuyChiaSeAsync(int maChiaSe, Guid maNguoiDung)
        {
            var chiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .FirstOrDefaultAsync(cs => cs.MaChiaSe == maChiaSe && cs.MaNguoiChiaSe == maNguoiDung);

            if (chiaSe == null)
            {
                return false;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Xóa bài đăng mới (nếu có)
                if (chiaSe.MaBaiDangMoiNavigation != null)
                {
                    chiaSe.MaBaiDangMoiNavigation.DaXoa = true;
                    _context.BaiDangs.Update(chiaSe.MaBaiDangMoiNavigation);
                }

                // Xóa bản ghi chia sẻ
                _context.ChiaSeBaiDangs.Remove(chiaSe);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<ChiaSeBaiDangResponse>> LayDanhSachChiaSeTheoNguoiDungAsync(Guid maNguoiDung, int pageIndex = 1, int pageSize = 10)
        {
            var danhSachChiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangGocNavigation)
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .Where(cs => cs.MaNguoiChiaSe == maNguoiDung)
                .OrderByDescending(cs => cs.ThoiGian)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<ChiaSeBaiDangResponse>>(danhSachChiaSe);
        }

        public async Task<bool> KiemTraDaChiaSeAsync(int maBaiDang, Guid maNguoiDung)
        {
            return await _context.ChiaSeBaiDangs
                .AnyAsync(cs => cs.MaBaiDangGoc == maBaiDang && cs.MaNguoiChiaSe == maNguoiDung);
        }

        public async Task<List<ChiaSeBaiDangResponse>> LayDanhSachBaiDangDaChiaSeAsync(Guid maNguoiDung, int skip, int take)
        {
            var danhSachChiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangGocNavigation)
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .Where(cs => cs.MaNguoiChiaSe == maNguoiDung)
                .OrderByDescending(cs => cs.ThoiGian)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<List<ChiaSeBaiDangResponse>>(danhSachChiaSe);
        }

        public async Task<ChiaSeBaiDangResponse?> LayChiTietChiaSeTheoBaiDangMoiAsync(int maBaiDangMoi)
        {
            var chiaSe = await _context.ChiaSeBaiDangs
                .Include(cs => cs.MaBaiDangGocNavigation)
                .Include(cs => cs.MaBaiDangMoiNavigation)
                .FirstOrDefaultAsync(cs => cs.MaBaiDangMoi == maBaiDangMoi);

            if (chiaSe == null) return null;

            var result = _mapper.Map<ChiaSeBaiDangResponse>(chiaSe);

            // ✅ FIX: Load thông tin người CHIA SẺ (BaiDangMoi)
            if (result.BaiDangMoi != null)
            {
                var nguoiChiaSe = await _userContext.NguoiDungs
                    .Where(u => u.MaNguoiDung == result.BaiDangMoi.MaNguoiDung)
                    .Select(u => new
                    {
                        u.HoVaTen,
                        u.TenDangNhap,
                        u.HinhDaiDien
                    })
                    .FirstOrDefaultAsync();

                if (nguoiChiaSe != null)
                {
                    result.BaiDangMoi.TenNguoiDung = nguoiChiaSe.HoVaTen ?? nguoiChiaSe.TenDangNhap;
                    result.BaiDangMoi.HinhDaiDien = nguoiChiaSe.HinhDaiDien;
                }
            }

            // ✅ Load thông tin người đăng gốc từ UserDbContext
            if (result.BaiDangGoc != null)
            {
                var nguoiDungGoc = await _userContext.NguoiDungs
                    .Where(u => u.MaNguoiDung == result.BaiDangGoc.MaNguoiDung)
                    .Select(u => new
                    {
                        u.HoVaTen,
                        u.TenDangNhap,
                        u.HinhDaiDien
                    })
                    .FirstOrDefaultAsync();

                if (nguoiDungGoc != null)
                {
                    result.BaiDangGoc.TenNguoiDung = nguoiDungGoc.HoVaTen ?? nguoiDungGoc.TenDangNhap;
                    result.BaiDangGoc.HinhDaiDien = nguoiDungGoc.HinhDaiDien;
                }
            }

            return result;
        }
    }
}