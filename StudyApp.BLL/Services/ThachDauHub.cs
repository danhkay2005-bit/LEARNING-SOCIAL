using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;


namespace StudyApp.BLL.Services
{
    // Tại dự án Web API hoặc Backend
    public class ThachDauHub : Hub
    {
        // Người chơi tham gia vào "Nhóm" dựa trên mã PIN (Room)
        public async Task JoinRoom(int maThachDau)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, maThachDau.ToString());
        }

        // Thông báo cho cả phòng rằng đã đủ người
        public async Task NotifyRoomReady(int maThachDau)
        {
            await Clients.Group(maThachDau.ToString()).SendAsync("ReadyToStart");
        }
    }
}
