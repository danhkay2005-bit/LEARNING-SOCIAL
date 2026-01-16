using Microsoft.AspNetCore.SignalR;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Services;

namespace StudyApp.API.Services
{
    class SignalRThachDauNotifier : IThachDauNotifier
    {
        private readonly IHubContext<ThachDauHub> _hubContext;

        public SignalRThachDauNotifier(IHubContext<ThachDauHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyReadyToStart(int maThachDau)
        {
            await _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("ReadyToStart");
        }

        public async Task NotifyUpdateScore(int maThachDau, Guid userId, int score)
        {
            await _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("UpdateOpponentScore", userId, score);
        }

        // Thực thi logic đồng bộ qua câu
        public async Task NotifyOpponentReadyNext(int maThachDau, Guid userId, int questionIndex)
        {
            // Gửi sự kiện "OpponentReadyNext" để WinForms Client nhận biết
            await _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("OpponentReadyNext", userId, questionIndex);
        }
    }
}