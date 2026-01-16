using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentionBaiDangController : ControllerBase
    {
        private readonly IMentionBaiDangService _mentionBaiDangService;

        public MentionBaiDangController(IMentionBaiDangService mentionBaiDangService)
        {
            _mentionBaiDangService = mentionBaiDangService;
        }

        /// <summary>
        /// Lấy danh sách mention trong bài đăng
        /// </summary>
        [HttpGet("bai-dang/{maBaiDang}")]
        public async Task<IActionResult> GetDanhSachMentionByBaiDang(int maBaiDang)
        {
            try
            {
                var result = await _mentionBaiDangService.GetDanhSachMentionByBaiDangAsync(maBaiDang);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách bài đăng có mention người dùng
        /// </summary>
        [HttpGet("nguoi-dung/{maNguoiDung}")]
        public async Task<IActionResult> GetDanhSachBaiDangDuocMention(Guid maNguoiDung)
        {
            try
            {
                var result = await _mentionBaiDangService.GetDanhSachBaiDangDuocMentionAsync(maNguoiDung);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Thêm mention vào bài đăng
        /// </summary>
        [HttpPost("them")]
        public async Task<IActionResult> ThemMention([FromBody] MentionBaiDangQueryRequest request)
        {
            try
            {
                // Cần thêm MaNguoiDuocMention vào request body
                var maNguoiDuocMention = Guid.NewGuid(); // Thay bằng logic lấy từ authentication
                var result = await _mentionBaiDangService.ThemMentionAsync(request.MaBaiDang, maNguoiDuocMention);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa mention khỏi bài đăng
        /// </summary>
        [HttpDelete("xoa")]
        public async Task<IActionResult> XoaMention(int maBaiDang, Guid maNguoiDuocMention)
        {
            try
            {
                var result = await _mentionBaiDangService.XoaMentionAsync(maBaiDang, maNguoiDuocMention);
                if (result)
                {
                    return Ok(new { message = "Xóa mention thành công" });
                }
                return NotFound(new { message = "Mention không tồn tại" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Kiểm tra người dùng có được mention không
        /// </summary>
        [HttpGet("kiem-tra")]
        public async Task<IActionResult> KiemTraDuocMention(int maBaiDang, Guid maNguoiDung)
        {
            try
            {
                var result = await _mentionBaiDangService.KiemTraDuocMentionAsync(maBaiDang, maNguoiDung);
                return Ok(new { duocMention = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}