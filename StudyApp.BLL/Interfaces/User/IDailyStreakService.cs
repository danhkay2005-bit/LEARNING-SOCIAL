using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IDailyStreakService
{
    Task<DiemDanhHangNgayResponse> CheckInDailyAsync(DiemDanhHangNgayRequest request);

    // Gọi khi user hoàn thành 1 bộ đề (chỉ đánh dấu đã học hôm nay)
    Task MarkLessonCompletedTodayAsync(Guid userId);

    // Gọi khi user claim thưởng nhiệm vụ (nếu hôm nay đã học thì mới cộng chuỗi)
    Task TryGrantStreakForTodayAsync(Guid userId);
}