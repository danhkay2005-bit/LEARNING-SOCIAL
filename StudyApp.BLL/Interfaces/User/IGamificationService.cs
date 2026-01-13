using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IGamificationService
{
    Task<UserStatsResponses> GetUserStatsAsync(Guid userId);
    Task AddXpAsync(Guid userId, int xpAmount);

    // Nhiệm vụ & Thành tựu
    Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId);
    Task<bool> UpdateQuestProgressAsync(Guid userId, string loaiDieuKien, int giaTriThem);
    Task<string> ClaimQuestRewardAsync(Guid userId, int maNhiemVu);
    Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid userId);
}