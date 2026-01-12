using StudyApp.DTO.Responses.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public interface IGamificationService
    {
        Task<DiemDanhHangNgayResponse?> DiemDanhHangNgayAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task<bool> CheckDiemDanhTodayAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task ProcessLoginStreakAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
    }
}