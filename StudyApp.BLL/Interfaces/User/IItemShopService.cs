using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IItemShopService
{
    Task<List<DanhMucSanPhamResponse>> GetCategoriesAsync();
    Task<List<VatPhamResponse>> GetShopItemsAsync();
    Task<string> BuyItemAsync(Guid userId, int vatPhamId, int soLuong);
    Task<List<KhoNguoiDungResponse>> GetMyInventoryAsync(Guid userId);
}