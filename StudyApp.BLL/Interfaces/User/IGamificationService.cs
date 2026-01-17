using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IGamificationService
{
    /// <summary>
    /// Thêm XP cho người dùng
    /// </summary>
    Task AddXpAsync(Guid userId, int xpAmount);

    /// <summary>
    /// Lấy thông tin thống kê của người dùng
    /// </summary>
    Task<UserStatsResponses> GetUserStatsAsync(Guid userId);

    /// <summary>
    /// Lấy danh sách nhiệm vụ của người dùng
    /// </summary>
    Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId);

    /// <summary>
    /// Xử lý hoàn thành bài học
    /// </summary>
    Task ProcessLessonCompletionAsync(Guid userId, int xpEarned);

    /// <summary>
    /// Xử lý thời gian online
    /// </summary>
    Task ProcessOnlineTimeAsync(Guid userId, int minutes);

    /// <summary>
    /// Cập nhật tiến độ nhiệm vụ
    /// </summary>
    Task<bool> UpdateQuestProgressAsync(Guid userId, string loaiDieuKien, int giaTriThem);

    /// <summary>
    /// Nhận thưởng nhiệm vụ
    /// </summary>
    Task<string> ClaimQuestRewardAsync(Guid userId, int maNhiemVu);

    /// <summary>
    /// Reset đếm nhiệm vụ hàng ngày
    /// </summary>
    Task ResetDailyQuestCountAsync(Guid userId);

    /// <summary>
    /// Lấy danh sách thành tựu
    /// </summary>
    Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid userId);

    /// <summary>
    /// Kiểm tra và cấp thành tựu
    /// </summary>
    Task CheckAndGrantAchievementsAsync(Guid userId);

    /// <summary>
    /// Hồi sinh chuỗi ngày học liên tiếp
    /// </summary>
    Task<string> RestoreStreakAsync(Guid userId);  // ✅ THÊM DÒNG NÀY
}

