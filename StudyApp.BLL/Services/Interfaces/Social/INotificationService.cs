using StudyApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.Social
{
    public interface INotificationService
    {
        event Action<ThongBaoDTO> OnNotify;

        void Push(ThongBaoDTO thongBao);
    }
}
