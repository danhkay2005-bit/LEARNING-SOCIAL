using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Requests.Social;
using System;
using System.Threading.Tasks;

namespace StudyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("danh-sach")]
        public async Task<IActionResult> GetDanhSachThongBao([FromQuery] DanhSachThongBaoRequest request)
        {
            var result = await _thongBaoService.GetDanhSachThongBaoAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Lấy thông báo theo ID
        /// </summary>
        [HttpGet("{maThongBao}")]
        public async Task<IActionResult> GetThongBaoById(int maThongBao)
        {
            var result = await _thongBaoService.GetThongBaoByIdAsync(maThongBao);
            
            if (result == null)
            {
                return NotFound(new { message = "Không tìm thấy thông báo" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Lấy thống kê thông báo của người dùng
        /// </summary>
        [HttpGet("thong-ke/{maNguoiNhan}")]
        public async Task<IActionResult> GetThongKe(Guid maNguoiNhan)
        {
            var result = await _thongBaoService.GetThongKeThongBaoAsync(maNguoiNhan);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách thông báo chưa đọc (dropdown/badge)
        /// </summary>
        [HttpGet("chua-doc/{maNguoiNhan}")]
        public async Task<IActionResult> GetThongBaoChuaDoc(Guid maNguoiNhan, [FromQuery] int soLuong = 10)
        {
            var result = await _thongBaoService.GetThongBaoChuaDocAsync(maNguoiNhan, soLuong);
            return Ok(result);
        }

        /// <summary>
        /// Tạo thông báo mới (hệ thống)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> TaoThongBao([FromBody] TaoThongBaoRequest request)
        {
            var result = await _thongBaoService.TaoThongBaoAsync(request);
            return CreatedAtAction(nameof(GetThongBaoById), new { maThongBao = result.MaThongBao }, result);
        }

        /// <summary>
        /// Đánh dấu một thông báo là đã đọc
        /// </summary>
        [HttpPut("danh-dau-da-doc/{maThongBao}")]
        public async Task<IActionResult> DanhDauDaDoc(int maThongBao)
        {
            var result = await _thongBaoService.DanhDauDaDocAsync(maThongBao);
            
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy thông báo" });
            }

            return Ok(new { message = "Đã đánh dấu thông báo là đã đọc" });
        }

        /// <summary>
        /// Đánh dấu tất cả thông báo là đã đọc
        /// </summary>
        [HttpPut("danh-dau-tat-ca-da-doc/{maNguoiNhan}")]
        public async Task<IActionResult> DanhDauTatCaDaDoc(Guid maNguoiNhan)
        {
            var soLuong = await _thongBaoService.DanhDauTatCaDaDocAsync(maNguoiNhan);
            return Ok(new { message = $"Đã đánh dấu {soLuong} thông báo là đã đọc" });
        }

        /// <summary>
        /// Xóa thông báo
        /// </summary>
        [HttpDelete("{maThongBao}")]
        public async Task<IActionResult> XoaThongBao(int maThongBao, [FromQuery] Guid maNguoiNhan)
        {
            var result = await _thongBaoService.XoaThongBaoAsync(maThongBao, maNguoiNhan);
            
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy thông báo hoặc bạn không có quyền xóa" });
            }

            return Ok(new { message = "Đã xóa thông báo thành công" });
        }

        /// <summary>
        /// Xóa tất cả thông báo đã đọc
        /// </summary>
        [HttpDelete("xoa-tat-ca-da-doc/{maNguoiNhan}")]
        public async Task<IActionResult> XoaTatCaThongBaoDaDoc(Guid maNguoiNhan)
        {
            var soLuong = await _thongBaoService.XoaTatCaThongBaoDaDocAsync(maNguoiNhan);
            return Ok(new { message = $"Đã xóa {soLuong} thông báo đã đọc" });
        }
    }
}