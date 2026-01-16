using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    // Trong dự án StudyApp.BLL hoặc một dự án chung (Common/Interfaces)
    public interface IThachDauNotifier
    {
        // Phải có 2 dòng này thì ThachDauService mới không báo lỗi
        Task NotifyReadyToStart(int maThachDau);
        Task NotifyUpdateScore(int maThachDau, Guid userId, int score);
        Task NotifyOpponentReadyNext(int maThachDau, Guid userId, int questionIndex);
    }
}
