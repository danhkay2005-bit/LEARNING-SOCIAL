using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IAuthService
{
    Task<(LoginResult Result, NguoiDungDTO? User)> LoginAsync(DangNhapRequest request);
    Task<RegisterResult> RegisterAsync(DangKyNguoiDungRequest request);

    Task<ResetPasswordResult> ResetPasswordAsync(string email, string newPassword);

    Task<string?> GetTieuSuAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
    Task<bool> UpdateTieuSuAsync(Guid maNguoiDung, string? tieuSu, CancellationToken cancellationToken = default);
    Task<string?> GetAvatarPathAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
    Task<bool> UpdateAvatarPathAsync(Guid maNguoiDung, string? avatarPath, CancellationToken cancellationToken = default);
}