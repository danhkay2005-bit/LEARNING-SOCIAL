using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.User;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IWebHostEnvironment _environment;

        public UserController(
            IUserProfileService userProfileService,
            IWebHostEnvironment environment)
        {
            _userProfileService = userProfileService;
            _environment = environment;
        }

        /// <summary>
        /// 📤 API Upload Avatar
        /// </summary>
        [HttpPost("upload-avatar/{userId}")]
        public async Task<IActionResult> UploadAvatar(Guid userId, IFormFile file)
        {
            try
            {
                // ✅ Kiểm tra file
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "Vui lòng chọn ảnh" });
                }

                // ✅ Kiểm tra định dạng
                var allowedExtensions = new[] { ". jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest(new { message = "Chỉ chấp nhận file ảnh (. jpg, .png, .gif)" });
                }

                // ✅ Kiểm tra kích thước (max 5MB)
                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest(new { message = "Ảnh không được lớn hơn 5MB" });
                }

                // ✅ Tạo tên file unique
                var fileName = $"user_{userId}{extension}";
                var uploadPath = Path.Combine(_environment.WebRootPath, "avatars");

                // ✅ Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                // ✅ Xóa ảnh cũ nếu có
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // ✅ Lưu file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // ✅ Cập nhật database
                var avatarUrl = $"/avatars/{fileName}";
                var success = await _userProfileService.UpdateAvatarAsync(userId, avatarUrl);

                if (!success)
                {
                    return BadRequest(new { message = "Cập nhật database thất bại" });
                }

                return Ok(new
                {
                    message = "Upload thành công",
                    avatarUrl = avatarUrl
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi:  {ex.Message}" });
            }
        }

        /// <summary>
        /// 🖼️ API Lấy Avatar
        /// </summary>
        [HttpGet("avatar/{userId}")]
        public async Task<IActionResult> GetAvatar(Guid userId)
        {
            try
            {
                var avatarUrl = await _userProfileService.GetAvatarUrlAsync(userId);

                if (string.IsNullOrEmpty(avatarUrl))
                {
                    avatarUrl = "/avatars/default.png";
                }

                return Ok(new { avatarUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}