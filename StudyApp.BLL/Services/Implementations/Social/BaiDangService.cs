using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using StudyApp.DTO.Responses.NguoiDung;
using System;
using System.Linq;
using System.Text.RegularExpressions;  
using SocialResponses = StudyApp.DTO.Responses.Social;  // ✅ ALIAS để tránh conflict

namespace StudyApp.BLL.Services.Social
{
    public class BaiDangService : IBaiDangService
    {
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;
        private readonly IMentionService _mentionService;
        private readonly INguoiDungService _userService;

        public BaiDangService(
            IMapper mapper,
            SocialDbContext dbContext,
            IMentionService mentionService,
            INguoiDungService userService)
        {
            _mapper = mapper;
            _db = dbContext;
            _mentionService = mentionService;
            _userService = userService;
        }

        // =====================================================
        // ================= FEED ==============================
        // =====================================================
        public DanhSachBaiDangResponse LayDanhSachBaiDang(LayBaiDangRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung;

            var query = _db.Set<BaiDang>()
                .AsNoTracking()
                .Include(x => x.ReactionBaiDangs)
                .Include(x => x.MaHashtags)
                .Where(x => x.DaXoa != true)
                .Where(x =>
                    x.QuyenRiengTu == (byte)QuyenRiengTuEnum.CongKhai ||
                    (userId.HasValue && x.MaNguoiDung == userId.Value));

            if (request.MaNguoiDung.HasValue)
                query = query.Where(x => x.MaNguoiDung == request.MaNguoiDung.Value);

            if (request.LoaiBaiDang.HasValue)
                query = query.Where(x =>
                    x.LoaiBaiDang == request.LoaiBaiDang.Value.ToString());

            var total = query.Count();

            var baiDangs = query
                .OrderByDescending(x => x.GhimBaiDang == true && x.MaNguoiDung == userId)
                .ThenByDescending(x => x.ThoiGianTao)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new DanhSachBaiDangResponse
            {
                BaiDangs = baiDangs
                    .Select(MapUserContext)
                    .ToList(),
                TongSo = total,
                TrangHienTai = request.PageNumber,
                TongSoTrang = (int)Math.Ceiling(total / (double)request.PageSize),
                CoTrangTiep = request.PageNumber * request.PageSize < total,
                CoTrangTruoc = request.PageNumber > 1
            };
        }

        // =====================================================
        // ================= DETAIL ============================
        // =====================================================
        public BaiDangResponse LayChiTietBaiDang(int maBaiDang)
        {
            var entity = _db.Set<BaiDang>()
                .Include(x => x.ReactionBaiDangs)
                .Include(x => x.MaHashtags)
                .FirstOrDefault(x => x.MaBaiDang == maBaiDang && x.DaXoa != true)
                ?? throw new Exception("Bài đăng không tồn tại");

            entity.SoLuotXem = (entity.SoLuotXem ?? 0) + 1;
            _db.SaveChanges();

            return MapUserContext(entity);
        }

        // =====================================================
        // ================= CREATE ============================
        // =====================================================
        public TaoBaiDangResponse TaoBaiDang(TaoBaiDangRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var entity = _mapper.Map<BaiDang>(request);
            entity.MaNguoiDung = UserSession.CurrentUser!.MaNguoiDung;

            XuLyHashtag(entity, request.NoiDung);

            _db.Add(entity);
            _db.SaveChanges();

            // 🔥 Mention
            _mentionService.XuLyMentionBaiDang(entity.MaBaiDang, request.NoiDung);

            return new TaoBaiDangResponse
            {
                ThanhCong = true,
                BaiDang = MapUserContext(entity)
            };
        }

        // =====================================================
        // ================= UPDATE ============================
        // =====================================================
        public void CapNhatBaiDang(CapNhatBaiDangRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var entity = _db.Set<BaiDang>()
                .Include(x => x.MaHashtags)
                .FirstOrDefault(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.MaNguoiDung == userId &&
                    x.DaXoa != true)
                ?? throw new Exception("Không có quyền sửa");

            // Giảm count hashtag cũ
            foreach (var old in entity.MaHashtags)
                old.SoLuotDung = Math.Max(0, (old.SoLuotDung ?? 1) - 1);

            entity.MaHashtags.Clear();

            _mapper.Map(request, entity);

            XuLyHashtag(entity, request.NoiDung);

            _db.SaveChanges();

            // 🔥 Mention
            _mentionService.XuLyMentionBaiDang(entity.MaBaiDang, request.NoiDung);
        }

