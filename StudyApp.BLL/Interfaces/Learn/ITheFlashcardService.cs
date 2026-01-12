using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface ITheFlashcardService
    {
        Task<TheFlashcardResponse> GetByIdAsync(int id);
        Task<IEnumerable<TheFlashcardResponse>> GetByBoDeAsync(int maBoDe);

        // Tạo thẻ kèm theo các thông tin bổ sung tùy chọn
        Task<TheFlashcardResponse> CreateAsync(TaoTheFlashcardRequest request);

        Task<TheFlashcardResponse> UpdateAsync(CapNhatTheFlashcardRequest request);
        Task<bool> DeleteAsync(int id);
    }
}