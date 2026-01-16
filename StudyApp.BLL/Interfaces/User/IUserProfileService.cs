using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IUserProfileService
{
    Task<NguoiDungResponse?> GetProfileAsync(Guid userId);
    Task<bool> UpdateProfileAsync(Guid userId, CapNhatHoSoRequest request);

    // Đổi mật khẩu khi VẪN NHỚ mật khẩu cũ
    Task<bool> ChangePasswordAsync(Guid userId, string oldPass, string newPass);
    // ✅ THÊM:  Method tìm kiếm người dùng
    Task<List<NguoiDungResponse>> TimKiemNguoiDungAsync(string keyword);
}