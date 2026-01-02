using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request lấy danh sách mention của người dùng
    public class LayMentionRequest
    {
        public bool? DaDoc { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    // Request đánh dấu mention đã đọc
    public class DanhDauMentionDaDocRequest
    {
        public int? MaBaiDang { get; set; }
        public int? MaBinhLuan { get; set; }
    }
}