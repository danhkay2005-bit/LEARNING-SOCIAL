using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface ITienDoHocTapService
    {
        // 1. Lấy danh sách các thẻ cần ôn tập hôm nay (Đã đến hạn)
        Task<IEnumerable<TienDoHocTapSummaryResponse>> GetDanhSachCanOnTapAsync(Guid userId, int? maBoDe = null);

        // 2. Khởi tạo tiến độ cho một thẻ (Lần đầu tiên người dùng chạm vào thẻ)
        Task<TienDoHocTapResponse> InitTienDoAsync(TaoTienDoHocTapRequest request);

        // 3. Lấy thống kê trạng thái học tập (Bao nhiêu thẻ đã thuộc, bao nhiêu thẻ mới...)
        Task<object> GetThongKeTienDoAsync(Guid userId, int maBoDe);

        // 4. Reset tiến độ (Học lại từ đầu)
        Task<bool> ResetTienDoAsync(int maTienDo);
    }
}