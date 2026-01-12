using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public interface INguoiDungService
    {
        LoginResult Login(DangNhapRequest request);
        RegisterResult Register(DangKyNguoiDungRequest request);
        ResetPasswordResult ResetPassword(string email, string newPassword);

        Task<string?> GetTieuSuAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task<bool> UpdateTieuSuAsync(Guid maNguoiDung, string? tieuSu, CancellationToken cancellationToken = default);

        Task<string?> GetAvatarPathAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task<bool> UpdateAvatarPathAsync(Guid maNguoiDung, string? avatarPath, CancellationToken cancellationToken = default);

        Task<UserStatsResponses?> GetUserStatsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task<bool> AddXPAsync(Guid maNguoiDung, int amount, CancellationToken cancellationToken = default);
    }
}