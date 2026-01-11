using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learn
{
    public class ThuMucResponse
    {
        public int MaThuMuc { get; set; }
        public string TenThuMuc { get; set; } = null!;
        public int? MaThuMucCha { get; set; }

        public List<ThuMucResponse> ThuMucCon { get; set; } = [];
    }
}