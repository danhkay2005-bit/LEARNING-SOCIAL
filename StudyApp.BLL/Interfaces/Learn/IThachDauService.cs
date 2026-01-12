using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IThachDauService
    {
        // Quản lý phòng thách đấu
        Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request);
        Task<bool> BatDauThachDauAsync(int maThachDau);
        Task<bool> KetThucThachDauAsync(int maThachDau);
        Task<bool> HuyThachDauAsync(int maThachDau);

        // Quản lý người chơi
        Task<bool> ThamGiaThachDauAsync(ThamGiaThachDauRequest request);
        Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request);

        // Truy vấn dữ liệu
        Task<ThachDauResponse?> GetByIdAsync(int maThachDau);
        Task<IEnumerable<ThachDauNguoiChoiResponse>> GetBangXepHangAsync(int maThachDau);
    }
}