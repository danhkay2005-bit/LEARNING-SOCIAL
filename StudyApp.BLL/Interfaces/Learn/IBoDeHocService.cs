using StudyApp.DAL.Entities.Learn;
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
        Task<BoDeHocResponse> UpdateFullAsync(int id, LuuToanBoBoDeRequest request);
        Task<bool> DeleteAsync(int id); // Soft delete

        // Logic Sao chép bộ đề
        Task<BoDeHocResponse> ForkAsync(SaoChepBoDeHocRequest request);

        // Lấy bộ đề công khai ngẫu nhiên để gợi ý
        Task<IEnumerable<BoDeHocResponse>> GetPublicRandomAsync(int count);

        // Lấy bộ đề theo chủ đề cụ thể
        Task<IEnumerable<BoDeHocResponse>> GetByTopicAsync(int topicId);

        // Lấy danh sách các chủ đề phổ biến (Để hiện tiêu đề các dòng)
        Task<IEnumerable<dynamic>> GetPopularTopicsAsync();

        // Lấy đầy đủ dữ liệu bộ đề (Header + List câu hỏi + Đáp án View)
        Task<HocBoDeResponse> GetFullDataToLearnAsync(int boDeId);

        // Chế độ Thách đấu
        Task<ThachDauResponse> CreateChallengeAsync(TaoThachDauRequest request);
        Task<bool> JoinChallengeAsync(LichSuThachDauRequest request);
        Task<bool> UpdateChallengeResultAsync(CapNhatKetQuaThachDauRequest request);

        Task<bool> UpdateCardProgressAsync(CapNhatTienDoHocTapRequest request);
        Task LuuKetQuaPhienHocAsync(PhienHoc phienHoc, List<ChiTietTraLoiRequest> chiTiets);
        Task<IEnumerable<BoDeHocResponse>> GetByFilterAsync(int maChuDe);
        Task TangSoLuotHocAsync(int maBoDe);

    }
}