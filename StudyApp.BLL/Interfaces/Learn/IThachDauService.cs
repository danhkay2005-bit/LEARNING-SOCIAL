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
        /// Tạo phòng đấu và ghi nhận Chủ phòng vào bảng LichSuThachDau với Diem = null.
        /// </summary>
        Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request);

        /// <summary>
        /// Chuyển trạng thái bảng ThachDau sang "DangDau".
        /// </summary>
        Task<bool> BatDauThachDauAsync(int maThachDau);

        /// <summary>
        /// Xác định thắng thua, cập nhật bảng NguoiDung và XÓA bản ghi tại bảng ThachDau.
        /// </summary>
        Task<bool> HoanThanhVaCleanupAsync(int maThachDau);

        /// <summary>
        /// Xóa phòng tại bảng ThachDau và dọn dẹp các bản ghi chưa có điểm trong LichSuThachDau.
        /// </summary>
        Task<bool> HuyThachDauAsync(int maThachDau);


        // ==========================================
        // QUẢN LÝ NGƯỜI CHƠI (Ghi trực tiếp vào Lịch sử)
        // ==========================================

        /// <summary>
        /// Ghi nhận Đối thủ tham gia vào bảng LichSuThachDau (Diem = null).
        /// </summary>
        /// <param name="request">Sử dụng DTO LichSuThachDauRequest mới</param>
        Task<bool> ThamGiaThachDauAsync(LichSuThachDauRequest request);

        /// <summary>
        /// Cập nhật kết quả cuối cùng vào dòng tương ứng trong bảng LichSuThachDau.
        /// </summary>
        Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request, List<ChiTietTraLoiRequest> chiTiets);


        // ==========================================
        // TRUY VẤN DỮ LIỆU
        // ==========================================

        /// <summary>
        /// Lấy thông tin phòng từ bảng ThachDau.
        /// </summary>
        Task<ThachDauResponse?> GetByIdAsync(int maThachDau);

        /// <summary>
        /// Truy vấn danh sách người chơi từ bảng LichSuThachDau dựa trên mã PIN (MaThachDauGoc).
        /// </summary>
        Task<IEnumerable<ThachDauNguoiChoiResponse>> GetBangXepHangAsync(int maThachDau);

        /// <summary>
        /// Đồng bộ trạng thái câu hỏi qua SignalR.
        /// </summary>
        Task<bool> BaoCaoReadyNextAsync(int maThachDau, Guid userId, int questionIndex);
    }
}