using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IDapAnTracNghiemService
    {
        Task<IEnumerable<DapAnTracNghiemResponse>> GetByMaTheAsync(int maThe);
        Task<bool> SyncAnswersAsync(int maThe, List<TaoDapAnTracNghiemRequest> requests);
    }
}
