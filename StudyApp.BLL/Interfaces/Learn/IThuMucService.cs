using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IThuMucService
    {
        Task<IEnumerable<ThuMucResponse>> GetByUserAsync(Guid userId);
        Task<IEnumerable<ThuMucTreeResponse>> GetTreeByUserAsync(Guid userId);
        Task<ThuMucResponse> CreateAsync(TaoThuMucRequest request);
        Task<ThuMucResponse> UpdateAsync(CapNhatThuMucRequest request);
        Task<bool> DeleteAsync(int id);
    }
}