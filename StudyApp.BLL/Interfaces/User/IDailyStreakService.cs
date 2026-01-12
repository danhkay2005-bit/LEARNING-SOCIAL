using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IDailyStreakService
{
    Task<DiemDanhHangNgayResponse> CheckInDailyAsync(DiemDanhHangNgayRequest request);
}