using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public enum LoginResult { Success, InvalidCredentials, UserNotFound }
    public enum RegisterResult { Success, UsernameExists, Fail }
    public enum ResetPasswordResult { Success, EmailNotFound, Fail }

    public interface INguoiDungService
    {
        LoginResult Login(DangNhapRequest request);
        RegisterResult Register(DangKyNguoiDungRequest request);
        ResetPasswordResult ResetPassword(string email, string newPassword);

        string? GetUserNameById(Guid userId);

        // ✅ THÊM: Method lấy thông tin user cơ bản
        NguoiDungInfo? GetUserInfoById(Guid userId);
    }
}
