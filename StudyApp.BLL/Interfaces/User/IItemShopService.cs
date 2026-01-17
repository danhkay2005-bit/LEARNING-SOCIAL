using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Interfaces.User;

public interface IItemShopService
{
    /// <summary>
    /// Lấy danh sách các danh mục sản phẩm
    /// </summary>
    Task<List<DanhMucSanPhamResponse>> GetCategoriesAsync();

    /// <summary>
    /// Lấy danh sách các vật phẩm có sẵn trong cửa hàng
    /// </summary>
    Task<List<VatPhamResponse>> GetShopItemsAsync();

    /// <summary>
    /// Mua vật phẩm
    /// </summary>
    Task<string> BuyItemAsync(Guid userId, int vatPhamId, int soLuong);

    /// <summary>
    /// Lấy danh sách vật phẩm trong kho người dùng
    /// </summary>
    Task<List<KhoNguoiDungResponse>> GetMyInventoryAsync(Guid userId);

    /// <summary>
    /// Sử dụng vật phẩm hồi sinh chuỗi
    /// </summary>
    Task<string> UseResurrectItemAsync(Guid userId, int khoVatPhamId);
}