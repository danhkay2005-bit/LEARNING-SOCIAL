using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TheoDoiNguoiDungController : ControllerBase
    {
        private readonly ITheoDoiNguoiDungService _service;

        public TheoDoiNguoiDungController(ITheoDoiNguoiDungService service)
        {
            _service = service;
        }

        /// <summary>
        /// Theo dõi người dùng
        /// </summary>
        [HttpPost("theo-doi")]
        public async Task<IActionResult> TheoDoi([FromBody] TheoDoiNguoiDungRequest request)
        {
            try
            {
                var result = await _service.TheoDoiAsync(request);
                return Ok(new { success = true, data = result, message = "Theo dõi thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Bỏ theo dõi người dùng
        /// </summary>
        [HttpPost("bo-theo-doi")]
        public async Task<IActionResult> BoTheoDoi([FromBody] BoTheoDoiNguoiDungRequest request)
        {
            try
            {
                var result = await _service.BoTheoDoiAsync(request);
                return Ok(new { success = true, data = result, message = "Bỏ theo dõi thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái theo dõi
        /// </summary>
        [HttpPost("kiem-tra")]
        public async Task<IActionResult> KiemTraTheoDoi([FromBody] KiemTraTheoDoiRequest request)
        {
            try
            {
                var result = await _service.KiemTraTheoDoiAsync(request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách người theo dõi (followers)
        /// </summary>
        [HttpGet("{maNguoiDung:guid}/nguoi-theo-doi")]
        public async Task<IActionResult> LayDanhSachNguoiTheoDoi(Guid maNguoiDung)
        {
            try
            {
                var nguoiXemId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                var result = await _service.LayDanhSachNguoiTheoDoiAsync(maNguoiDung, nguoiXemId);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách người đang theo dõi (following)
        /// </summary>
        [HttpGet("{maNguoiDung:guid}/dang-theo-doi")]
        public async Task<IActionResult> LayDanhSachDangTheoDoi(Guid maNguoiDung)
        {
            try
            {
                var nguoiXemId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                var result = await _service.LayDanhSachDangTheoDoiAsync(maNguoiDung, nguoiXemId);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thống kê theo dõi
        /// </summary>
        [HttpGet("{maNguoiDung:guid}/thong-ke")]
        public async Task<IActionResult> LayThongKeTheoDoi(Guid maNguoiDung)
        {
            try
            {
                var result = await _service.LayThongKeTheoDoiAsync(maNguoiDung);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy gợi ý người dùng để theo dõi
        /// </summary>
        [HttpGet("goi-y")]
        public async Task<IActionResult> LayGoiYTheoDoi([FromQuery] int soLuong = 10)
        {
            try
            {
                var maNguoiDung = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                var result = await _service.LayGoiYTheoDoiAsync(maNguoiDung, soLuong);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách bạn chung
        /// </summary>
        [HttpGet("ban-chung")]
        public async Task<IActionResult> LayDanhSachBanChung([FromQuery] Guid maNguoiDung1, [FromQuery] Guid maNguoiDung2)
        {
            try
            {
                var result = await _service.LayDanhSachBanChungAsync(maNguoiDung1, maNguoiDung2);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}