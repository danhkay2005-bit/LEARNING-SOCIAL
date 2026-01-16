using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentionBinhLuanController : ControllerBase
    {
        private readonly IMentionBinhLuanService _mentionBinhLuanService;

        public MentionBinhLuanController(IMentionBinhLuanService mentionBinhLuanService)
        {
            _mentionBinhLuanService = mentionBinhLuanService;
        }

        /// Lấy danh sách mention trong bình luận
    
        [HttpGet("binh-luan/{maBinhLuan}")]
        public async Task<IActionResult> GetDanhSachMentionByBinhLuan(int maBinhLuan)
        {
            try
            {
                var result = await _mentionBinhLuanService.GetDanhSachMentionByBinhLuanAsync(maBinhLuan);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách bình luận có mention người dùng
        /// </summary>
        [HttpGet("nguoi-dung/{maNguoiDung}")]
        public async Task<IActionResult> GetDanhSachBinhLuanDuocMention(Guid maNguoiDung)
        {
            try
            {
                var result = await _mentionBinhLuanService.GetDanhSachBinhLuanDuocMentionAsync(maNguoiDung);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Thêm mention vào bình luận
        /// </summary>
        [HttpPost("them")]
        public async Task<IActionResult> ThemMention([FromBody] ThemMentionBinhLuanRequest request)
        {
            try
            {
                var result = await _mentionBinhLuanService.ThemMentionAsync(request.MaBinhLuan, request.MaNguoiDuocMention);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa mention khỏi bình luận
        /// </summary>
        [HttpDelete("xoa")]
        public async Task<IActionResult> XoaMention([FromBody] XoaMentionBinhLuanRequest request)
        {
            try
            {
                var result = await _mentionBinhLuanService.XoaMentionAsync(request.MaBinhLuan, request.MaNguoiDuocMention);
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
        public async Task<IActionResult> KiemTraDuocMention(int maBinhLuan, Guid maNguoiDung)
        {
            try
            {
                var result = await _mentionBinhLuanService.KiemTraDuocMentionAsync(maBinhLuan, maNguoiDung);
                return Ok(new { duocMention = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách mention của nhiều bình luận (bulk)
        /// </summary>
        [HttpPost("bulk")]
        public async Task<IActionResult> GetDanhSachMentionByBinhLuans([FromBody] List<int> maBinhLuans)
        {
            try
            {
                var result = await _mentionBinhLuanService.GetDanhSachMentionByBinhLuansAsync(maBinhLuans);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}