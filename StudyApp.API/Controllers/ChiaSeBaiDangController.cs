using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChiaSeBaiDangController : ControllerBase
    {
        private readonly IChiaSeBaiDangService _chiaSeBaiDangService;

        public ChiaSeBaiDangController(IChiaSeBaiDangService chiaSeBaiDangService)
        {
            _chiaSeBaiDangService = chiaSeBaiDangService;
        }

        /// <summary>
        /// Chia sẻ bài đăng
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ChiaSeBaiDang([FromBody] ChiaSeBaiDangRequest request)
        {
            try
            {
                var maNguoiDung = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                request.MaNguoiChiaSe = maNguoiDung;

                var result = await _chiaSeBaiDangService.ChiaSeBaiDangAsync(request);
                return Ok(new { success = true, data = result, message = "Chia sẻ bài đăng thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách chia sẻ của bài đăng
        /// </summary>
        [HttpGet("bai-dang/{maBaiDang}")]
        [AllowAnonymous]
       
            public async Task<IActionResult> GetDanhSachChiaSeByBaiDang(int maBaiDang)
        {
            try
            {
                var result = await _chiaSeBaiDangService.LayDanhSachChiaSeTheoMaBaiDangAsync( maBaiDang);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách chia sẻ của người dùng
        /// </summary>
        [HttpGet("  kiem-tra/{maBaiDang}")]
        public async Task<IActionResult> KiemTraChiaSe(int maBaiDang)
        {
            try
            {
                var maNguoiDung = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var request = new KiemTraChiaSeRequest
                {
                    MaBaiDangGoc = maBaiDang,
                    MaNguoiDung = maNguoiDung
                };

                var result = await _chiaSeBaiDangService.LayThongKeChiaSeAsync(maBaiDang);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thống kê chia sẻ của bài đăng
        /// </summary>
        [HttpGet("thong-ke/{maBaiDang}")]
        public async Task<IActionResult> LayThongKeChiaSe(int maBaiDang)
        {
            try
            {
                var result = await _chiaSeBaiDangService.LayChiTietChiaSeAsync (maBaiDang);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách bài đăng mà người dùng đã chia sẻ
        /// </summary>
        [HttpGet("cua-toi")]
        public async Task<IActionResult> LayDanhSachBaiDangDaChiaSe([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var maNguoiDung = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var result = await _chiaSeBaiDangService.LayDanhSachChiaSeTheoNguoiDungAsync(maNguoiDung);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách bài đăng mà một người dùng khác đã chia sẻ
        /// </summary>
        [HttpGet("nguoi-dung/{maNguoiDung}")]
        public async Task<IActionResult> LayDanhSachBaiDangDaChiaSeTheoNguoiDung(Guid maNguoiDung, [FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var result = await _chiaSeBaiDangService.LayDanhSachBaiDangDaChiaSeAsync(maNguoiDung, skip, take);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy chi tiết chia sẻ bài đăng
        /// </summary>
        [HttpGet("{maChiaSe}")]
        public async Task<IActionResult> LayChiTietChiaSe(int maChiaSe)
        {
            try
            {
                var result = await _chiaSeBaiDangService.LayChiTietChiaSeAsync(maChiaSe);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}