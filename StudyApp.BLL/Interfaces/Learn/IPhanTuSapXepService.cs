using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IPhanTuSapXepService
    {
        Task<bool> SyncElementsAsync(int maThe, List<TaoPhanTuSapXepRequest> requests);
    }
}
