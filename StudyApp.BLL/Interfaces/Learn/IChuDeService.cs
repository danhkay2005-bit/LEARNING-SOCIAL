using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IChuDeService
    {
        Task<IEnumerable<ChuDeResponse>> GetAllAsync();
        Task<ChuDeResponse?> GetByIdAsync(int id);
        Task<ChuDeResponse> CreateAsync(TaoChuDeRequest request);
        Task<ChuDeResponse> UpdateAsync(CapNhatChuDeRequest request);
        Task<bool> DeleteAsync(int id);
    }
}