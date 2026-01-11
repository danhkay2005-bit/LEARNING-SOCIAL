using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.BLL.Services.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StudyApp.BLL.Services.Implementations.Social
{
    public class MentionService : IMentionService
    {
        private readonly SocialDbContext _db;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public MentionService(
            SocialDbContext dbContext,
            IMapper mapper,
            INotificationService notificationService)
        {
            _db = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        // =====================================================
        // ================= PARSE & CREATE ===================
        // =====================================================
        public void XuLyMentionBaiDang(int maBaiDang, string? noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung)) return;

            var users = TimNguoiDungDuocMention(noiDung);

            foreach (var userId in users)
            {
                _db.Add(new MentionBaiDang
                {
                    MaBaiDang = maBaiDang,
                    MaNguoiDuocMention = userId,
                    ThoiGian = DateTime.UtcNow
                });

                _notificationService.Push(new ThongBaoDTO
                {
                    Loai = LoaiThongBao.MentionBaiDang,
                    NoiDung = "Bạn được nhắc trong một bài đăng",
                    ThoiGian = DateTime.UtcNow,
                    MaBaiDang = maBaiDang,
                    MaNguoiGui = UserSession.CurrentUser!.MaNguoiDung
                });
            }

            _db.SaveChanges();
        }

        public void XuLyMentionBinhLuan(int maBinhLuan, string? noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung))
                return;

            var currentUserId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            // 1️⃣ Parse danh sách user được mention (distinct)
            var mentionedUsers = TimNguoiDungDuocMention(noiDung)
                .Where(u => u != currentUserId)
                .Distinct()
                .ToList();

            if (!mentionedUsers.Any())
                return;

            // 2️⃣ Tạo mention trong DB
            foreach (var userId in mentionedUsers)
            {
                _db.Add(new MentionBinhLuan
                {
                    MaBinhLuan = maBinhLuan,
                    MaNguoiDuocMention = userId,
                    ThoiGian = DateTime.UtcNow
                });
            }

            // 3️⃣ Commit DB TRƯỚC
            _db.SaveChanges();

            // 4️⃣ Push notification SAU commit
            foreach (var userId in mentionedUsers)
            {
                _notificationService.Push(new ThongBaoDTO
                {
                    Loai = LoaiThongBao.MentionBinhLuan,
                    NoiDung = "Bạn được nhắc trong một bình luận",
                    ThoiGian = DateTime.UtcNow,
                    MaBinhLuan = maBinhLuan,
                    MaNguoiGui = currentUserId
                });
            }
        }


        // =====================================================
        // ================= GET MENTION ======================
        // =====================================================
        public DanhSachMentionResponse LayDanhSachMention(LayMentionRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var baiDangMentions = _db.Set<MentionBaiDang>()
                .AsNoTracking()
                .Where(x => x.MaNguoiDuocMention == userId)
                .OrderByDescending(x => x.ThoiGian)
                .ToList();

            var binhLuanMentions = _db.Set<MentionBinhLuan>()
                .AsNoTracking()
                .Where(x => x.MaNguoiDuocMention == userId)
                .OrderByDescending(x => x.ThoiGian)
                .ToList();

            return new DanhSachMentionResponse
            {
                MentionBaiDangs = baiDangMentions
                    .Select(x => _mapper.Map<MentionBaiDangResponse>(x))
                    .ToList(),

                MentionBinhLuans = binhLuanMentions
                    .Select(x => _mapper.Map<MentionBinhLuanResponse>(x))
                    .ToList(),

                TongSo = baiDangMentions.Count + binhLuanMentions.Count,
                SoChuaDoc = baiDangMentions.Count + binhLuanMentions.Count
            };
        }

        public void DanhDauDaDoc(DanhDauMentionDaDocRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            if (request.MaBaiDang.HasValue)
            {
                var mentions = _db.Set<MentionBaiDang>()
                    .Where(x =>
                        x.MaBaiDang == request.MaBaiDang &&
                        x.MaNguoiDuocMention == userId);

                foreach (var m in mentions)
                    _db.Entry(m).Property("DaDoc").CurrentValue = true;
            }

            if (request.MaBinhLuan.HasValue)
            {
                var mentions = _db.Set<MentionBinhLuan>()
                    .Where(x =>
                        x.MaBinhLuan == request.MaBinhLuan &&
                        x.MaNguoiDuocMention == userId);

                foreach (var m in mentions)
                    _db.Entry(m).Property("DaDoc").CurrentValue = true;
            }

            _db.SaveChanges();
        }

        // =====================================================
        // ================= PRIVATE ===========================
        // =====================================================
        private List<Guid> TimNguoiDungDuocMention(string noiDung)
        {
            // @username
            var matches = Regex.Matches(noiDung, @"@([\w\.]+)");

            if (matches.Count == 0)
                return [];

            var usernames = matches
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();

            // giả định có bảng NguoiDung
            return _db.Set<NguoiDung>()
                .Where(u => usernames.Contains(u.TenDangNhap))
                .Select(u => u.MaNguoiDung)
                .ToList();
        }
    }
}
