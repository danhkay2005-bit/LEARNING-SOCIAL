using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WinForms.Services
{
    public interface INotificationService
    {
        Task<List<ThongBaoResponse>> GetUserNotificationsAsync(DanhSachThongBaoRequest request);
        Task<int> GetUnreadCountAsync(Guid maNguoiDung);
        Task<bool> MarkAsReadAsync(int maThongBao);
        Task<bool> MarkAllAsReadAsync(Guid maNguoiDung);
    }
}