using Microsoft.AspNetCore.SignalR;

namespace StudyApp.API.Hubs
{
    public class ThachDauHub : Hub
    {
        public async Task JoinRoom(string roomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task SendScore(string roomCode, Guid userId, int score)
        {
            await Clients.Group(roomCode).SendAsync("UpdateOpponentScore", userId, score);
        }

        public async Task TriggerStartMatch(string roomCode)
        {
            // Đổi từ "StartMatch" thành "ReadyToStart" để máy chủ phòng nhận được
            await Clients.Group(roomCode).SendAsync("ReadyToStart");
        }

        public async Task SendStartSignal(string roomCode)
        {
            await Clients.OthersInGroup(roomCode).SendAsync("StartMatchSignal");
        }
    }
}