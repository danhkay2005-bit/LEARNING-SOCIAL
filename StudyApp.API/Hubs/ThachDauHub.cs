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
            // Cập nhật điểm cho tất cả mọi người trong phòng
            await Clients.Group(roomCode).SendAsync("UpdateOpponentScore", userId, score);
        }

        /// <summary>
        /// Gửi tín hiệu đã trả lời xong câu hỏi hiện tại
        /// </summary>
        public async Task SendReadyNext(string roomCode, Guid userId, int questionIndex)
        {
            // Gửi thông báo cho đối thủ rằng Player này đã xong câu X
            // Phía WinForms sẽ lắng nghe sự kiện "OpponentReadyNext"
            await Clients.OthersInGroup(roomCode).SendAsync("OpponentReadyNext", userId, questionIndex);
        }

        public async Task TriggerStartMatch(string roomCode)
        {
            await Clients.Group(roomCode).SendAsync("ReadyToStart");
        }

        public async Task SendStartSignal(string roomCode)
        {
            await Clients.OthersInGroup(roomCode).SendAsync("StartMatchSignal");
        }

        // Thêm hàm dọn dẹp phòng khi hoàn thành hoặc có người thoát
        public async Task LeaveRoom(string roomCode)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task NotifyOpponentLeft(string maThachDau)
        {
            // Gửi lệnh "OpponentLeft" cho tất cả mọi người trong nhóm (trừ người vừa gửi)
            await Clients.OthersInGroup(maThachDau).SendAsync("OpponentLeft");
        }
    }
}