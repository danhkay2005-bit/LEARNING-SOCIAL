using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IPhienHocService
    {
        // 1. Khởi tạo phiên học
        Task<PhienHocResponse> BatDauPhienHocAsync(BatDauPhienHocRequest request);

        // 2. Chấm điểm và ghi nhận từng câu trả lời
        Task<ChiTietTraLoiResponse> NopCauTraLoiAsync(ChiTietTraLoiRequest request);

        // 3. Tổng kết và đóng phiên học
        Task<PhienHocResponse> KetThucPhienHocAsync(KetThucPhienHocRequest request);
    }
}