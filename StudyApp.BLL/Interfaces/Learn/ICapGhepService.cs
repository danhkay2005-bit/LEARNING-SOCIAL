using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface ICapGhepService
    {
        Task<bool> SyncPairsAsync(int maThe, List<CapGhepRequest> requests);
    }
}
