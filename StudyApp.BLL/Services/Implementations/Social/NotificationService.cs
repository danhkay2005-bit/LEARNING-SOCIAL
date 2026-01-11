using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Implementations.Social
{
    public class NotificationService : INotificationService
    {
        public event Action<ThongBaoDTO>? OnNotify;

        public void Push(ThongBaoDTO thongBao)
        {
            OnNotify?.Invoke(thongBao);
        }
    }
}
