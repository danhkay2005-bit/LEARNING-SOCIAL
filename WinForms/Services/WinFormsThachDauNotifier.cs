using StudyApp.BLL.Interfaces.Learn;
using System;
using System.Threading.Tasks;

namespace WinForms.Services
{
    // Lớp này đóng vai trò là "bản rỗng" để WinForms có thể khởi tạo ThachDauService
    // mà không cần đến IHubContext của Server.
    public class WinFormsThachDauNotifier : IThachDauNotifier
    {
        public Task NotifyReadyToStart(int maThachDau)
        {
            // WinForms không cần làm gì ở đây vì nó lắng nghe qua HubConnection ở UI
            return Task.CompletedTask;
        }

        public Task NotifyUpdateScore(int maThachDau, Guid userId, int score)
        {
            // WinForms không cần làm gì ở đây
            return Task.CompletedTask;
        }

        public Task NotifyOpponentReadyNext(int maThachDau, Guid userId, int questionIndex)
        {
            // WinForms không cần làm gì ở đây
            return Task.CompletedTask;
        }
    }
}