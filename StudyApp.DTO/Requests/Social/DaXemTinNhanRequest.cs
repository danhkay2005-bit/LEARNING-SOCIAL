using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request đánh dấu đã xem tin nhắn
    public class DanhDauDaXemRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        public int? MaTinNhanCuoi { get; set; }
    }

    // Request đánh dấu đã xem nhiều tin nhắn
    public class DanhDauDaXemNhieuRequest
    {
        [Required(ErrorMessage = "Danh sách mã tin nhắn là bắt buộc")]
        public List<int> MaTinNhans { get; set; } = [];
    }
}