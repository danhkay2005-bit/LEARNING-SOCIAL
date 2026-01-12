using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IAuthService
{
    // Đăng nhập / Đăng ký
    Task<(LoginResult Result, NguoiDungDTO? User)> LoginAsync(DangNhapRequest request);
    Task<RegisterResult> RegisterAsync(DangKyNguoiDungRequest request);

    // --- QUÊN MẬT KHẨU ---
    // Bước 1: Gửi yêu cầu (Kiểm tra email tồn tại)
    Task<ResetPasswordResult> ForgotPasswordAsync(string email);

    // Bước 2: Đặt lại mật khẩu (Cần Token xác thực - Ở đây giả lập)
    Task<ResetPasswordResult> ResetPasswordAsync(DoiMatKhauRequest request);
}