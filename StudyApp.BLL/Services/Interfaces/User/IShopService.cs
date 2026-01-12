using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public interface IShopService
    {
        Task<List<VatPhamResponse>> GetShopItemsAsync(CancellationToken cancellationToken = default);
        Task<bool> BuyItemAsync(MuaVatPhamRequest request, CancellationToken cancellationToken = default);
        Task<List<KhoNguoiDungResponse>> GetUserInventoryAsync(Guid maNguoiDung, CancellationToken cancellationToken = default);
        Task<bool> UseItemAsync(SuDungVatPhamRequest request, CancellationToken cancellationToken = default);
    }
}