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
    public class NotificationService : INotificationService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public NotificationService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ThongBaoResponse>> GetUserNotificationsAsync(DanhSachThongBaoRequest request)
        {
            var query = _context.Set<ThongBao>()
                .Where(x => x.MaNguoiNhan == request.MaNguoiNhan);

            // Lọc theo trạng thái đọc
            if (request.ChiChuaDoc.HasValue && request.ChiChuaDoc.Value)
            {
                query = query.Where(x => !x.DaDoc);
            }

            var notifications = await query
                .OrderByDescending(x => x.ThoiGian)
                .Skip((request.Trang - 1) * request.KichThuocTrang)
                .Take(request.KichThuocTrang)
                .ToListAsync();

            return _mapper.Map<List<ThongBaoResponse>>(notifications);
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Set<ThongBao>().FindAsync(notificationId);
            if (notification == null)
                return false;

            notification.DaDoc = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid userId)
        {
            var unreadNotifications = await _context.Set<ThongBao>()
                .Where(x => x.MaNguoiNhan == userId && !x.DaDoc)
                .ToListAsync();

            foreach (var notif in unreadNotifications)
            {
                notif.DaDoc = true;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _context.Set<ThongBao>()
                .Where(x => x.MaNguoiNhan == userId && !x.DaDoc)
                .CountAsync();
        }
    }
}