        // =====================================================
        // ================= DELETE ============================
        // =====================================================
        public void XoaBaiDang(XoaBaiDangRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var entity = _db.Set<BaiDang>()
                .FirstOrDefault(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.MaNguoiDung == userId)
                ?? throw new Exception("Không có quyền xóa");

            entity.DaXoa = true;
            _db.SaveChanges();
        }

        // =====================================================
        // ================= PIN ===============================
        // =====================================================
        public void GhimBaiDang(GhimBaiDangRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var entity = _db.Set<BaiDang>()
                .FirstOrDefault(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.MaNguoiDung == userId)
                ?? throw new Exception("Không có quyền");

            entity.GhimBaiDang = request.GhimBaiDang;
            _db.SaveChanges();
        }

        // =====================================================
        // ================= PRIVATE ===========================
        // =====================================================
        private BaiDangResponse MapUserContext(BaiDang entity)
        {
            var response = _mapper.Map<BaiDangResponse>(entity);

            // Set thông tin người dùng
            response.NguoiDang = GetUserSummaryById(entity.MaNguoiDung);

            if (UserSession.IsLoggedIn)
            {
                var myReaction = entity.ReactionBaiDangs
                    .FirstOrDefault(x =>
                        x.MaNguoiDung == UserSession.CurrentUser!.MaNguoiDung);

                response.DaReaction = myReaction != null;
                response.LoaiReactionCuaToi = myReaction != null
                    ? Enum.Parse<LoaiReactionEnum>(myReaction.LoaiReaction!)
                    : null;
            }

            response.Hashtags = entity.MaHashtags
                .Select(h => _mapper.Map<HashtagResponse>(h))
                .ToList();

            return response;
        }

        private SocialResponses.NguoiDungTomTatResponse? GetUserSummaryById(Guid userId)  // ✅ Sử dụng alias
        {
            try
            {
                // Nếu là current user, lấy từ session
                if (UserSession.CurrentUser?.MaNguoiDung == userId)
                {
                    return new SocialResponses.NguoiDungTomTatResponse  // ✅ Sử dụng alias
                    {
                        MaNguoiDung = userId,
                        TenHienThi = UserSession.CurrentUser.HoVaTen,
                        AnhDaiDien = UserSession.CurrentUser.HinhDaiDien
                    };
                }

                // Gọi user service từ database chính
                var userInfo = _userService.GetUserInfoById(userId);
                if (userInfo != null)
                {
                    return new SocialResponses.NguoiDungTomTatResponse  // ✅ Sử dụng alias
                    {
                        MaNguoiDung = userInfo.MaNguoiDung,
                        TenHienThi = userInfo.HoTen,
                        AnhDaiDien = userInfo.AnhDaiDien
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 🔥 Hashtag CHỈ sinh khi người dùng gõ #
        /// </summary>
        private void XuLyHashtag(BaiDang baiDang, string? noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung)) return;

            var tags = Regex.Matches(noiDung, @"#\w+")
                .Select(m => m.Value[1..].ToLower())
                .Distinct();

            foreach (var ten in tags)
            {
                var hashtag = _db.Set<Hashtag>()
                    .FirstOrDefault(x => x.TenHashtag == ten);

                if (hashtag == null)
                {
                    hashtag = new Hashtag
                    {
                        TenHashtag = ten,
                        SoLuotDung = 1,
                        ThoiGianTao = DateTime.UtcNow
                    };
                    _db.Add(hashtag);
                }
                else
                {
                    hashtag.SoLuotDung = (hashtag.SoLuotDung ?? 0) + 1;
                }

                baiDang.MaHashtags.Add(hashtag);
            }
        }
    }
}