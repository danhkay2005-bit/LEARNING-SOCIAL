using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces;
using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;

namespace StudyApp.BLL.Services.Implementations.Social
{
    public class ReactionService : IReactionService
    {
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;
        private readonly INotificationService _notification;

        public ReactionService(
            IMapper mapper,
            SocialDbContext dbContext,
            INotificationService notificationService
        )
        {
            _mapper = mapper;
            _db = dbContext;
            _notification = notificationService;
        }

        // =====================================================
        // ================= REACTION BÀI ĐĂNG =================
        // =====================================================
        public KetQuaReactionResponse ReactionBaiDang(ReactionBaiDangRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var currentUserId = UserSession.CurrentUser!.MaNguoiDung;

            var baiDang = _db.Set<BaiDang>()
                .Include(x => x.ReactionBaiDangs)
                .FirstOrDefault(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.DaXoa != true)
                ?? throw new Exception("Bài đăng không tồn tại");

            var existed = baiDang.ReactionBaiDangs
                .FirstOrDefault(x => x.MaNguoiDung == currentUserId);

            var isNewReaction = false;

            if (existed != null)
            {
                // đổi loại reaction
                existed.LoaiReaction = request.LoaiReaction.ToString();
                existed.ThoiGian = DateTime.UtcNow;
            }
            else
            {
                var entity = _mapper.Map<ReactionBaiDang>(request);
                entity.MaNguoiDung = currentUserId;

                _db.Add(entity);

                baiDang.SoReaction = (baiDang.SoReaction ?? 0) + 1;
                isNewReaction = true;
            }

            _db.SaveChanges();

            // 🔔 Notification runtime (KHÔNG DB)
            if (isNewReaction && baiDang.MaNguoiDung != currentUserId)
            {
                _notification.Push(new ThongBaoDTO
                {
                    Loai = LoaiThongBao.ReactionBaiDang,
                    NoiDung = "Bài viết của bạn vừa có reaction",
                    ThoiGian = DateTime.UtcNow,
                    MaBaiDang = baiDang.MaBaiDang,
                    MaNguoiGui = currentUserId
                });
            }

            return new KetQuaReactionResponse
            {
                ThanhCong = true,
                SoReactionMoi = baiDang.SoReaction ?? 0
            };
        }

        public KetQuaReactionResponse XoaReactionBaiDang(XoaReactionBaiDangRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var currentUserId = UserSession.CurrentUser!.MaNguoiDung;

            var reaction = _db.Set<ReactionBaiDang>()
                .FirstOrDefault(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.MaNguoiDung == currentUserId)
                ?? throw new Exception("Reaction không tồn tại");

            _db.Remove(reaction);

            var baiDang = _db.Set<BaiDang>()
                .First(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.DaXoa != true);

            baiDang.SoReaction = Math.Max(0, (baiDang.SoReaction ?? 1) - 1);

            _db.SaveChanges();

            return new KetQuaReactionResponse
            {
                ThanhCong = true,
                SoReactionMoi = baiDang.SoReaction ?? 0
            };
        }

        // =====================================================
        // ================= THÍCH BÌNH LUẬN ===================
        // =====================================================
        public KetQuaThichBinhLuanResponse ThichBinhLuan(ThichBinhLuanRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var currentUserId = UserSession.CurrentUser!.MaNguoiDung;

            var binhLuan = _db.Set<BinhLuanBaiDang>()
                .Include(x => x.ThichBinhLuans)
                .FirstOrDefault(x =>
                    x.MaBinhLuan == request.MaBinhLuan &&
                    x.DaXoa != true)
                ?? throw new Exception("Bình luận không tồn tại");

            var existed = binhLuan.ThichBinhLuans
                .FirstOrDefault(x => x.MaNguoiDung == currentUserId);

            if (existed != null)
            {
                return new KetQuaThichBinhLuanResponse
                {
                    ThanhCong = true,
                    SoLuotThichMoi = binhLuan.SoLuotThich ?? 0,
                    DaThich = true
                };
            }

            var entity = _mapper.Map<ThichBinhLuan>(request);
            entity.MaNguoiDung = currentUserId;

            _db.Add(entity);

            binhLuan.SoLuotThich = (binhLuan.SoLuotThich ?? 0) + 1;

            _db.SaveChanges();

            // 🔔 Notification runtime
            if (binhLuan.MaNguoiDung != currentUserId)
            {
                _notification.Push(new ThongBaoDTO
                {
                    Loai = LoaiThongBao.ThichBinhLuan,
                    NoiDung = "Bình luận của bạn vừa được thích",
                    ThoiGian = DateTime.UtcNow,
                    MaBinhLuan = binhLuan.MaBinhLuan,
                    MaNguoiGui = currentUserId
                });
            }

            return new KetQuaThichBinhLuanResponse
            {
                ThanhCong = true,
                SoLuotThichMoi = binhLuan.SoLuotThich ?? 0,
                DaThich = true
            };
        }

        public KetQuaThichBinhLuanResponse BoThichBinhLuan(BoThichBinhLuanRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var currentUserId = UserSession.CurrentUser!.MaNguoiDung;

            var entity = _db.Set<ThichBinhLuan>()
                .FirstOrDefault(x =>
                    x.MaBinhLuan == request.MaBinhLuan &&
                    x.MaNguoiDung == currentUserId)
                ?? throw new Exception("Chưa thích bình luận");

            _db.Remove(entity);

            var binhLuan = _db.Set<BinhLuanBaiDang>()
                .First(x =>
                    x.MaBinhLuan == request.MaBinhLuan &&
                    x.DaXoa != true);

            binhLuan.SoLuotThich = Math.Max(0, (binhLuan.SoLuotThich ?? 1) - 1);

            _db.SaveChanges();

            return new KetQuaThichBinhLuanResponse
            {
                ThanhCong = true,
                SoLuotThichMoi = binhLuan.SoLuotThich ?? 0,
                DaThich = false
            };
        }
    }
}
