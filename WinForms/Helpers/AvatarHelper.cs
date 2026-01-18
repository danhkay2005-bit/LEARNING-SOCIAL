using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.Helpers
{
    /// <summary>
    /// 👤 Helper quản lý avatar
    /// </summary>
    public static class AvatarHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string DEFAULT_AVATAR = "default.png";

        /// <summary>
        /// 🖼️ Set avatar cho PictureBox
        /// </summary>
        public static void SetAvatar(PictureBox pictureBox, string? avatarUrl, string initials)
        {
            try
            {
                if (string.IsNullOrEmpty(avatarUrl) || avatarUrl == DEFAULT_AVATAR)
                {
                    // Hiển thị ảnh từ chữ cái đầu
                    pictureBox.Image = ImageHelper.CreateInitialsAvatar(initials, pictureBox.Width);
                }
                else
                {
                    // Load ảnh từ URL
                    _ = LoadAvatarAsync(pictureBox, avatarUrl, initials);
                }
            }
            catch
            {
                pictureBox.Image = ImageHelper.CreateInitialsAvatar(initials, pictureBox.Width);
            }
        }

        /// <summary>
        /// 📥 Load ảnh bất đồng bộ
        /// </summary>
        private static async Task LoadAvatarAsync(PictureBox pictureBox, string avatarUrl, string initials)
        {
            try
            {
                // Xác định URL đầy đủ
                string fullUrl;
                if (avatarUrl.StartsWith("http://") || avatarUrl.StartsWith("https://"))
                {
                    // URL đầy đủ (online hoặc external)
                    fullUrl = avatarUrl;
                }
                else if (avatarUrl.StartsWith("/"))
                {
                    // Relative path - thêm API base URL
                    fullUrl = $"https://localhost:7001{avatarUrl}";
                }
                else
                {
                    // Local file path
                    fullUrl = avatarUrl;
                }

                System.Diagnostics.Debug.WriteLine($"Loading avatar from: {fullUrl}");

                byte[] imageBytes;
                if (File.Exists(fullUrl))
                {
                    // Load từ file local
                    imageBytes = File.ReadAllBytes(fullUrl);
                }
                else
                {
                    // Load từ URL
                    imageBytes = await _httpClient.GetByteArrayAsync(fullUrl);
                }

                using (var ms = new MemoryStream(imageBytes))
                {
                    var image = Image.FromStream(ms);
                    var circularImage = ImageHelper.CreateCircularImage(image, pictureBox.Width);

                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke(new Action(() => pictureBox.Image = circularImage));
                    }
                    else
                    {
                        pictureBox.Image = circularImage;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Avatar loaded successfully!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading avatar: {ex.Message}");
                
                var fallbackImage = ImageHelper.CreateInitialsAvatar(initials, pictureBox.Width);

                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(new Action(() => pictureBox.Image = fallbackImage));
                }
                else
                {
                    pictureBox.Image = fallbackImage;
                }
            }
        }

        /// <summary>
        /// 📤 Upload avatar
        /// </summary>
        public static async Task<string?> UploadAvatarAsync(Guid userId, string imagePath)
        {
            try
            {
                var apiUrl = $"https://localhost:7001/api/User/upload-avatar/{userId}";

                using (var content = new MultipartFormDataContent())
                {
                    var fileBytes = File.ReadAllBytes(imagePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                    content.Add(fileContent, "file", Path.GetFileName(imagePath));

                    var response = await _httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return json;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}