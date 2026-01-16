using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ThongBaoController : ControllerBase
    {
        private readonly IThongBaoService _thongBaoService;

        public ThongBaoController(IThongBaoService thongBaoService)
        {
            _thongBaoService = thongBaoService;
        }

        /// <summary>
        /// Lấy danh sách thông báo có phân trang
        /// </summary>
        [HttpPost("danh-sach")]
        public async Task<IActionResult> GetDanhSachThongBao([FromBody] DanhSachThongBaoRequest request)
        {
            try
            {
                var result = await _thongBaoService.GetDanhSachThongBaoAsync(request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông báo theo ID
        /// </summary>
        [HttpGet("{maThongBao}")]
        public async Task<IActionResult> GetThongBaoById(int maThongBao)
        {
            try
            {
                var result = await _thongBaoService.GetThongBaoByIdAsync(maThongBao);
                
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy thông báo" });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thống kê thông báo
        /// </summary>
        [HttpGet("thong-ke/{maNguoiNhan}")]
        public async Task<IActionResult> GetThongKeThongBao(Guid maNguoiNhan)
        {
            try
            {
                var result = await _thongBaoService.GetThongKeThongBaoAsync(maNguoiNhan);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách thông báo chưa đọc (rút gọn)
        /// </summary>
        [HttpGet("chua-doc/{maNguoiNhan}")]
        public async Task<IActionResult> GetThongBaoChuaDoc(Guid maNguoiNhan, [FromQuery] int soLuong = 10)
        {
            try
            {
                var result = await _thongBaoService.GetThongBaoChuaDocAsync(maNguoiNhan, soLuong);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Tạo thông báo thủ công (Admin/System)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TaoThongBao([FromBody] TaoThongBaoRequest request)
        {
            try
            {
                var result = await _thongBaoService.TaoThongBaoAsync(request);
                return Ok(new { success = true, data = result, message = "Tạo thông báo thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Đánh dấu một thông báo là đã đọc
        /// </summary>
        [HttpPut("{maThongBao}/danh-dau-da-doc")]
        public async Task<IActionResult> DanhDauDaDoc(int maThongBao)
        {
            try
            {
                var result = await _thongBaoService.DanhDauDaDocAsync(maThongBao);
                
                if (result)
                {
                    return Ok(new { success = true, message = "Đã đánh dấu đọc thông báo" });
                }

                return NotFound(new { success = false, message = "Không tìm thấy thông báo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Đánh dấu tất cả thông báo là đã đọc
        /// </summary>
        [HttpPut("danh-dau-tat-ca-da-doc/{maNguoiNhan}")]
        public async Task<IActionResult> DanhDauTatCaDaDoc(Guid maNguoiNhan)
        {
            try
            {
                var result = await _thongBaoService.DanhDauTatCaDaDocAsync(maNguoiNhan);
                return Ok(new { success = true, data = result, message = $"Đã đánh dấu đọc {result} thông báo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa một thông báo
        /// </summary>
        [HttpDelete("{maThongBao}/nguoi-nhan/{maNguoiNhan}")]
        public async Task<IActionResult> XoaThongBao(int maThongBao, Guid maNguoiNhan)
        {
            try
            {
                var result = await _thongBaoService.XoaThongBaoAsync(maThongBao, maNguoiNhan);
                
                if (result)
                {
                    return Ok(new { success = true, message = "Xóa thông báo thành công" });
                }

                return NotFound(new { success = false, message = "Không tìm thấy thông báo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa tất cả thông báo đã đọc
        /// </summary>
        [HttpDelete("xoa-tat-ca-da-doc/{maNguoiNhan}")]
        public async Task<IActionResult> XoaTatCaThongBaoDaDoc(Guid maNguoiNhan)
        {
            try
            {
                var result = await _thongBaoService.XoaTatCaThongBaoDaDocAsync(maNguoiNhan);
                return Ok(new { success = true, data = result, message = $"Đã xóa {result} thông báo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}