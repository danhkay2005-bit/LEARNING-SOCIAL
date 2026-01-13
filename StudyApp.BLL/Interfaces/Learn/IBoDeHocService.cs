using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IBoDeHocService
    {
        Task<BoDeHocResponse?> GetByIdAsync(int id);
        Task<IEnumerable<BoDeHocResponse>> GetByUserAsync(Guid userId);
        Task<BoDeHocResponse> CreateAsync(TaoBoDeHocRequest request);
        Task<BoDeHocResponse> CreateFullAsync(LuuToanBoBoDeRequest request);
        Task<BoDeHocResponse> UpdateAsync(int id, CapNhatBoDeHocRequest request);
        Task<bool> DeleteAsync(int id); // Soft delete

        // Logic Sao chép bộ đề
        Task<BoDeHocResponse> ForkAsync(SaoChepBoDeHocRequest request);
    }
}