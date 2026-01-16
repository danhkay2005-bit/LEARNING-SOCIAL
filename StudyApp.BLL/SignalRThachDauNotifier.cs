using Microsoft.AspNetCore.SignalR;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL
{
     class SignalRThachDauNotifier : IThachDauNotifier
    {
        private readonly IHubContext<ThachDauHub> _hubContext;



        public SignalRThachDauNotifier(IHubContext<ThachDauHub> hubContext)

        {

            _hubContext = hubContext;

        }

        public Task NotifyOpponentReadyNext(int maThachDau, Guid userId, int questionIndex)
        {
            return _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("OpponentReadyNext", userId, questionIndex);
        }

        public async Task NotifyReadyToStart(int maThachDau)

        {

            await _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("ReadyToStart");

        }



        public async Task NotifyUpdateScore(int maThachDau, Guid userId, int score)

        {

            await _hubContext.Clients.Group(maThachDau.ToString()).SendAsync("UpdateOpponentScore", userId, score);

        }
    }
}
