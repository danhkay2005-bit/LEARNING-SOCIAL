using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.User;

public interface IUserProfileService
{
    Task<NguoiDungResponse?> GetProfileAsync(Guid userId);
    Task<bool> UpdateProfileAsync(Guid userId, CapNhatHoSoRequest request);
    Task<bool> ChangePasswordAsync(Guid userId, string oldPass, string newPass);
    Task<List<NguoiDungResponse>> TimKiemNguoiDungAsync(string keyword);
    
    // ✅ Avatar methods (chỉ giữ 1 UpdateAvatarAsync)
    Task<bool> UpdateAvatarAsync(Guid userId, string avatarUrl);
    Task<string?> GetAvatarUrlAsync(Guid userId);
    Task<List<NguoiDungResponse>> GetUsersByIdsAsync(List<Guid> userIds);
    Task<int> GetTotalUsersCountAsync();
}