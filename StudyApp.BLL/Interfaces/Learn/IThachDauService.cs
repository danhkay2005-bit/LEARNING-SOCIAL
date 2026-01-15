using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IThachDauService
    {
        // ==========================================
        // QUẢN LÝ PHÒNG (ROOM)
        // ==========================================

        /// <summary>
        /// Tạo phòng mới với mã PIN 6 số ngẫu nhiên và gán chủ phòng (User A).
        /// </summary>
        Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request);

        /// <summary>
        /// Khóa phòng, chuyển trạng thái sang "DangDau" khi đủ người.
        /// </summary>
        Task<bool> BatDauThachDauAsync(int maThachDau);

        /// <summary>
        /// Kết thúc trận đấu: Tính toán hạng, lưu vào Lịch sử và XÓA dữ liệu tại bảng Thách Đấu tạm.
        /// </summary>
        Task<bool> HoanThanhVaCleanupAsync(int maThachDau);

        /// <summary>
        /// Hủy phòng đấu khi chủ phòng thoát hoặc không có người tham gia.
        /// </summary>
        Task<bool> HuyThachDauAsync(int maThachDau);


        // ==========================================
        // QUẢN LÝ NGƯỜI CHƠI
        // ==========================================

        /// <summary>
        /// Đối thủ (User B) nhập mã PIN để tham gia phòng.
        /// </summary>
        Task<bool> ThamGiaThachDauAsync(ThamGiaThachDauRequest request);

        /// <summary>
        /// Cập nhật điểm số và thời gian làm bài của từng người chơi.
        /// </summary>
        Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request);


        // ==========================================
        // TRUY VẤN DỮ LIỆU
        // ==========================================

        /// <summary>
        /// Kiểm tra trạng thái phòng (đang chờ, đang đấu) để đồng bộ UI.
        /// </summary>
        Task<ThachDauResponse?> GetByIdAsync(int maThachDau);

        /// <summary>
        /// Lấy bảng điểm hiện tại của các thành viên trong phòng.
        /// </summary>
        Task<IEnumerable<ThachDauNguoiChoiResponse>> GetBangXepHangAsync(int maThachDau);
    }
}