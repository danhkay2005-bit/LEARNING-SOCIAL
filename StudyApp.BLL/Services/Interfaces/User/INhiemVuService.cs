using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public interface INhiemVuService
    {
        // Lấy danh sách nhiệm vụ kèm tiến độ của User
        Task<List<NhiemVuResponse>> GetUserQuestsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);

        // Cập nhật tiến độ (Hàm này sẽ được gọi từ các Service khác khi User làm hành động)
        // Ví dụ: Khi học xong 1 bài -> gọi UpdateProgress(user, "HocTap", 1)
        Task UpdateQuestProgressAsync(Guid maNguoiDung, string loaiDieuKien, int giaTriThem, CancellationToken cancellationToken = default);

        // Nhận thưởng khi nhiệm vụ hoàn thành
        Task<bool> ClaimQuestRewardAsync(Guid maNguoiDung, int maNhiemVu, CancellationToken cancellationToken = default);
    }
}