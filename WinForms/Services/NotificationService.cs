using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WinForms.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ThongBaoResponse>> GetUserNotificationsAsync(DanhSachThongBaoRequest request)
        {
            try
            {
                var queryString = $"?MaNguoiNhan={request.MaNguoiNhan}" +
                                  $"&Trang={request.Trang}" +
                                  $"&KichThuocTrang={request.KichThuocTrang}";

                if (request.ChiChuaDoc.HasValue)
                    queryString += $"&ChiChuaDoc={request.ChiChuaDoc.Value}";

                if (request.LoaiThongBao.HasValue)
                    queryString += $"&LoaiThongBao={(int)request.LoaiThongBao.Value}";

                var response = await _httpClient.GetFromJsonAsync<PhanTrangThongBaoResponse>(
                    $"api/ThongBao/danh-sach{queryString}");

                return response?.DanhSach ?? new List<ThongBaoResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading notifications: {ex.Message}");
                return new List<ThongBaoResponse>();
            }
        }

        public async Task<int> GetUnreadCountAsync(Guid maNguoiDung)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ThongKeThongBaoResponse>(
                    $"api/ThongBao/thong-ke/{maNguoiDung}");

                return response?.SoThongBaoChuaDoc ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting unread count: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> MarkAsReadAsync(int maThongBao)
        {
            try
            {
                var response = await _httpClient.PutAsync(
                    $"api/ThongBao/danh-dau-da-doc/{maThongBao}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking as read: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> MarkAllAsReadAsync(Guid maNguoiDung)
        {
            try
            {
                var response = await _httpClient.PutAsync(
                    $"api/ThongBao/danh-dau-tat-ca-da-doc/{maNguoiDung}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking all as read: {ex.Message}");
                return false;
            }
        }
    }
}