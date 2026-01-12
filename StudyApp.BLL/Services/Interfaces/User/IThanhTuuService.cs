using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public interface IThanhTuuService
    {
        // Lấy tất cả thành tựu, đánh dấu cái nào User đã đạt được
        Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);

        // Kiểm tra xem User có đủ điều kiện mở khóa thành tựu nào không
        // Hàm này thường chạy ngầm sau mỗi hành động lớn của User
        Task CheckAndUnlockAchievementAsync(Guid maNguoiDung, string loaiThanhTuu, int giaTriHienTai, CancellationToken cancellationToken = default);
    }
}